using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bot.Resources.Database
{
    public class UserPets
    {
        [Key]
        public ulong UserID { get; set; }
        public int PetID { get; set; }
        public string PetName { get; set; }
        public int affection { get; set; }
    }
}
