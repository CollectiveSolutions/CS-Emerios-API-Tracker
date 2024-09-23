namespace CS_Emerios_API_Tracker.Entities
{
    public class CallActivityLogs
    {
        public int LogId { get; set; }
        public string ActivityLog { get; set; } = null!;
        public DateTime DateAdded { get; set; }
        public DateTime DateAddedEst { get; set; }
    }
}
