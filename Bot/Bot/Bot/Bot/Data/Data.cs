using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

using Bot.Resources.Database;

using Discord;
using Discord.Commands;
using System.IO;

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

        public static int removeCoins(ulong userID, int coins, int itemID)
        {
            int ret=0;
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
                        addItem(userID, itemID);
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
        public static void addItem(ulong userID, int itemID)
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
                DBContext.SaveChangesAsync();
            }
        }
        public static bool isAvailable(ulong userID)
        {
            using (var DBContext = new SqliteDbContext())
            {
                DateTime date = readDate();
                DateTime curDate = DateTime.Today;
                MyUser current;
                string path = Directory.GetCurrentDirectory() + @"\Data\CurDate.txt";
                var alliuser = DBContext.myUser.AsEnumerable().Select(x => x.UserID);
                List<MyUser> users = new List<MyUser>();
                foreach (var user in alliuser)
                {
                    current = DBContext.myUser.Where(x => x.UserID == user).FirstOrDefault();
                    users.Add(current);
                }

                if (curDate != date)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        users[i].Loggedin = false;
                    }
                    File.WriteAllText(path, curDate.ToString(), Encoding.Default);

                }
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].UserID == userID && users[i].Loggedin == false)
                    {
                        current = users[i];
                        current.Loggedin = true;
                        DBContext.myUser.Update(current);
                        DBContext.SaveChangesAsync();
                        return true;
                    }
                    else if (users[i].UserID == userID && users[i].Loggedin)
                    {
                        return false;
                    }
                }
                return false;
                
            }

        }

        public static DateTime readDate()
        {
            DateTime date=new DateTime();
            string path = Directory.GetCurrentDirectory()+@"\Data\CurDate.txt";

            int i = 0;

            using (StreamReader sr = File.OpenText(path))
            {
                string text;
                if ((text=sr.ReadLine()) != null)
                {
                    date = DateTime.Parse(text);
                    return date;
                }
                else
                {
                    i = 1;
                }
            }
            if (i == 1)
            {
                date = DateTime.Today;
                File.WriteAllText(path, date.ToString(), Encoding.Default);
                return date;
            }
            return date;
        }
    }
}