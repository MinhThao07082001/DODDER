using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? ConversationId { get; set; }
        public int? UserAccountIdSender { get; set; }
        public int? UserAccountIdReceiver { get; set; }
        public string Content { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual Conversation Conversation { get; set; }
        public virtual UserAccount UserAccountIdReceiverNavigation { get; set; }
        public virtual UserAccount UserAccountIdSenderNavigation { get; set; }
    }
}
