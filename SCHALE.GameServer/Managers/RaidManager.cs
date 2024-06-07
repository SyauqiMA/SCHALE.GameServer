using MX.Logic.Data;
using SCHALE.Common.Database;
using SCHALE.Common.FlatData;
using SCHALE.Common.NetworkProtocol;
using SCHALE.GameServer.Controllers.Api.ProtocolHandlers;
using SCHALE.GameServer.Utils;

namespace SCHALE.GameServer.Managers
{
    public class RaidManager : Singleton<RaidManager>
    {
        public SingleRaidLobbyInfoDB? RaidLobbyInfoDB { get; private set; }

        public RaidDB? RaidDB { get; private set; }

        public RaidBattleDB? RaidBattleDB { get; private set; }

        public SingleRaidLobbyInfoDB GetLobby(
            RaidInfo raidInfo,
            RaidSeasonManageExcelT targetSeasonData
        )
        {
            if (RaidLobbyInfoDB == null || RaidLobbyInfoDB.SeasonId != raidInfo.SeasonId)
            {
                RaidLobbyInfoDB = new SingleRaidLobbyInfoDB()
                {
                    Tier = 0,
                    Ranking = 1,
                    SeasonId = raidInfo.SeasonId,
                    BestRankingPoint = raidInfo.BestRankingPoint,
                    TotalRankingPoint = raidInfo.TotalRankingPoint,
                    ReceiveRewardIds = [],
                    PlayableHighestDifficulty = new()
                    {
                        { targetSeasonData.OpenRaidBossGroup.First(), Difficulty.Torment }
                    }
                };
            }
            else
            {
                RaidLobbyInfoDB.BestRankingPoint = raidInfo.BestRankingPoint;
                RaidLobbyInfoDB.TotalRankingPoint = raidInfo.TotalRankingPoint;
            }
            if (RaidDB != null)
                RaidLobbyInfoDB.PlayingRaidDB = RaidDB;
            return RaidLobbyInfoDB;
        }

        public RaidDB CreateRaid(AccountDB account, CharacterStatExcelT bossData, bool isPractice)
        {
            if (RaidDB == null)
            {
                RaidDB = new()
                {
                    Owner = new() { AccountId = account.ServerId, AccountName = account.Nickname, },

                    ContentType = ContentType.Raid,
                    UniqueId = account.RaidInfo.CurrentRaidUniqueId,
                    SeasonId = account.RaidInfo.SeasonId,
                    Begin = TimeManager.KoreaNow,
                    End = TimeManager.KoreaNow.AddHours(1),
                    PlayerCount = 1,
                    SecretCode = "0",
                    RaidState = RaidStatus.Playing,
                    IsPractice = isPractice,
                    RaidBossDBs =
                    [
                        new() { ContentType = ContentType.Raid, BossCurrentHP = bossData.MaxHP100 }
                    ],
                    AccountLevelWhenCreateDB = account.Level
                };
            }
            else
            {
                RaidDB.BossDifficulty = account.RaidInfo.CurrentDifficulty;
                RaidDB.UniqueId = account.RaidInfo.CurrentRaidUniqueId;
                RaidDB.IsPractice = isPractice;
            }

            return RaidDB;
        }

        public RaidBattleDB CreateBattle(AccountDB account)
        {
            if (RaidBattleDB == null)
            {
                RaidBattleDB = new()
                {
                    ContentType = ContentType.Raid,
                    RaidUniqueId = account.RaidInfo.CurrentRaidUniqueId,
                    CurrentBossHP = RaidDB!.RaidBossDBs.First().BossCurrentHP,
                    RaidMembers =
                    [
                        new() { AccountId = account.ServerId, AccountName = account.Nickname, }
                    ]
                };
            }
            else
            {
                RaidBattleDB.RaidUniqueId = account.RaidInfo.CurrentRaidUniqueId;
            }

            return RaidBattleDB;
        }

        public bool EndBattle(AccountDB account, RaidBossResultCollection bossResults)
        {
            var battle = Instance.RaidBattleDB!;
            var raid = Instance.RaidDB!;
            RaidMemberDescription raidMember = battle.RaidMembers[0];
            raidMember.DamageCollection ??= [];

            battle.CurrentBossHP -= bossResults[0].RaidDamage.GivenDamage;
            battle.CurrentBossGroggy += bossResults[0].RaidDamage.GivenGroggyPoint;
            battle.CurrentBossAIPhase = bossResults[0].AIPhase;
            battle.SubPartsHPs = bossResults[0].SubPartsHPs;

            for (var i = 0; i < raid.RaidBossDBs.Count; i++)
            {
                raid.RaidBossDBs[i].BossCurrentHP -= bossResults[i].RaidDamage.GivenDamage;
                raid.RaidBossDBs[i].BossGroggyPoint += bossResults[i].RaidDamage.GivenGroggyPoint;
                if (!raidMember.DamageCollection.Contains(i))
                    raidMember.DamageCollection.Add(new() { Index = i });
                raidMember.DamageCollection[i].GivenDamage += bossResults[i].RaidDamage.GivenDamage;
                raidMember.DamageCollection[i].GivenGroggyPoint += bossResults[i]
                    .RaidDamage
                    .GivenGroggyPoint;
            }

            return battle.CurrentBossHP <= 0;
        }

        public void FinishRaid(RaidInfo raidInfo)
        {
            ArgumentNullException.ThrowIfNull(RaidLobbyInfoDB, nameof(RaidLobbyInfoDB));
            RaidLobbyInfoDB.BestRankingPoint = raidInfo.BestRankingPoint;
            RaidLobbyInfoDB.TotalRankingPoint = raidInfo.TotalRankingPoint;
            RaidLobbyInfoDB.PlayingRaidDB = null;

            RaidDB = null;
            RaidBattleDB = null;
        }

        public static long CalculateTimeScore(float duration, Difficulty difficulty)
        {
            int[] multipliers = [120, 240, 480, 960, 1440, 1920, 2400]; // from wiki

            return (long)((3600f - duration) * multipliers[(int)difficulty]);
        }
    }
}
