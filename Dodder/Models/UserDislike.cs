using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class UserDislike
    {
        public int Id { get; set; }
        public int? UserAccountId { get; set; }
        public int? UserAccountIdDislike { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual UserAccount UserAccountIdDislikeNavigation { get; set; }
    }
}
