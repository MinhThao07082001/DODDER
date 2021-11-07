using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class Relation
    {
        public Relation()
        {
            RelationInteresteds = new HashSet<RelationInterested>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RelationInterested> RelationInteresteds { get; set; }
    }
}
