
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/contact")]

	public class ContactController : ControllerBase
	{
		[HttpPost]
		public IActionResult SendMessage([FromBody] ContactRequest request)
		{
			return Ok(new { message = "تم إرسال الرسالة بنجاح 👍" });
		}
	}

	public class ContactRequest
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Message { get; set; }
	}
