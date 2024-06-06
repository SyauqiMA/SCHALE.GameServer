using Microsoft.AspNetCore.Mvc;

namespace SCHALE.GameServer.Controllers.Data
{
    [ApiController]
    [Route("/data")]
    public class DataController : ControllerBase
    {
        private readonly string dataFolder;
        private readonly string? resourcesVersion;
        private readonly ILogger<DataController> logger;
        private readonly IConfiguration configuration;

        public DataController(ILogger<DataController> _logger, IConfiguration _configuration)
        {
            logger = _logger;
            configuration = _configuration;
            resourcesVersion = configuration["Resources:Version"];

            string? resourcesFolder = configuration["Resources:Folder"];
            if (resourcesFolder == null)
            {
                resourcesFolder = Path.Combine(AppContext.BaseDirectory, "Resources");
                Directory.CreateDirectory(resourcesFolder);
            }
            else if (!Directory.Exists(resourcesFolder))
            {
                throw new DirectoryNotFoundException(resourcesFolder);
            }
            dataFolder = Path.Combine(resourcesFolder, "data");
            Directory.CreateDirectory(dataFolder);
        }

        string? AbsolutePath(string relativePath)
        {
            string filePath = Path.Combine(dataFolder, relativePath);
            if (!System.IO.File.Exists(filePath))
                return null;
            logger.LogInformation("Using our own {relativePath}.", relativePath);
            return filePath;
        }

        [HttpGet("{version}/{*relativePath}")]
        public IActionResult CatchAll(string version, string relativePath)
        {
            string? filePath = AbsolutePath(relativePath);
            if (filePath != null)
            {
                if (filePath.EndsWith(".json"))
                {
                    var jsonContent = System.IO.File.ReadAllText(filePath);
                    return Content(jsonContent, "application/json");
                }
                if (filePath.EndsWith(".zip"))
                {
                    var fileStream = System.IO.File.OpenRead(filePath);
                    return File(fileStream, "application/zip", Path.GetFileName(filePath));
                }
                // binary files
                {
                    var fileStream = System.IO.File.OpenRead(filePath);
                    return File(fileStream, "application/octet-stream", Path.GetFileName(filePath));
                }
            }

            string ver = resourcesVersion ?? version;
            string url = $"https://prod-clientpatch.bluearchiveyostar.com/{ver}/{relativePath}";
            logger.LogDebug("Redirect to: {path}", url);
            return Redirect(url);
        }
    }
}
