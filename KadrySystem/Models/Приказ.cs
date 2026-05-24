using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KadrySystem.Models
{
    public class Приказ
    {
        [Key]
        public int Код_приказа { get; set; }

        [Required]
        public string Номер_приказа { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Дата_издания { get; set; }

        [Required]
        public string Тип_приказа { get; set; }

        [Required]
        public string Основание { get; set; }

        [ForeignKey("Сотрудник")]
        public int Код_сотрудника { get; set; }

        public virtual Сотрудник Сотрудник { get; set; }
    }
}