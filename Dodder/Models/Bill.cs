using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class Bill
    {
        public int? UpgradeServiceId { get; set; }
        public int? UserAccountId { get; set; }
        public int? ServiceMonth { get; set; }
        public int? Price { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual UpgradeService UpgradeService { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
