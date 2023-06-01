using Microsoft.EntityFrameworkCore;
using PesaPap.Entities;

namespace PesaPap.Data
{
    public class PaymentsDbContext : DbContext
    {
        public PaymentsDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<IPNSettings> IPNSettings { get; set; }

        public DbSet<PaymentChannels> PaymentChannels { get; set; }

        public DbSet<PaymentNotifications> PaymentNotifications { get; set; }

        public DbSet<UserLogin> Users { get; set; }


    }
}
