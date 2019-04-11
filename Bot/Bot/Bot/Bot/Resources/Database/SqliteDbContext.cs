using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;

namespace Bot.Resources.Database
{
    class SqliteDbContext :DbContext
    {
        public DbSet<MyUser> myUser { get; set; }
        public DbSet<UserItems> userItems { get; set; }
        public DbSet<Items> items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dblocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.0", Directory.GetCurrentDirectory() + @"\Data\");
            options.UseSqlite($"Data Source={Directory.GetCurrentDirectory()}\\Data\\Database.sqlite");
        }
    }
}