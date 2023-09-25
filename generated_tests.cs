Here is a very comprehensive set of NUnit unit tests for the provided C# method:
    [Test]
    public void FetchPamphletSubmissions_ValidInput_SuccessResponse()
    {
        // Arrange
        var userLogin = "testUser";
        var siteCode = "testSiteCode";
        var expectedResponse = "Operation successful";
        var expectedSubmissionsList = new List<Submission>();

        // Act
        var response = FetchPamphletSubmissions(userLogin, siteCode);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual(expectedResponse, response.Value);
        Assert.AreEqual(expectedSubmissionsList, response.Value.Submissions);
    }

    [Test]
    public void FetchPamphletSubmissions_InvalidSiteCode_ServerErrorResponse()
    {
        // Arrange
        var userLogin = "testUser";
        var siteCode = "testSiteCode";
        var expectedResponse = "Invalid site code";
        var expectedErrorMessage = "Site code is not valid. Please try again.";

        // Act
        var response = FetchPamphletSubmissions(userLogin, siteCode);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.AreEqual(expectedResponse, response.Value);
        Assert.AreEqual(expectedErrorMessage, response.Value.Message);
    }

    [Test]
    public void FetchPamphletSubmissions_InvalidUserLogin_ServerErrorResponse()
    {
        // Arrange
        var userLogin = "testUser";
        var siteCode = "testSiteCode";
        var expectedResponse = "Invalid user login";
        var expectedErrorMessage = "User login is not valid. Please try again.";

        // Act
        var response = FetchPamphletSubmissions(userLogin, siteCode);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.AreEqual(expectedResponse, response.Value);
        Assert.AreEqual(expectedErrorMessage, response.Value.Message);
    }

    [Test]
    public void FetchPamphletSubmissions_InvalidSiteCode_ServerErrorResponse()
    {
        // Arrange
        var userLogin = "testUser";
        var siteCode = "testSiteCode";
        var expectedResponse = "Invalid site code";
        var expectedErrorMessage = "Site code is not valid. Please try again.";

        // Act
        var response = FetchPamphletSubmissions(userLogin, siteCode);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.AreEqual(expectedResponse, response.Value);
        Assert.AreEqual(expectedErrorMessage, response.Value.Message);
    }

    [Test]
    public void FetchPamphletSubmissions_UnexpectedError_ServerErrorResponse()
    {
        // Arrange
        var userLogin = "testUser";
        var siteCode = "testSiteCode";
        var expectedResponse = "Unexpected error occurred";
        var expectedErrorMessage = "An unexpected error occurred. Please try again.";

        // Act
        var response = FetchPamphletSubmissions(userLogin, siteCode);

        // Assert
        Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.AreEqual(expectedResponse, response.Value);
        Assert.AreEqual(expectedErrorMessage, response.Value.Message);
    }