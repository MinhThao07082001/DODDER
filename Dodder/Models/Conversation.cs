using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class Conversation
    {
        public Conversation()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public int? UserAccountIdCreator { get; set; }
        public int? UserAccountId2 { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual UserAccount UserAccountId2Navigation { get; set; }
        public virtual UserAccount UserAccountIdCreatorNavigation { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
