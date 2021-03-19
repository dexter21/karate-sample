Feature: Parts

Background:
    * url config.baseUrl+"/auth"

Scenario: Login
    Given request {"login":"#(config.login)","password":"#(config.password)"}
	When method POST
	Then status 200
    And responseHeaders['Authorization'][0] != ''