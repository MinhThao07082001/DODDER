using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class UserBlock
    {
        public int Id { get; set; }
        public int? UserAccountId { get; set; }
        public int? UserAccountIdBlock { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual UserAccount UserAccountIdBlockNavigation { get; set; }
    }
}
