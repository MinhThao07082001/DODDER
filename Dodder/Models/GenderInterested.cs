using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class GenderInterested
    {
        public int Id { get; set; }
        public int? UserAccountId { get; set; }
        public int? GenderId { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
