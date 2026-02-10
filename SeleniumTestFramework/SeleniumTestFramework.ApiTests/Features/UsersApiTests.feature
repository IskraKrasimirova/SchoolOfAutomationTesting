@UsersApi
Feature: UsersApiTests

CRUD operations for users endpoints

Scenario: Get users by id retuns the corrrect user
	Given I make a get request to users endpoint with id 1
	Then the response status code should be 200
	And users response should contain the following data:
		| Id | FirstName | Password |
		|  1 | Admin     | pass123  |


@Negative
Scenario: Get users by id retuns the corrrect error for invalid user ID
	Given I make a get request to users endpoint with id 0
	Then the response status code should be 404
	And the response should contain the following error message "User not found"


Scenario: Create user with valid data returns the created user
	Given I make a post request to users endpoint with the following data:
		| Field     | Value               |
		| Title     | Mr.                 |
		| FirstName | Ivan                |
		| SirName   | Ivanov              |
		| Country   | Bulgaria            |
		| City      | Sofia               |
		| Email     | ivan@automation.com |
		| Password  | pass123             |
		| IsAdmin   |                   0 |
	Then the response status code should be 200
	And create users response should contain the following data:
		| Field     | Value    |
		| Title     | Mr.      |
		| FirstName | Ivan     |
		| SirName   | Ivanov   |
		| Country   | Bulgaria |
		| City      | Sofia    |
		| IsAdmin   |        0 |


Scenario: Delete user by id removes the user successfully
	Given I create a new user via the API
	When I delete that user
	Then the response status code should be 200
	And the response should contain the following message "User deleted successfully"
	And I make a get request to users endpoint with that id
	And the response status code should be 404
	And the response should contain the following error message "User not found"


@Negative
Scenario Outline: Delete users by id retuns the corrrect error for non-existing user ID
	When I make a Delete request to users endpoint with id <id>
	Then the response status code should be 404
	And the response should contain the following error message "User not found"

Examples:
	| id        |
	|         0 |
	| 123456789 |

@Negative
Scenario: Delete users by id with negative value retuns error
	When I make a Delete request to users endpoint with id -1
	Then the response status code should be 404
	And the response should contain the following error message "Not Found"


Scenario: Update user by id updates the user successfully
    Given I create a new user via the API
    When I update that user with valid data
    Then the response status code should be 200
    And the updated user should have the new data
