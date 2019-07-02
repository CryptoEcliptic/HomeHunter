using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain.Enums
{
    public enum BuildingType
    {
        [Display(Name = "Панел")]
        Pannel = 1,

        [Display(Name = "ЕПК")]
        EPK = 2,

        [Display(Name = "Тухла")]
        Bricks = 3,

        [Display(Name = "Ново строителство")]
        NewBuilding = 4,

        [Display(Name = "Пълзящ куфраж")]
        PK = 5,
    }
}
