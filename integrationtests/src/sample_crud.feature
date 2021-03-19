Feature: Not authorized 

Background:
    * def signInResponse = signIn()
    * def baseUrl = config.baseUrl + "/tasks"
    * deleteTasks()

Scenario: Get all - empty response
    Given url baseUrl 
    And request
	When method GET
	Then status 200
    And assert response.length == 0

Scenario: Add
    Given url baseUrl 
    And request { name: "test name" }
	When method POST
	Then status 201
    * def id = response
    * match id == '1'
    
    Given url baseUrl + "/" + id
    And request
	When method GET
	Then status 200
    And match $.name == "test name"

Scenario: Update
    Given url baseUrl 
    And request { name: "test name" }
	When method POST
	Then status 201
    * def id = response
    
    Given url baseUrl + '/' + id 
    And request { name: "test name 2" }
	When method PUT
	Then status 200
    
    Given url baseUrl + "/" + id
    And request
	When method GET
	Then status 200
    And match $.name == "test name 2"

Scenario: Get one
    Given url baseUrl 
    And request { name: "test name" }
	When method POST
	Then status 201
    * def id = response

    Given url baseUrl + "/" + id
    And request
	When method GET
	Then status 200
    And match $.name == "test name"

Scenario: Get all - array
    Given url baseUrl 
    And request { name: "test name" }
	When method POST

    Given url baseUrl 
    And request { name: "test name" }
	When method POST

    Given url baseUrl 
    And request
	When method GET
	Then status 200
    And assert response.length == 2
    
Scenario: Delete all
    Given url baseUrl 
    And request { name: "test name" }
	When method POST

    Given url baseUrl 
    And request { name: "test name" }
	When method POST
    
    Given url baseUrl
    And request 
    When method DELETE
    Then status 204

    Given url baseUrl 
    And request
	When method GET
	Then status 200
    And assert response.length == 0
    
Scenario: Delete one
    Given url baseUrl 
    And request { name: "test name" }
	When method POST
    * def id = response

    Given url baseUrl + "/" + id
    And request 
    When method DELETE
    Then status 204

    Given url baseUrl 
    And request
	When method GET
	Then status 200
    And assert response.length == 0