using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KadrySystem.Models
{
    public class ШтатноеРасписание
    {
        [Key]
        public int Код_позиции { get; set; }

        [ForeignKey("Должность")]
        public int Код_должности { get; set; }

        [Required]
        public int Количество_штатных_единиц { get; set; }

        [Required]
        public int Количество_занятых_ставок { get; set; }

        public virtual Должность Должность { get; set; }
    }
}
