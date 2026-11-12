using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StayFlow.Services.Features.Hotels.Commands
{
    public class UpdateHotelRequest
    {
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        [MaxLength(200)]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Miasto jest wymagane.")]
        [MaxLength(100)]
        public string City { get; set; } = "";

        [Required(ErrorMessage = "Adres jest wymagany.")]
        [MaxLength(300)]
        public string Address { get; set; } = "";

        [MaxLength(30)]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Niepoprawny adres e-mail.")]
        [MaxLength(254)]
        public string? Email { get; set; }

        public TimeOnly CheckInTime { get; set; } = new TimeOnly(15, 0);
    }
}
