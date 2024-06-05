using SCHALE.Common.Database;
using SCHALE.Common.Database.ModelExtensions;
using SCHALE.Common.NetworkProtocol;
using SCHALE.GameServer.Services;

namespace SCHALE.GameServer.Controllers.Api.ProtocolHandlers
{
    public class Character(
        IProtocolHandlerFactory protocolHandlerFactory,
        ISessionKeyService _sessionKeyService,
        SCHALEContext _context,
        ExcelTableService _excelTableService
    ) : ProtocolHandlerBase(protocolHandlerFactory)
    {
        private readonly ISessionKeyService sessionKeyService = _sessionKeyService;
        private readonly SCHALEContext context = _context;
        private readonly ExcelTableService excelTableService = _excelTableService;

        // [ProtocolHandler(Protocol.Character_BatchSkillLevelUpdate)]
        // public ResponsePacket BatchSkillLevelUpdateHandler(
        //     CharacterBatchSkillLevelUpdateRequest req
        // )
        // {
        //     var account = sessionKeyService.GetAccount(req.SessionKey);
        //     var targetCharacter = account.Characters.First(x =>
        //         x.ServerId == req.TargetCharacterDBId
        //     );

        //     foreach (var skillReq in req.SkillLevelUpdateRequestDBs)
        //     {
        //         switch (skillReq.SkillSlot)
        //         {
        //             case SkillSlot.ExSkill01:
        //                 targetCharacter.ExSkillLevel = skillReq.Level;
        //                 break;
        //             case SkillSlot.PublicSkill01:
        //                 targetCharacter.PublicSkillLevel = skillReq.Level;
        //                 break;
        //             case SkillSlot.Passive01:
        //                 targetCharacter.PassiveSkillLevel = skillReq.Level;
        //                 break;
        //             case SkillSlot.ExtraPassive01:
        //                 targetCharacter.ExtraPassiveSkillLevel = skillReq.Level;
        //                 break;
        //             default:
        //                 throw new NotImplementedException();
        //         }
        //     }

        //     context.SaveChanges();

        //     return new CharacterBatchSkillLevelUpdateResponse()
        //     {
        //         CharacterDB = targetCharacter,
        //         ParcelResultDB = "TODO: SO MUCH WORK",
        //     };
        // }

        [ProtocolHandler(Protocol.Character_PotentialGrowth)]
        public ResponsePacket PotentialGrowthHandler(CharacterPotentialGrowthRequest req)
        {
            var account = sessionKeyService.GetAccount(req.SessionKey);
            var targetCharacter = account.Characters.First(x =>
                x.ServerId == req.TargetCharacterDBId
            );

            foreach (var growthReq in req.PotentialGrowthRequestDBs)
            {
                targetCharacter.PotentialStats[(int)growthReq.Type] = growthReq.Level;
            }

            context.SaveChanges();

            return new CharacterPotentialGrowthResponse() { CharacterDB = targetCharacter };
        }

        [ProtocolHandler(Protocol.Character_SetFavorites)]
        public ResponsePacket SetFavoritesHandler(CharacterSetFavoritesRequest req)
        {
            return new CharacterSetFavoritesResponse();
        }

        [ProtocolHandler(Protocol.Character_UnlockWeapon)]
        public ResponsePacket UnlockWeaponHandler(CharacterUnlockWeaponRequest req)
        {
            var account = sessionKeyService.GetAccount(req.SessionKey);
            var newWeapon = new WeaponDB()
            {
                UniqueId = account
                    .Characters.First(x => x.ServerId == req.TargetCharacterServerId)
                    .UniqueId,
                BoundCharacterServerId = req.TargetCharacterServerId,
                IsLocked = false,
                StarGrade = 1,
                Level = 1
            };

            account.AddWeapons(context, [newWeapon]);
            context.SaveChanges();

            return new CharacterUnlockWeaponResponse() { WeaponDB = newWeapon, };
        }
    }
}
