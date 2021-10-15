using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NewsId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
    }
}
