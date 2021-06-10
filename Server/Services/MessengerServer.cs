using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

using Messange;
using Server.Abstract;
using Server.Models;

namespace Server
{
    public class MessengerServer : Messenger.MessengerBase
    {
        private readonly ILogger<MessengerServer> _logger;
        IDbContext _dbContext;
        IEncrypt _encrypt;
        public MessengerServer(ILogger<MessengerServer> logger, IDbContext dbContext, IEncrypt encrypt)
        {
            _logger = logger;
            _dbContext = dbContext;
            _encrypt = encrypt;
        }

        public override Task<StatusReply> Recieve(SentMessage request, ServerCallContext context)
        {
            var time = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString();
            if (request.IsEncrypted) 
                request.Text = _encrypt.Encrypt(request.Text);

            _dbContext.SaveNewMessage(new MessageModel() { Name = request.Name, Text = request.Text, Time = time, IsEncrypted = (request.IsEncrypted ? 1 : 0)});
            _dbContext.UpdateVersion(time);

            return Task.FromResult(new StatusReply
            {
                StatusCode = 0,
                Message = "ok",
                Time = time
            });
        }
        public override Task<AllMessagesReply> GetAllMessages(AllMessageRequest request, ServerCallContext context) {
            var requestTime = Int64.Parse(request.Time);

            var allMessages = _dbContext.GetAllMessages(requestTime);
            
            AllMessagesReply reply = new AllMessagesReply() {
                StatusCode = 0
            };

            foreach(var message in allMessages) {
                if (message.IsEncrypted == 1)
                    message.Text = _encrypt.Decrypt(message.Text);
                reply.UserMessages.Add(new UserMessage() { Name = message.Name, Text = message.Text, Time = message.Time });
            }
            return Task.FromResult(reply);
        }

        public override Task<VersionsReply> GetVersion(Empty request, ServerCallContext context)
        {
            VersionsModel ver = _dbContext.GetVersions();
            return Task.FromResult(new VersionsReply() { StatusCode = 0, Version = ver.Version });
        }
    }
}
