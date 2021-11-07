using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int? UserAccountId { get; set; }
        public string Content { get; set; }
        public int? NotiType { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
