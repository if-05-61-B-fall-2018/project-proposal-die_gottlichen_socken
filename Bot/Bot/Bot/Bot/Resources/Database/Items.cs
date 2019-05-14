using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bot.Resources.Database
{
    public class Items
    {
        [Key]
        public int ItemID { get; set; }
        public string IName { get; set; }
        public int Price { get; set; }
    }
}
