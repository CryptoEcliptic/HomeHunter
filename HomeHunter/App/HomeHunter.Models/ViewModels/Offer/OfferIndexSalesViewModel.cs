﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeHunter.Models.ViewModels.Offer
{
    public class OfferIndexSalesViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Референтен номер")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "Тип на имота")]
        public string RealEstateType { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Цена €")]
        public decimal Price { get; set; }

        [Display(Name = "Квартал")]
        public string Neighbourhood { get; set; }

        [Display(Name = "Площ кв. м.")]
        public double Area { get; set; }

        [Display(Name = "Тип сграда")]
        public string BuildingType { get; set; }

        [Display(Name = "Снимки")]
        public List<string> Images { get; set; }

        [Display(Name = "Коментари")]
        public string Comments { get; set; }

        [Display(Name = "Дата на публикуване")]
        public string CreatedOn { get; set; }
    }
}