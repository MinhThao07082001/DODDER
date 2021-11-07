using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class Gender
    {
        public Gender()
        {
            GenderInteresteds = new HashSet<GenderInterested>();
            UserAccounts = new HashSet<UserAccount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GenderInterested> GenderInteresteds { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
