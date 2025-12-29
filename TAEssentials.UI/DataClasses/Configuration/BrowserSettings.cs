namespace TAEssentials.UI.DataClasses.Configuration
{
    public class BrowserSettings
    {
        public bool Headless { get; set; }
        public string? Channel { get; set; }
        public int? SlowMo { get; set; }
        public int DefaultExpectedTimeout { get; set; }

        public BrowserSettings() { }
    }
}