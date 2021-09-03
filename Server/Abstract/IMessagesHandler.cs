using System.Collections.Generic;
using Server.Models;

namespace Server.Abstract {
    public interface IMessageHandler {
        void SaveNewMessage(MessageModel message);
        IEnumerable<MessageModel> GetAllMessages(long time=0);
        VersionsModel GetVersions();
    }
}