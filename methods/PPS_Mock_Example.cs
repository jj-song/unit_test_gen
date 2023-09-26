using System;
using Microsoft.AspNetCore.Mvc;

namespace PassportApplicationSystem
{
    [ApiController]
    [Route("[controller]")]
    public class PassportApplicationController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult SendToPrinter([FromBody] PassportApplication application, [FromQuery] string printerID)
        {
            // Validate the provided application
            if (application == null || string.IsNullOrEmpty(printerID))
            {
                var validationErrorResponse = ConstructResponse("Invalid request. Missing application details or printer ID.");
                return BadRequest(validationErrorResponse);
            }

            try
            {
                // Simulate sending the application to the printer.
                Console.WriteLine($"Sending application of {application.ApplicantName} to printer with ID {printerID}.");

                // If successful, return a success message
                var successResponse = ConstructResponse("Application sent to printer successfully.");
                return Ok(successResponse);
            }
            catch (Exception error)
            {
                // Log any unexpected errors
                Console.WriteLine($"Error sending application to printer. Message: {error.Message}");

                // Respond with a server error message
                var serverErrorResponse = ConstructResponse($"Error sending application to printer. Message: {error.Message}");
                return StatusCode(500, serverErrorResponse);
            }
        }
}
