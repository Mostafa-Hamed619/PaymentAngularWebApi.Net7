using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;
using PaymentApi.Models.Data;
using PaymentApi.Models.Dto;
using PaymentApi.Services;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices payRepo;
        private readonly AppDbContext db;

        public PaymentController(IPaymentServices payRepo,AppDbContext db)
        {
            this.payRepo = payRepo;
            this.db = db;
        }

        [HttpGet("GetAll",Name ="GetAllPayments")]
        
        public async Task<ActionResult<ApiResponse>> GetAll()
        {
            var results = await payRepo.GetAllAsync();
            ApiResponse response = new ApiResponse();
            if(results == null)
            {
                response.Message = "No payment exists";
                return BadRequest(response);
            }
            response.result = results;
            
            return Ok(response.result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get(int id)
        {
            ApiResponse response = new ApiResponse();
            response.result = await payRepo.GetAsync(id);
            if(response.result != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response.Message = "payment not exists");

            }
        }
        [HttpPost("addPayment")]
        public async Task<IActionResult> Addpayment([FromBody] AddNewPayments model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                PaymentDetails paymentDetails = new PaymentDetails
                {
                    cardOwnerName = model.cardOwnerName,
                    cardSecretNumber = model.cardSecretNumber,
                    cardNumber = model.cardNumber,
                    expiredDate = model.expiredDate
                };

                var Addition = await payRepo.AddAsync(paymentDetails);
                var response = new ApiResponse { result = Addition, Message = "Addition Successful" };
                return Ok(await payRepo.GetAllAsync());
            }
        }

        [HttpDelete("{id}",Name ="DeletePayment")]
        //[Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            //var payment = await payRepo.GetAsync(id);
            ApiResponse response = new ApiResponse(); 

         
            var deletePayment = await payRepo.GetAsync(id);
            
            if (deletePayment != null)
            {
                await payRepo.DeleteAsync(id);
                response.Message = "Deletion successful";
                return Ok(response);
            }
            else
            {
                response.Message = $"No payment exists with id {id}";
                return NotFound(response.Message);
            }

        }

        [HttpPut("{id}",Name ="UpdatePayment")]
        //[Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,UpdatePayment model)
        {
            var payment = await payRepo.GetAsync(id);
            ApiResponse response = new ApiResponse();
            if (payment != null)
            {
                await payRepo.UpdateAsync(id, payment);
                response.Message = "Updation is fulfilled";
                return Ok(response);
            }
            else
            {
                response.Message = $"payment id {id} is Not exists";
                return NotFound(response.Message);
            }
        }
    }
}
