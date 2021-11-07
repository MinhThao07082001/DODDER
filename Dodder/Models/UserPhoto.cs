using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class UserPhoto
    {
        public int Id { get; set; }
        public int? UserAccountId { get; set; }
        public string Link { get; set; }
        public string Detail { get; set; }
        public int? PhotoType { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
