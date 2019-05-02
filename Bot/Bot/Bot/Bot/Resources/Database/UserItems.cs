using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bot.Resources.Database
{
    public class UserItems
    {
        [Key]
        public int ID { get; set; }
        public int ItemID { get; set; }
        public ulong UserID { get; set; }
        public int Amount { get; set; }
    }
}
