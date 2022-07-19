using Microsoft.Extensions.Options;
using Minio.AspNetCore;

namespace MapRepositoryService.Infrastructure.MinIo;
internal class MinioClient 
{
    private readonly string _endpoint;
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly string _region;
    private readonly string _sessionToken;

    public MinioClient(string endpoint, string accessKey, string secretKey, string region,
        string sessionToken)
    {
        _endpoint = endpoint;
        _accessKey = accessKey;
        _secretKey = secretKey;
        _region = region;
        _sessionToken = sessionToken;
    }



    public void CreateClient()
    {
        var client = new MinioClient()
            .WithEndpoint(_endpoint)
            .WithCredentials(_accessKey, _secretKey)
            .WithSessionToken(_sessionToken);
    }

}
