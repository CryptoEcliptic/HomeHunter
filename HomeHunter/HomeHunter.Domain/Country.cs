using HomeHunter.Domain.Common;
using System.Collections.Generic;

namespace HomeHunter.Domain
{
    public class Country : BaseModel<int>
    {
        public Country()
        {
            this.Municipalities = new List<Municipality>();
        }

        public string Name { get; set; }

        public ICollection<Municipality> Municipalities { get; set; }

    }
}
