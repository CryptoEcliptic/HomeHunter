using HomeHunter.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain
{
    public class Neighbourhood : BaseModel<int>
    {
        public Neighbourhood()
        {
            this.Addresses = new List<Address>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}

