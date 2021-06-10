using System;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Messange;

namespace GrpcMessangerClient
{
    class Program
    {
        static bool isEncrypted = false;

        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Starting client...");
            if (args.Length > 0) {
                System.Console.WriteLine($"Got console argument{args[0]}");
                if (args[0].Contains("encryptedMessages")) {
                    isEncrypted = true;
                    System.Console.WriteLine("messages will be encrypted");
                }
            }

            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Messenger.MessengerClient(channel);

            string name, message;
            Console.Write("Your name: ");

            name = Console.ReadLine();
            while (true)
            {
                Console.Write("Your message: ");
                message = Console.ReadLine();
                if(message == "")
                    break;

                var response = await client.RecieveAsync(new SentMessage()
                {
                    Name = name,
                    Text = message,
                    IsEncrypted = isEncrypted
                });
                if (response.StatusCode != 0)
                    Console.WriteLine("Error occured");
            }

            System.Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
