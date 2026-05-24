using System.ComponentModel.DataAnnotations;

namespace KadrySystem.Models
{
    public class Подразделение
    {
        [Key]
        public int Код_подразделения { get; set; }

        [Required]
        [Display(Name = "Наименование подразделения")]
        public string Наименование { get; set; }

        [Display(Name = "Телефон")]
        public string Телефон { get; set; }
    }
}
