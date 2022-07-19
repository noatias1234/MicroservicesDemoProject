namespace MapRepositoryService.Core.Configuration
{
    public class Settings
    {
        public string HostName { get; set; } = "";
        public string MapRepositoryTopic { get; set; } = "";

        public string EndPoint { get; set; } = ""; // URL to obj storage service
        public string BucketName { get; set; } = "";
        public string Location { get; set; } = "";

    }
}
