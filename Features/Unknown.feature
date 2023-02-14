Feature: Unknown
Simple Unknown API that returns colour resource data

Link to a feature: [Login]($projectname$/Features/Unkown.feature)

  @UnknownAPI
  Scenario Outline: Request to get a specific resource
    Given the resource id <UnkownResourceId>
    When requesting to get the data for the specific unknown resource
    Then the returned HTTP response code is <HttpRespCode>

    Examples: 
      | UnkownResourceId | HttpRespCode |
      |               23 |          404 |
      |                2 |          200 |
