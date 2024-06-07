namespace MX.Logic.Battles.Summary
{
    public class SkillCostSummary
    {
        public float InitialCost { get; set; }

        public CostRegenSnapshotCollection CostPerFrameSnapshots { get; set; }

        public List<SkillCostAddSnapshot> CostAddSnapshots { get; set; }

        // public void SetInitialSkillCost(float initialCost) { }

        // public void WriteIfCostAdded(long frame, float addedCost) { }

        // public void WriteIfCostUsed(
        //     long frame,
        //     long characterId,
        //     float usedCost,
        //     string groupId,
        //     int skillLevel
        // ) { }

        // public void WriteIfCostPerFrameChanged(long frame, float costPerFrame) { }
    }
}
