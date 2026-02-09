Feature: UsersApiTests

CRUD operations for users endpoints

@Users @Api
Scenario: Get users by id retuns the corrrect user
	Given I make a get request to users endpoint with id 1
	Then the response status code should be 200
	And users response should contain the following data:
		| Id | FirstName | Password |
		|  1 | Admin     | pass123  |

@Users @Api
Scenario: Get users by id retuns the corrrect error for invalid user ID
	Given I make a get request to users endpoint with id 0
	Then the response status code should be 404
	And the response should contain the following error message "User not found"

@Users @Api
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
