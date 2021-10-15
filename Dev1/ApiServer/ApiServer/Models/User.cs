using System;
using System.Collections.Generic;

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

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
