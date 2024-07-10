namespace CS_Emerios_API_Tracker.Entities
{
    public class CallLogs
    {
        public int C_Id { get; set; }
        public int C_AgentId { get; set; }
        public string DestinationPhone { get; set; } = null!;
        public string Callback_Url { get; set; } = null!;        
        public string Ip { get; set; } = null!;
        public bool Status { get; set; }
        public string Audio_file_sent { get; set; } = null!;
        public string Call_Id { get; set; } = null!;
        public DateTime Date_added { get; set; }
        public DateTime Date_updated { get; set; }
        public int Audio_length { get; set; }
    }
}
