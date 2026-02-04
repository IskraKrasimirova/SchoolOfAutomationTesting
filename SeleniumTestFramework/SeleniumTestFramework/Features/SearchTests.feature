Feature: SearchTests

Validates that a user can search by country/city. The search results should only include users matching the search criteria and should display their skills. Validates that database‑created users with skills appear correctly in UI search.

Background:
	Given I navigate to the main page
	And I verify that the login form is displayed
	And I login with admin credentials
	And I navigate to the search page

@Users @DB @Search
Scenario: User created directly in the database with skills appears in UI search
	Given a user exists in the database with:
		| firstName | surname | email                | country  | city  | title | password     | isAdmin |
		| Ivan      | Petrov  | ivan.petrov@test.com | Bulgaria | Sofia | Mr.   | Password123! | false   |
	And the user has the following skills:
		| skillName         | competence |
		| Java              |          3 |
		| Automated Testing |          5 |
	When I search for users with skill "Java"
	Then all results should contain skill "Java"
	And I should see the created user in the search results

@DB @Search @Ignore
# The scenario is ignored because the country dropdown has fixed values and a new country cannot be added even it exists in database
Scenario: Country and city created directly in the database appear in the Search page
	Given A country exists in the database with name "Egypt"
	And A city exists in the database with name "Cairo" in country "Egypt"
	When I refresh the search page
	And I open the country dropdown
	Then I should see "Egypt" in the country dropdown
	And I should see "Cairo" in the city dropdown
	
@DB @Search
Scenario: City created directly in the database appears in the Search page
	Given A city exists in the database with name "Lyon" in country "France"
	When I refresh the search page
	And I open the country dropdown
	And I select country "France"
	Then I should see "Lyon" in the city dropdown

@Search
Scenario: Search by avilable country shows only users from that country
	When I select country "Bulgaria" for search
	Then all results should contain country "Bulgaria"

@Search
Scenario: Search by multiple cities returns only users from those cities
	When I select country "Bulgaria"
	And I select city "Varna" for search
	And I select city "Plovdiv" for search
	And I perform the search
	Then all results should contain only cities: 
	| City    |
	| Varna   |
	| Plovdiv |


