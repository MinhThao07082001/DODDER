using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class AuthenticationCode
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
