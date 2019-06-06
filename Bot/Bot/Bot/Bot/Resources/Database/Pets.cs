using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bot.Resources.Database
{
    public class Pets
    {
        [Key]
        public int PetID { get; set; }
        public string Name { get; set; }
        public int affection { get; set; }
        public int price { get; set; }
    }
}
