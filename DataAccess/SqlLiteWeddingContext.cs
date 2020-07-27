using Microsoft.EntityFrameworkCore;

namespace WeddingWebsiteCore.DataAccess
{
    public class SqlLiteWeddingContext : WeddingContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            const string sqlLiteConnection = "Data Source = wedding.db";
            options.UseSqlite(sqlLiteConnection);
            base.OnConfiguring(options);
        }
    }
}
