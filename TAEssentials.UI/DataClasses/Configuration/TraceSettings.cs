namespace TAEssentials.UI.DataClasses.Configuration
{
    public class TraceSettings
    {
        public bool EnableTracing { get; set; }
        public bool Screenshots { get; set; }
        public bool Snapshots { get; set; }
        public bool Sources { get; set; }

        public TraceSettings() { }
    }
}