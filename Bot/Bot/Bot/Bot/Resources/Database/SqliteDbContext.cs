using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bot.Resources.Database
{
    class SqliteDbContext :DbContext
    {
        public DbSet<MyUser> myUser { get; set; }
        public DbSet<UserItems> userItems { get; set; }
        public DbSet<Items> items { get; set; }
        public DbSet<MyBlacklist> myblacklist { get; set; }
        public DbSet<UserPets> userPets { get; set; }
        public DbSet<Pets> pets { get; set; }
        public DbSet<MyMemes> memes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dblocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.0",@"Data\");
            options.UseSqlite($"Data Source=Data\\Database.sqlite");
        }
    }
}
    