using System.Collections.Generic;
using Server.Models;

namespace Server.Abstract {
    public interface IMessageHandler {
        void SaveNewMessage(MessageModel message, bool isEncrypted);
        IEnumerable<MessageModel> GetAllMessages(long time=0);
        VersionsModel GetVersions();
    }
}