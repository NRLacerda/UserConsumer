using Amazon.SQS;
using UserConsumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddHostedService<SqsConsumerService>();
builder.Services.AddSingleton<CredentialsService>();
builder.Services.AddSingleton<IAmazonSQS>(sp =>
{
    var credentialsService = sp.GetRequiredService<CredentialsService>();
    var (accessKey, secretKey) = credentialsService.GetCredentials();
    Console.WriteLine(credentialsService.GetCredentials());
    return new AmazonSQSClient(accessKey, secretKey, Amazon.RegionEndpoint.USEast1);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
