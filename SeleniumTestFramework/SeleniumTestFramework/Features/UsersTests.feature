Feature: UsersTests

The system should allow the administrator to manage user accounts, including creating, listing and deleting users. A common user should be able to view the list of users but not create or delete them.

@Users
Scenario: A user can register a new account successfully and the administrator can see the new user in the users list and delete it
	Given a new user can register with valid data successfully
	And the user can see the dashboard with its data
	And the user should be able to logout successfully

	When the administrator logs in with valid credentials
	And navigates to the users page
	Then the new user should be present in the users list

	When the administrator deletes the created user
	Then the user should no longer be present in the users list

	When the administrator logs out successfully 
	And I navigate to the main page 
	And I verify that the login form is displayed

	When I login with the deleted user's credentials
	Then I should still be on the login page
	And I should see an error message with the following text "Invalid email or password"
