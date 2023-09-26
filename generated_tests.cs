using NUnit.Framework;

using System;
using Microsoft.AspNetCore.Mvc;

namespace PassportApplicationSystem
{
    [TestFixture]
    public class PassportApplicationControllerTest
    {
        [Test]
        public void SendToPrinter_Valid_Application_And_Valid_PrinterID_Test()
        {
            PassportApplication application = new PassportApplication()
            {
                ApplicantName = "John Smith"
            };

            string printerID = "1";

            // Create PassportApplicationController
            PassportApplicationController controller = new PassportApplicationController();

            // Call SendToPrinter
            IActionResult result = controller.SendToPrinter(application, printerID);

            // Assert that the response is Ok
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void SendToPrinter_Invalid_Application_Test()
        {
            PassportApplication application = null;

            string printerID = "1";

            // Create PassportApplicationController
            PassportApplicationController controller = new PassportApplicationController();

            // Call SendToPrinter
            IActionResult result = controller.SendToPrinter(application, printerID);

            // Assert that the response is BadRequest
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void SendToPrinter_Invalid_PrinterID_Test()
        {
            PassportApplication application = new PassportApplication()
            {
                ApplicantName = "John Smith"
            };

            string printerID = null;

            // Create PassportApplicationController
            PassportApplicationController controller = new PassportApplicationController();

            // Call SendToPrinter
            IActionResult result = controller.SendToPrinter(application, printerID);

            // Assert that the response is BadRequest
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void SendToPrinter_Missing_Application_Test()
        {
            string printerID = "1";

            // Create PassportApplicationController
            PassportApplicationController controller = new PassportApplicationController();

            // Call SendToPrinter
            IActionResult result = controller.SendToPrinter(application, printerID);

            // Assert that the response is BadRequest
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void SendToPrinter_Missing_PrinterID_Test()
        {
            PassportApplication application = new PassportApplication()
            {
                ApplicantName = "John Smith"
            };

            string printerID = null;

            // Create PassportApplicationController
            PassportApplicationController controller = new PassportApplicationController();

            // Call SendToPrinter
            IActionResult result = controller.SendToPrinter(application, printerID);

            // Assert that the response is BadRequest
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
