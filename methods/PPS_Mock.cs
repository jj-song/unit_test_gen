		/// <summary>
        /// Gets a list of all submissions needing pamphlets
        /// </summary>
        /// <param name="loginInfo">The requesting user</param>
        /// <param name="originArea">The requesting site code</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult NeedingPamphlet([FromQuery]string loginInfo, [FromQuery]string originArea)
        {
            submissionNeedingPamphletValidator validator =
                new submissionNeedingPamphletValidator(originArea);

            validator.Validate();

            if (validator.ValidationErrors.Count > 0)
                return BadRequest(ModelHelper.CreateSubmissionNeedingPamphletResponseObject(loginInfo,
                    originArea, 1, validator.ToString()));

            try
            {
                var pending = submissionTable.GetsubmissionsNeedingPamphlet(originArea);

                return Ok(ModelHelper.CreateSubmissionNeedingPamphletResponseObject(loginInfo, originArea, 0,
                    "Success", pending));
            }
            catch (Exception ex)
            {
                Logger.Debug($"Submission had an error with loading mechanism. Get x3. Error: {ex.Message}");

                // Log Error to Error Log Table
                NLog.MappedDiagnosticsContext.Set("StatusCode", 500);
                Logger.Error(ex, "Status Code: 500");

                return StatusCode(StatusCodes.Status500InternalServerError,
                   ModelHelper.CreateSubmissionNeedingPamphletResponseObject(loginInfo, originArea,
                   2, ex.Message));
            }
        }