# InpiraProject

## Technical Assumptions for quick solution delivery of this challenge:
<ul>
	<li>API request authentication is not implemented due to time constraints</li>
	<li>Form and Submission Catalogue data is submitted before request received by this service</li>
	<li>Form and Submission catalogues are populated before the request is made to this service</li>
	<li>The userId and submissionId are saved in the database before the request is made to this service.</li>
	<li>The userId, submissionId and a correlationId are provided in the HttpContext Items</li>
	<li>SSNInternalCheck SOAP request/response structures are mocked using an internal service</li>
	<li>Dependency Validator (in groovy) is not implemented in this solution</li>
	<li>A middleware to be used to provide HttpContext validation of the request</li>
	<li>We assume the same groovy script HTTP response format for all statuscodes</li>
	<li>Provided Unit and Integration tests are to be used to demonstrate service outputs</li>
	<li>Minimal Unit & Integration tests due to time constraints</li>
	<li>Logging and Exception handling are done in simple form due to time constraints</li>
	<li>Default ApiController ValidationProblemDetails is returned when request model is INVALID</li>
	<li>API versioning is not applied</li>
	<li>No Database login setup</li>
</ul>

## Security Considerations
<ul>
	<li>Authorize User request to this microservice by validating their token against the appropriate IdentityServer by implementing .Net Oauth2.0 Authorization & Authorization services</li>
	<li>Furthermore on inbound request, It's important that we can validate the submission request by comparing request's userId and SubmissionId against the Submissions Table.</li>
	<li>Regarding IdentityServer client authorization setup, it's important to understand client/user access policies specific to the scoped permissions, as these can be implemented in code to further narrow user access</li>
	<li>Apply correct Authorize attribute to controllers to restrict access to request</li>
	<li>User sensetive data to be stored in the Identity claims</li>
	<li>Sanitize request body/form-data and making sure no malicious data are being stored or passed to there services</li> 
	<li>Apply correct request routing, controller/action filters and request model validation</li>
	<li>Implementing a well designed logging and exception handling to improve cyber attack detection</li>
	<li>Having a solid application and network monitoring system in place helps to detect cyber attacks and vulnerabilities</li> 
	<li>Use encryption for added sensitive data protection</li>
	<li>Setup correct CORS policy to help protect against CSRF attacks</li>
	<li>Protect storing Auth related security keys/secrets using Azure Keyvault, also manage secrets in local-dev IDE correctly</li>
	<li>Having excellent understanding of what sensitive data is within the organization, making note of those and keeping up to date</li>
	<li>Avoid logging sensitive data or else apply correct masking</li>
	<li>If possible, have an understanding of the client-side Application where the request is coming from and be mindful of the vulnerabilities</li>
	<li>In addition production environment, it's also important having secure DevOps environment setup and following DevOps best practices</li>
	<li>being up to date with new cyber-attacks and OWASP recommendations</li>
</ul>

## Testing: you can run all tests from SubmissionsProcessor.API.Tests project
## To complete this solution it requires more time and once I do add more Unit and Integration Tests then below setup instructions can be used before running tests:

<ol>	
	<li>Setup MongoDB </li>
		<li> Create Db as "Avoka"</li>
		<li> Create below catalogues:</li>
			<ul>
				<li>Submission</li>
				<li>SubmissionProperty - import SeedSubmissionProperties.json file to seed data</li>
				<li>Form</li>
			</ul>
		<li>Setup Mongodb Configs in Tests project appsettings</li>
		<li>Sample config:</li>
<code>
"AvokaDatabase": {
	"ConnectionString": "mongodb://localhost:27017",
	"DatabaseName": "Avoka"
}
</code>
	<li>Run tests and check tests results</li>
</ol>

In place of some above the assumptions I have put down some TODO comments in the code.

Please contact me for any questions or concerns.

Ehsan :)