using HomeHunter.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Domain
{
    public class Village : BaseModel<int>
    {
        public string Name { get; set; }

        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }
    }
}
