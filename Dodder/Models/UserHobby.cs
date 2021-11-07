using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class UserHobby
    {
        public int? HobbyId { get; set; }
        public int? UserAccountId { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual Hobby Hobby { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
