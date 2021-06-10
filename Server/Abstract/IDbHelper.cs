using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Server.Abstract
{
    public interface IDbHelper
    {
        IDbConnection GetDbConnection();
    }
}