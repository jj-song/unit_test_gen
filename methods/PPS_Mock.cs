[HttpGet]
[Route("[action]")]
public IActionResult FetchPamphletSubmissions([FromQuery]string userLogin, [FromQuery]string siteCode)
{
    // Validate the provided site code
    var siteValidator = new SiteValidator(siteCode);
    siteValidator.PerformValidation();

    // If there are issues with validation, respond with an error
    if (siteValidator.Errors.Count > 0)
    {
        var validationErrorResponse = ConstructResponse(userLogin, siteCode, 1, siteValidator.ToString());
        return BadRequest(validationErrorResponse);
    }

    try
    {
        // Get the list of submissions that need pamphlets
        var submissionsList = submissionsDatabase.GetPendingSubmissions(siteCode);

        // If successful, return the list
        var successResponse = ConstructResponse(userLogin, siteCode, 0, "Operation successful", submissionsList);
        return Ok(successResponse);
    }
    catch (Exception error)
    {
        // Log any unexpected errors
        LogErrorDetails("Error fetching submissions. Message: ", error);

        // Respond with a server error message
        var serverErrorResponse = ConstructResponse(userLogin, siteCode, 2, error.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, serverErrorResponse);
    }
}