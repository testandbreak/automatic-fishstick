Feature: Login
![Login API Spec](https://reqres.in/api-docs/#/default/post_login)
Simple authentication API

Link to a feature: [Login]($projectname$/Features/Login.feature)

  @LoginAPI
  Scenario Outline: Login with an existing user to get authentication token
    Given the secret credentials for <EmailAddress>
    When requesting to login with my credentials
    Then the returned HTTP response code is 200
    And the authentication response contains a token

    Examples: 
      | EmailAddress       | 
      | eve.holt@reqres.in |

  @LoginAPI
  Scenario Outline: Login with invalid email and passwords combos
    Given the credentials (<EmailAddress>,<Password>)
    When requesting to login with my credentials
    Then the returned HTTP response code is 400
    And the error message is <ErrorMessage>

    Examples: 
      | EmailAddress       | Password   | ErrorMessage                |
      | eve.holt@reqres.in |            | 'Missing password'          |
      | eve.holt@reqres.in | xyz456     | '???'                       |
      |                    | qwertyAS2! | 'Missing email or username' |
      |                    |            | 'Missing email or username' |
      | fakeuser@here.com  | abc123     | 'user not found'            |
