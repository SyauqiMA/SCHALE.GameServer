using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCHALE.Common.Crypto;
using SCHALE.Common.NetworkProtocol;
using SCHALE.GameServer.Controllers.Api.ProtocolHandlers;

namespace SCHALE.GameServer.Controllers.Api
{
    [Route("/api/[controller]")]
    public class GatewayController(
        IProtocolHandlerFactory _protocolHandlerFactory,
        ILogger<GatewayController> _logger
    ) : ControllerBase
    {
        private static readonly JsonSerializerSettings jsonOptions =
            new() { DefaultValueHandling = DefaultValueHandling.Ignore };

        private readonly IProtocolHandlerFactory protocolHandlerFactory = _protocolHandlerFactory;
        private readonly ILogger<GatewayController> logger = _logger;

        [HttpPost]
        public IResult GatewayRequest()
        {
            var formFile = Request.Form.Files.GetFile("mx");
            if (formFile is null)
                return Results.BadRequest("Expecting an mx file");

            using var reader = new BinaryReader(formFile.OpenReadStream());

            // CRC + Protocol type conversion + payload length
            reader.BaseStream.Position = 12;

            byte[] compressedPayload = reader.ReadBytes((int)reader.BaseStream.Length - 12);
            XOR.Crypt(compressedPayload, [0xD9]);
            using var gzStream = new GZipStream(
                new MemoryStream(compressedPayload),
                CompressionMode.Decompress
            );
            using var payloadMs = new MemoryStream();
            gzStream.CopyTo(payloadMs);

            try
            {
                var payloadStr = Encoding.UTF8.GetString(payloadMs.ToArray());
                var jsonNode = JObject.Parse(payloadStr);
                var protocol = (Protocol?)(int?)jsonNode?["Protocol"] ?? Protocol.None;

                logger.LogDebug("Protocol: {Protocol}", protocol.ToString());
                logger.LogDebug("Protocol: {Protocol}", (int)protocol);

                if (protocol == Protocol.None)
                {
                    logger.LogWarning(
                        "Failed to read protocol from JsonNode, {Payload:j}",
                        payloadStr
                    );
                    goto protocolErrorRet;
                }

                var requestType = protocolHandlerFactory.GetRequestPacketTypeByProtocol(protocol);
                if (requestType is null)
                {
                    logger.LogError(
                        "Protocol {Protocol} doesn't have corresponding type registered",
                        protocol
                    );
                    goto protocolErrorRet;
                }

                logger.LogDebug(payloadStr);

                var payload = (
                    JsonConvert.DeserializeObject(payloadStr, requestType) as RequestPacket
                )!;

                var rsp = protocolHandlerFactory.Invoke(protocol, payload);

                if (rsp is null)
                {
                    logger.LogDebug("{Protocol} {Payload:j}", payload.Protocol, payloadStr);
                    logger.LogError(
                        "Protocol {Protocol} is unimplemented and left unhandled",
                        payload.Protocol
                    );

                    goto protocolErrorRet;
                }

                if ((rsp as BasePacket)!.SessionKey is null)
                    (rsp as BasePacket)!.SessionKey = payload.SessionKey;

                return Results.Json(
                    new
                    {
                        packet = JsonConvert.SerializeObject(rsp, jsonOptions),
                        protocol = ((BasePacket)rsp).Protocol.ToString()
                    }
                );

                protocolErrorRet:
                return Results.Json(
                    new
                    {
                        packet = JsonConvert.SerializeObject(
                            new ErrorPacket()
                            {
                                Reason = "Protocol not implemented (Server Error)",
                                ErrorCode = WebAPIErrorCode.ServerFailedToHandleRequest
                            },
                            jsonOptions
                        ),
                        protocol = Protocol.Error.ToString()
                    }
                );
            }
            catch (WebAPIException ex)
            {
                return Results.Json(
                    new
                    {
                        packet = JsonConvert.SerializeObject(
                            new ErrorPacket()
                            {
                                Reason = string.IsNullOrEmpty(ex.Message)
                                    ? ex.ErrorCode.ToString()
                                    : ex.Message,
                                ErrorCode = ex.ErrorCode
                            },
                            jsonOptions
                        ),
                        protocol = Protocol.Error.ToString()
                    }
                );
            }
        }
    }

    public class WebAPIException : Exception
    {
        public WebAPIErrorCode ErrorCode { get; }

        public WebAPIException()
        {
            ErrorCode = WebAPIErrorCode.InternalServerError;
        }

        public WebAPIException(WebAPIErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public WebAPIException(WebAPIErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
