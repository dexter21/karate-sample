Feature: Parts

Background:
    * url config.baseUrl+"/tasks"

Scenario: Delete all tasks
    Given request
	When method DELETE