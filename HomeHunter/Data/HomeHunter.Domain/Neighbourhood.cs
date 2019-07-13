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
        [MaxLength(64)]
        public string Name { get; set; }

        public int? CityId { get; set; }
        public City City { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}

