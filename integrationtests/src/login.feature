Feature: Login

Background:
    * url config.baseUrl+"/auth"

Scenario: Invalid username
    Given request {"login":"invalid-user","password":"#(config.password)"}
	When method POST
	Then status 400
    
Scenario: Invalid password
    Given request {"login":"#(config.login)","password":"invalid-password"}
	When method POST
	Then status 400
    
Scenario: Correct credentials
    Given request {"login":"#(config.login)","password":"#(config.password)"}
	When method POST
	Then status 200
    And responseHeaders['Authorization'][0] != ''