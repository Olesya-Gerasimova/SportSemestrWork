using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore] 
        public virtual Team Team { get; set; }
    }
}
