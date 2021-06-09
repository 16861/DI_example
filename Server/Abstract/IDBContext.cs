using Server.Models;
using System.Collections.Generic;

namespace Server.Abstract {
    public interface IDbContext {
        IEnumerable<MessageModel> GetAllMessages(long time=0);
        void SaveNewMessage(MessageModel message);
        VersionsModel GetVersions();
        void UpdateVersion(string time);
    }
}