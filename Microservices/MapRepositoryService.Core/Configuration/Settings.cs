namespace MapRepositoryService.Core.Configuration
{
    public class Settings
    {
        public string HostName { get; set; } = "";
        public string MapRepositoryTopic { get; set; } = "";
        public string EndPoint { get; set; } = "";
        public string AccessKey { get; set; } = "";
        public string SecretKey { get; set; } = "";
        public string MapBucketName { get; set; } = "";
        public string MissionMapBucketName { get; set; } = "";
    }
}
