using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class RelationInterested
    {
        public int Id { get; set; }
        public int? UserAccountId { get; set; }
        public int? RelationId { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual Relation Relation { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
