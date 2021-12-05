using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ApiServer.Models
{
    public partial class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? UserId { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }

        [JsonIgnore] 
        public virtual User User { get; set; }
    }
}
