using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Models.Dto
{
    public class UpdatePayment
    {
        [Required, MaxLength(50)]
        public string cardOwnerName { get; set; } = "";

        [Required, MaxLength(16)]
        public string cardNumber { get; set; } = "";

        [Required, MaxLength(4)]
        public string cardSecretNumber { get; set; } = "";

        [Required, MaxLength(15)]
        public string expiredDate { get; set; } = "";
    }
}
