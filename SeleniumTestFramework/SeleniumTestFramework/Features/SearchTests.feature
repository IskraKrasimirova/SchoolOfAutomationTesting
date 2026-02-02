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
      | firstName | Ivan                     |
      | surname   | Petrov                   |
      | email     | ivan.petrov@test.com     |
      | country   | Bulgaria                 |
      | city      | Sofia                    |
      | title     | Mr.                      |
      | password  | Password123!             |
      | isAdmin   | false                    |
    And the user has the following skills:
      | skillName           | competence |
      | Java                | 3          |
      | Automated Testing   | 5          |
    When I search for users with skill "Java"
    Then I should see "Ivan Petrov" in the search results
    And the skill "Java" should be displayed for "Ivan Petrov"
