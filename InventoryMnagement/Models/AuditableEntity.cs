using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMnagement.Models
{
    public class AuditableEntity
    {
        [StringLength(80)]
        public string CreatedUser { get; set; }

        [StringLength(80)]
        public string ModifiedUser { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime ModifiedTime { get; set; }
    }
}
