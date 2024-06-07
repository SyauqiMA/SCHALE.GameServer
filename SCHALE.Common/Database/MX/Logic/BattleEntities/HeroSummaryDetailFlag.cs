namespace MX.Logic.BattleEntities
{
    [Flags]
    public enum HeroSummaryDetailFlag
    {
        None = 0,
        BattleProperty = 2,
        BattleStatistics = 4,
        NumericLogs = 8,
        StatSnapshot = 16,
        Default = 14,
        All = 30,
    }
}
