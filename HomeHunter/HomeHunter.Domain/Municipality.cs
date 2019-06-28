﻿using HomeHunter.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Domain
{
    public class Municipality : BaseModel<int>
    {
        public Municipality()
        {
            this.Cities = new List<City>();
            this.Villages = new List<Village>();
        }

        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<Village> Villages { get; set; }
    }
}
