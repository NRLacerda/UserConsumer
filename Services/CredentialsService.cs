using Amazon.Internal;

public class CredentialsService
{
    private readonly IConfiguration _configuration;

    public CredentialsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetQueueName()
    {
        string queueName = _configuration["AWS:QueueName"];
        return queueName;
    }
    public (string accessKey, string secretKey) GetCredentials()
    {
        string accessKey = _configuration["AWS:AccessKey"];
        string secretKey = _configuration["AWS:SecretKey"];
        return (accessKey, secretKey);
    }
}
