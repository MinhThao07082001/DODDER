using System;
using System.Collections.Generic;

#nullable disable

namespace Dodder.Models
{
    public partial class UpgradeService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int? Price { get; set; }
    }
}
