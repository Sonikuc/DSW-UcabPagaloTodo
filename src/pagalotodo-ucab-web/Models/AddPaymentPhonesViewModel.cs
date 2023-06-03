using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoWeb.Models
{
    public class AddPaymentPhonesViewModel
    {
        public Guid ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public Guid UserId { get; set; }
        public List<PaymentOptionsByServiceIdResponse>? PaymentOption { get; set; }
        public string? PhoneNumber { get; set; }
        public double? Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
