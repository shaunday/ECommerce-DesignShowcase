using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Payment
{
    public interface IPaymentService : IService
    {
        Task<IEnumerable<Payment>> ListPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(string id);
    }

    public class Payment
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string OrderId { get; set; }
    }
} 