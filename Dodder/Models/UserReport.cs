using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class UserReport
    {
        public int Id { get; set; }
        public int? UserAccountId { get; set; }
        public int? UserAccountIdReport { get; set; }
        public string Details { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual UserAccount UserAccountIdReportNavigation { get; set; }
    }
}
