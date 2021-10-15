using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
