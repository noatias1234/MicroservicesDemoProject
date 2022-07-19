using Microsoft.AspNetCore.Http;

namespace MapRepositoryService.Core.Model
{
    public class UploadMapDto
    {
        public string? MapName { get; set; }
        public IFormFile? MapFile { get; set; }
    }
}
