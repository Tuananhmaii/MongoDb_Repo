namespace MongoDb_Repo.Domain.Models
{
    public record SkillProperty
    {
        public string? SkillId { get; init; }
        public string? AdvancedLevelRequirements { get; set; }
        public string? ExpertLevelRequirements { get; set; }
        public LevelRequirement? LevelRequirements { get; set; }
        public int Weight { get; set; }
        public bool Upgraded { get; set; }
        public List<KeyValuePair<string, string>>? Others { get; set; }
    }

    public enum LevelRequirement
    {
        Rookie,
        Junior,
        Middle,
        Senior
    }

}
