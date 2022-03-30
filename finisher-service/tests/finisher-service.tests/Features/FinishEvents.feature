Feature: Finish Events

@Healthcheck
Scenario: Healthcheck
	Given the service is initialised
	When the health endpoint is called
	Then the response should be 200

@FinishEvent
Scenario: A runner crosses the finish line
	Given the service is initialised
	When a FinishEvent is posted to the finsih endpoint
	Then the finish is persisted
