using Azure.Messaging.ServiceBus;
using MessagePublisher;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;

namespace MessagePulisher // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                GetConfigValue();
                await StartProcess(args);
            }
            catch (Exception ex)
            {
            }
        }

        async private static Task StartProcess(string[] args)
        {
            var message = new MessageInfo
                (
                Guid.NewGuid().ToString(),
                "Message 1",
                "Message deails of 1"
                );

            var _sbconnectionstring = "Endpoint=sb://milestoneservice.servicebus.windows.net/;SharedAccessKeyName=MessageHolder;SharedAccessKey=k7Zi0UrJh6T6N7AJB20fx8OhzhjsKscBYwduMVfyGVQ=;EntityPath=engagment1";
            string _sbtopicName = "engagment1";

            try
            {
                var client = new ServiceBusClient(_sbconnectionstring);
                var sender = client.CreateSender(_sbtopicName);
                var body = JsonSerializer.Serialize(message);
                var _sbMessage = new ServiceBusMessage(body);   
                await sender.SendMessageAsync(_sbMessage);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private static string GetConfigValue()
        {
            IConfiguration config = new ConfigurationBuilder()
                                     .SetBasePath(Directory.GetCurrentDirectory())
                                     .AddJsonFile("appsetting.json")
                                     .AddEnvironmentVariables()
                                     .Build();

            var schdulerMinues = config.GetSection("Scheduler").GetChildren().FirstOrDefault(x => x.Key == "Minutes").Value;

            return schdulerMinues;

        }
    }
}