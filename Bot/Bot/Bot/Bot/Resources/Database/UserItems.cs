using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bot.Resources.Database
{
    public class UserItems
    {
        
        public int ItemID { get; set; }
        [Key]
        public ulong UserID { get; set; }
    }
}
