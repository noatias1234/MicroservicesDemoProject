using Minio;

namespace MapRepositoryService.Infrastructure.Minio;

public interface IMinIoClientBuilder
{
    MinioClient Build(string bucketName);
}
