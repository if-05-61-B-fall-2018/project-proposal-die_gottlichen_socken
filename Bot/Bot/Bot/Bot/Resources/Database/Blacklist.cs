using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bot.Resources.Database
{
    public class MyBlacklist
    {
        [Key]
        public int block_id { get; set; }
        public ulong UserID { get; set; }
    }
}
