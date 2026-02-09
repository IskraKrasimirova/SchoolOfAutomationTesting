Feature: UsersApiTest

CRUD operations for users endpoints

@Users @Api
Scenario: Get users by id retuns the corrrect user
	Given I make a get request to users endpoint with id 1
	Then the response status code should be 200
	And users response should contain the following data:
		| Id | FirstName | Password |
		|  1 | Admin     | pass123  |