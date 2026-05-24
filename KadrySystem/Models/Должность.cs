using System.ComponentModel.DataAnnotations;

namespace KadrySystem.Models
{
    public class Должность
    {
        [Key]
        public int Код_должности { get; set; }

        [Required]
        [Display(Name = "Наименование должности")]
        public string Наименование { get; set; }

        [Required]
        [Display(Name = "Оклад")]
        public decimal Оклад { get; set; }

        [Display(Name = "Квалификационные требования")]
        public string Квалификационные_требования { get; set; }
    }
}