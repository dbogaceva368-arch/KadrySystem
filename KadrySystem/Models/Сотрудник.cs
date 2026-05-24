using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KadrySystem.Models
{
    public class Сотрудник
    {
        [Key]
        public int Код_сотрудника { get; set; }

        [Required]
        public string Фамилия { get; set; }

        [Required]
        public string Имя { get; set; }

        public string Отчество { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Дата_рождения { get; set; }

        [Required]
        public string Паспортные_данные { get; set; }

        [Required]
        public string ИНН { get; set; }

        [Required]
        public string СНИЛС { get; set; }

        public string Телефон { get; set; }

        [Required]
        public string Адрес_регистрации { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Дата_приема { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Дата_увольнения { get; set; }

        [ForeignKey("Должность")]
        public int Код_должности { get; set; }

        [ForeignKey("Подразделение")]
        public int Код_подразделения { get; set; }

        public virtual Должность Должность { get; set; }
        public virtual Подразделение Подразделение { get; set; }
    }
}
