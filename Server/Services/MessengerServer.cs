using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

using Messange;
using Server.Abstract;
using Server.Models;
using Server.Mappers;

namespace Server
{
    public class MessengerServer : Messenger.MessengerBase
    {
        readonly IMessageHandler _messageHandler;
        public MessengerServer(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public override Task<StatusReply> Recieve(SentMessage request, ServerCallContext context)
        {
            _messageHandler.SaveNewMessage(GenericMapper.Map<SentMessage, MessageModel>(request), request.IsEncrypted);

            return Task.FromResult(new StatusReply
            {
                StatusCode = 0,
                Message = "ok"
            });
        }
        public override Task<AllMessagesReply> GetAllMessages(AllMessageRequest request, ServerCallContext context) {
            var requestTime = Int64.Parse(request.Time); //TODO check that parsing done withot errors
            
            AllMessagesReply reply = new AllMessagesReply() {
                StatusCode = 0
            };

            foreach(var message in _messageHandler.GetAllMessages(requestTime).ToList()) {
                reply.UserMessages.Add(GenericMapper.Map<MessageModel, UserMessage>(message));
            }

            return Task.FromResult(reply);
        }

        public override Task<VersionsReply> GetVersion(Empty request, ServerCallContext context)
        {
            VersionsModel ver = _messageHandler.GetVersions();
            return Task.FromResult(new VersionsReply() { StatusCode = 0, Version = ver.Version });
        }
    }
}
