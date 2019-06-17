using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bot.Resources.Database
{
    class MyMemes
    {
        [Key]
        public int MemeID { get; set; }
        public string Path { get; set; }
    }
}
