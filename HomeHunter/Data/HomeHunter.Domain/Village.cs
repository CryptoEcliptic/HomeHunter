using HomeHunter.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain
{
    public class Village : BaseModel<int>
    {
        public Village()
        {
            this.Addresses = new List<Address>();
        }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
