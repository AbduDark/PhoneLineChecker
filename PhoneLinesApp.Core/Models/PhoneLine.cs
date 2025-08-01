using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneLinesApp.Core.Models
{
    public class PhoneLine
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "رقم الخط مطلوب")]
        [Phone(ErrorMessage = "صيغة رقم غير صحيحة")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "نوع الخط مطلوب")]
        public string LineType { get; set; }

        [Required]
        public DateTime LastActivationDate { get; set; }

        public bool IsActive { get; set; }

        [StringLength(500, ErrorMessage = "الملاحظات طويلة جداً")]
        public string Notes { get; set; }
    }
}
