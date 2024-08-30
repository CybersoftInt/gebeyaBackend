// Controllers/PaymentController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public PaymentController(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    [HttpPost("accept-payment")]
    public async Task<IActionResult> AcceptPayment([FromBody] PaymentRequest request)
    {
        var secretKey = _configuration["ChapaSettings:SecretKey"];
        var url = "https://api.chapa.co/v1/transaction/initialize";

        var paymentData = new
        {
            amount = request.Amount,
            currency = request.Currency,
            email = request.Email,
            first_name = request.FirstName,
            last_name = request.LastName,
            phone_number = request.PhoneNumber,
            tx_ref = request.TxRef,
            return_url = "http://localhost:3000/" // Change to your front-end URL
        };

        var header = new HttpRequestMessage
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", secretKey) },
            Content = new StringContent(JsonConvert.SerializeObject(paymentData), System.Text.Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.PostAsync(url, header.Content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
            return Ok(responseData);
        }

        return BadRequest("Payment initialization failed.");
    }
}

public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string TxRef { get; set; }
}
