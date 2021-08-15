using LoginApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoginApi.Data.Contexts
{
    public class LoginDbContext : DbContext
    {
        #region LoginDbContext
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {
        }
        #endregion

        public DbSet<Usuarios> Usuarios { get; set; }

    }
}
