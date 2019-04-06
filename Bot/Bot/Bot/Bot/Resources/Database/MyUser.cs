using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bot.Resources.Database
{

    public class MyUser
    {
        [Key]
        public ulong UserID { get; set; }
        public int Coins{get; set;}

    }
}
