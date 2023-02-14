Feature: Users API
  ![Users API Spec](https://reqres.in/api-docs/#/default/get_users)
  Simple Users API

  Link to a feature: [Users]($projectname$/Features/Users.feature)

  @UsersAPI
  Scenario: List all users retrieving just the default response
    Given requesting to get list of users without pagenation
    Then the returned HTTP response code is 200
    And there are more than 1 users in total
    And there are more than 1 pages in total

  @UsersAPI
  Scenario: List all users retrieving starting with the default response
    Given requesting to get list of users with default pagenation

  @UsersAPI
  Scenario Outline: Users already in the system should be returned in the list of users
    Given requesting to get list of users with default pagenation
    Then the user with full name <firstName> <lastName> is in the list

    Examples:
      | firstName | lastName |
      | Lindsay   | Ferguson |

  @UsersAPI
  Scenario Outline: Only users already in the system should be returned in the list of users
    Given requesting to get list of users with default pagenation
    Then the user with full name <firstName> <lastName> is not in the list

    Examples:
      | firstName | lastName |
      | Julie     | McMannis |

  @UsersAPI
  Scenario: Get a list of users on a page that is beyond the data
    Given requesting to get list of users on page 6 with default pagenation
    Then the returned HTTP response code is 400
    Then the error message is '??? page requested it out of range of the data'

  @UsersAPI
  Scenario: Get a list of all users on a single page
    Given requesting to get list of users on page 1 with 12 users per page
    Then the returned HTTP response code is 200
    And there are 12 users returned
    And there are 12 users in total
    And there are 1 pages in total


  @UsersAPI
  Scenario Outline: Get a specific user details
    Given the user id <userId>
    When requesting to get the data for the specific user
    Then the returned HTTP response code is 200
    And the users first name is <firstName>
    And the users last name is <lastName>
    And the users email address is <emailAddress>
    And the users avatar is <avatar>

    Examples:
      | userId | firstName | lastName | emailAddress           | avatar                                  |
      | 1      | George    | Bluth    | george.bluth@reqres.in | https://reqres.in/img/faces/1-image.jpg |

  @UsersAPI
  Scenario Outline: Try to get specific user details for unknown user
    Given the user id <userId>
    When requesting to get the data for the specific user
    Then the returned HTTP response code is 404

    Examples:
      | userId |
      | 9999   |

  @UsersAPI
  Scenario Outline: Delete a specific user
    Given the user id <userId>
    When requesting to delete the specific user
    Then the returned HTTP response code is 204
    When requesting to get the data for the specific user
    Then the returned HTTP response code is 404

    Examples:
      | userId |
      | 1      |

  @UsersAPI
  Scenario: Create a new user (Swagger)
    Given new user data:
      | Field     | Value                              |
      | FirstName | Tim                                |
      | LastName  | Taylor                             |
      | Email     | tt@here.tt                         |
      | Avatar    | https://here.tt/avatar/primary.png |
    When requesting to create a new user
    Then the returned HTTP response code is 201
    And the new users data is:
      | Field     | Value                              |
      | FirstName | Tim                                |
      | LastName  | Taylor                             |
      | Email     | tt@here.tt                         |
      | Avatar    | https://here.tt/avatar/primary.png |
    And the user has an Id

  @UsersAPI
  Scenario Outline: Create a new user (Samples)
    Given new user with name <name> and job <job>
    When requesting to create a new user
    Then the returned HTTP response code is 201
    And the users name is <name>
    And the users job is <job>
    And the user has an Id

    Examples:
      | name     | job    |
      | morpheus | leader |
