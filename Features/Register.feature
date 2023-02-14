Feature: Register
![Register API Spec](https://reqres.in/api-docs/#/default/post_register)
Simple authentication API

Link to a feature: [Login]($projectname$/Features/Login.feature)

  @RegisterAPI
  Scenario Outline: Register a user to get authentication token
    Given the credentials (<EmailAddress>,<Password>)
    When requesting to register with my credentials
    Then the returned HTTP response code is 200
    And the authentication response contains a token
    And the authentication response contains the id <Id>

    Examples: 
      | EmailAddress       | Password  | Id |
      | eve.holt@reqres.in | az09@!_AZ |  4 |

  @RegisterAPI
  Scenario Outline: Register with invalid email and passwords combos
    Given the credentials (<EmailAddress>,<Password>)
    When requesting to register with my credentials
    Then the returned HTTP response code is 400
    And the error message is <ErrorMessage>

    Examples: 
      | EmailAddress       | Password   | ErrorMessage                                    |
      | eve.holt@reqres.in |            | 'Missing password'                              |
      |                    | cityslicka | 'Missing email or username'                     |
      |                    |            | 'Missing email or username'                     |
      | fakeuser@here.com  | abc123     | 'Note: Only defined users succeed registration' |
