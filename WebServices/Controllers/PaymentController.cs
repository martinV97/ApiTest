using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
namespace WebServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Create a payment
        /// </summary>
        /// <param name="payment">payment object</param>
        /// <returns>payment object with id</returns>
        [HttpPost]
        [Route("createPayment")]
        [ProducesResponseType(typeof(PaymentDTO), 200)]
        public async Task<IActionResult> CreatePayment(Payment payment)
        {
            var result = await _paymentService.CreatePayment(payment);
            return Ok(result);
        }

        /// <summary>
        /// Get a lisof payments
        /// </summary>
        /// <returns>payment list</returns>
        [HttpGet]
        [Route("getAllPayments")]
        public async Task<IActionResult> GetPayments()
        {
            var result = await _paymentService.GetAllPayments();
            return Ok(result);
        }

        /// <summary>
        /// Get payment by id
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <returns>Payment Object</returns>
        [HttpGet]
        [Route("getPaymentById")]
        public async Task<IActionResult> GetPaymentById(string id)
        {
            var result = await _paymentService.GetPaymentById(id);
            return Ok(result);
        }

        /// <summary>
        /// Update payment
        /// </summary>
        /// <param name="payment">Payment object</param>
        /// <returns>Updated payment object</returns>
        [HttpPut]
        [Route("updatePayment")]
        public async Task<IActionResult> UpdatePayment(PaymentDTO payment)
        {
            var result = await _paymentService.UpdatePaymentById(payment);
            return Ok(result);
        }

        /// <summary>
        /// Delete payment
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <returns>Deleted payment object</returns>
        [HttpDelete]
        [Route("deletePaymentById")]
        public async Task<IActionResult> DeletePaymentById(string id)
        {
            var result = await _paymentService.DeletePaymentById(id);
            return Ok(result);
        }
    }
}
