using System;
using System.Collections.Generic;
using System.Text;
using Bot.Resources.Database;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace Bot.Data
{
    public static class Data
    {
        public static int getCoins(ulong userID)
        {
            using(var DBContext = new SqliteDbContext())
            {
                if(DBContext.myUser.Where(x=> x.UserID == userID).Count() < 1)
                {
                    return 0;
                }
                return DBContext.myUser.Where(x => x.UserID == userID).Select(x => x.Coins).FirstOrDefault();
            }
        }

        public static async Task addCoins(ulong userID, int coins)
        {
            using(var DBContext = new SqliteDbContext())
            {
                if(DBContext.myUser.Where(x=> x.UserID == userID).Count() < 1)
                {
                    DBContext.myUser.Add(new MyUser
                    {
                        UserID = userID,
                        Coins = coins
                    });
                }
                else
                {
                    MyUser current = DBContext.myUser.Where(x => x.UserID == userID).FirstOrDefault();
                    current.Coins += coins;
                    DBContext.myUser.Update(current);
                }
                await DBContext.SaveChangesAsync();
            }
        }

        public static async Task removeCoins(ulong userID, int coins, int itemID)
        {
            using (var DBContext = new SqliteDbContext())
            {
                if (DBContext.myUser.Where(x => x.UserID == userID).Count() < 1)
                {
                    DBContext.myUser.Add(new MyUser
                    {
                        UserID = userID,
                        Coins = 100
                    });
                }
                else
                {
                    MyUser current = DBContext.myUser.Where(x => x.UserID == userID).FirstOrDefault();
                    if (current.Coins >= coins)
                    {
                        current.Coins -= coins;
                        addItem(userID, itemID);
                        DBContext.myUser.Update(current);
                    }
                    else
                    {
                        
                    }
                    
                }
                await DBContext.SaveChangesAsync();
              
            }

           
        }
        public static async Task addItem(ulong userID, int itemID)
        {
            using (var DBContext = new SqliteDbContext())
            {
                DBContext.userItems.Add(new UserItems
                {
                    UserID = userID,
                    ItemID = itemID
                });
            }
                
        }
    }
}
