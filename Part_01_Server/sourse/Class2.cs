using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Part_01_Server.sourse
{
    public class StockContext : DbContext
    {
        public DbSet<Ticker> Tickers { get; set; }
        public DbSet<Price> Prices { get; set; }
        // public DbSet<TodayCondition> TodaysCondition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost:3306;user=root;password=password;database=Tickers",
                new MySqlServerVersion(new Version(8, 0, 25)));
        }
    }
}
