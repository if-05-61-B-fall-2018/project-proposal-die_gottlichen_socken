﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

using Bot.Resources.Database;

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
                        await addItem(userID, itemID);
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
                if (DBContext.userItems.Where(x => (x.UserID == userID)&&(x.ItemID==itemID) ).Count() < 1)
                {
                    int maxValue=0;
                    if (DBContext.userItems.Count() < 1) maxValue = 0;
                    else  maxValue = DBContext.userItems.Max(x => x.ID);
                    
                    int id = maxValue += 1;
                    DBContext.userItems.Add(new UserItems
                    {
                        ID=id,
                        UserID = userID,
                        ItemID = itemID,
                        Amount = 1
                    });
                }
                else
                {
                    int i=DBContext.userItems.Where(x => x.UserID == userID).Select(x => x.Amount).FirstOrDefault();
                    i += 1;
                    UserItems current = DBContext.userItems.Where(x => x.UserID == userID).FirstOrDefault();
                    current.Amount = i;
                    DBContext.userItems.Update(current);

                }
                await DBContext.SaveChangesAsync();
            }
        }

        public static int removeUserfromBlacklist(ulong userID)
        {
            using(var DBContext = new SqliteDbContext())
            {
                if(DBContext.myblacklist.Where(x => (userID == x.UserID)).Count() == 1)
                {
                    DBContext.myblacklist.Remove(DBContext.myblacklist.Where(x=>x.UserID==userID).FirstOrDefault());
                    DBContext.SaveChangesAsync();
                    return 0;
                }
                return 1;
            }
        } 

        public static int addUserToBlacklist(ulong userID)
        {
            using (var DBContext = new SqliteDbContext())
            {
                if (DBContext.myblacklist.Where(x => (userID == x.UserID)).Count() == 0)
                {
                    int maxValue = 0;
                    if (DBContext.userItems.Count() < 1) maxValue = 0;
                    else maxValue = DBContext.myblacklist.Max(x => x.block_id);
                    DBContext.myblacklist.Add(new MyBlacklist
                    {
                        block_id = maxValue,
                        UserID = userID
                    });
                    DBContext.SaveChangesAsync();
                    return 0;
                }
                return 1;
            }
        }

        public static int getBlacklist(ulong userID)
        {
            using (var DBContext = new SqliteDbContext())
            {
                int i= DBContext.myblacklist.Where(x => (userID == x.UserID)).Count();
                return i;
            }
        }

        public static int realRemoveCoins(ulong userID, int coins)
        {
            int ret = 0;
            using (var DBContext = new SqliteDbContext())
            {
                if (DBContext.myUser.Where(x => x.UserID == userID).Count() < 1)
                {
                    DBContext.myUser.Add(new MyUser
                    {
                        UserID = userID,
                        Coins = 0
                    });
                    ret = 1;
                }
                else
                {
                    MyUser current = DBContext.myUser.Where(x => x.UserID == userID).FirstOrDefault();
                    if (current.Coins >= coins)
                    {
                        current.Coins -= coins;
                        DBContext.myUser.Update(current);
                        ret = 0;
                    }
                    else
                    {
                        ret = 1;
                    }

                }
                DBContext.SaveChangesAsync();
                return ret;
            }


        }
    }
}
