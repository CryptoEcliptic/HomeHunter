using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.ViewModels.Offer
{
    public class OfferIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Референтен номер")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "Тип на обявата")]
        public string OfferType { get; set; }

        [Display(Name = "Дата на публикуване")]
        public string CreatedOn { get; set; }

        [Display(Name = "Последна промяна")]
        public string ModifiedOn { get; set; }

        [Display(Name = "Тип на имота")]
        public string RealEstateType { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Квартал")]
        public string Neighbourhood { get; set; }
    }
}
