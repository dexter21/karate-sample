Feature: TODO items - unauthorized

Background:
    * def baseUrl = config.baseUrl+"/tasks"

Scenario: Get all
    Given url baseUrl 
    And request
	When method GET
	Then status 401

Scenario: Add
    Given url baseUrl 
    And request "test name"
	When method POST
	Then status 401

Scenario: Update
    Given url baseUrl + '/1' 
    And request { name: "test name" }
	When method PUT
	Then status 401
    
Scenario: Delete all
    Given url baseUrl 
    And request 1
	When method DELETE
	Then status 401