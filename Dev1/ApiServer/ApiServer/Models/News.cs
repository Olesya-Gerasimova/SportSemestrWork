using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
