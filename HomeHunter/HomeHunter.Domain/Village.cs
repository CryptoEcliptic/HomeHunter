using HomeHunter.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHunter.Domain
{
    public class Village : BaseModel<int>
    {
        public Village()
        {
            this.Addresses = new List<Address>();
        }

        [Required]
        public string Name { get; set; }

        public int? MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
