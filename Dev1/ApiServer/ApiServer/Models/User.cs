using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ApiServer.Models
{
    public partial class User
    {
        public User()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore] 
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
