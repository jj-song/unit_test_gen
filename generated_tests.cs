using NUnit.Framework;
using System;
using Microsoft.AspNetCore.Mvc;

namespace PassportApplicationSystem
{
    [TestFixture]
    public class PassportApplicationControllerTests
    {
        [Test]
        public void SendToPrinter_Valid_Application_And_PrinterID_Sends_To_Printer_Successfully()
        {
            // Arrange
            var application = new
            {
                ApplicantName = "Timothy Dalton",
                Country = "United Kingdom",
                DateOfBirth = new DateTime(1980, 4, 15),
                DocumentNumber = "A1234567",
                DocumentType = "Passport",
                ValidFrom = new DateTime(2019, 5, 1),
                ValidTo = new DateTime(2021, 5, 1)
            };
            var printerID = "P1";

            // Act
            var result = new PassportApplicationController()
               .SendToPrinter(application, printerID);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var ok = (OkObjectResult)result;
            Assert.IsNotNull(ok.Value);
            Assert.IsTrue(ok.Value.ToString().Contains("Application sent to printer successfully."));
        }

        [Test]
        public void SendToPrinter_Invalid_Application_Or_PrinterID_Sends_To_Printer_InvalidRequest()
        {
            // Arrange
            var application = new { };
            var printerID = "P1";

            // Act
            var result = new PassportApplicationController()
               .SendToPrinter(application, printerID);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequest = (BadRequestObjectResult)result;
            Assert.IsNotNull(badRequest.Value);
            Assert.IsNotNull(badRequest.Value.ToString());
            Assert.IsTrue(badRequest.Value.ToString().Contains("Invalid request. Missing application details or printer ID."));
        }
    }
}
