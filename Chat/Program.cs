using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Messange;

namespace GrpcMessangerChat
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Starting chat...");
            
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Messenger.MessengerClient(channel);


            long lastVersionTime = 0;

            while (true)
            {
                long newVersionTime = 0;
                var version = await client.GetVersionAsync(new Empty());
                newVersionTime = Int64.Parse(version.Version);
                if (newVersionTime > lastVersionTime)
                {
                    var responseAllMessage = await client.GetAllMessagesAsync(new AllMessageRequest() { Time = lastVersionTime.ToString() });
                    StringBuilder builder = new StringBuilder();
                    foreach (var mes in responseAllMessage.UserMessages)
                        builder.AppendLine($"Name: {mes.Name}, text: {mes.Text}, time: {mes.Time}");

                    System.Console.Write(builder.ToString());

                    lastVersionTime = newVersionTime;
                }

                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
