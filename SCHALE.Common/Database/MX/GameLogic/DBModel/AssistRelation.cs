namespace MX.GameLogic.DBModel
{
    [Flags]
    public enum AssistRelation
    {
        None = 0,
        Clan = 1,
        Friend = 2,
        Cheat = 4,
        Stranger = 8
    }
}
