Feature: Finish Events

@Add
Scenario: Healthcheck
	Given the service is initialised
	When the health endpoint is called
	Then the response should be 200