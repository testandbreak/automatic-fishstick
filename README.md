# Sample Test Framework

A simple framework to demonstrate basic API testing using C# and SpecFlow.

## System Under Test
Base API Endpoint: https://reqres.in/api/

## Specifications
- By Request/Resposne Example - "Give it a try" (https://reqres.in/)
- By Swagger Spec (https://reqres.in/api-docs)


### Base API Scenarios
As per spec by example

- GET List Users
- GET Single User
- GET Single User Not Found
- GET Single\<Resource\> Not Found
- POST Create
- DELETE Delete
- POST Register - Successful
- POST Register - Unsuccessful
- POST Login - Successful


## Setting Up Automation

Built using dotnet SDK `v7.0.102` against the `v7.0.2` runtime.  The following
steps are for using basic PowerShell to execute the tests, but can be adjusted
to execute on the legacy basic command prompt/terminal.

Install the LivingDoc reporting command line tools to be able to generate the
test results as a web page report.

```powershell
dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
```

If you are going to execute from the solution folder set an environment
variable to point at the config folder which holds the environment config
for various environments.

```powershell
$Env:QA_CONFIG_ROOT="$pwd\Config\"
```

Select the test environment to execute against, the name of the environment
should be paired with a file in the ```Config``` folder (e.g. `sit.json`).
This basic configuration component just uses a simple json object with key
and value pairs.

```powershell
$Env:QA_ENV="sit"
```

Set the Secret Server API Key should be set as an environment variable for the
process (can be hidden in most pipeline toolsets) - this is a good approach 
when working in secure environments where you use a secured server to manage
and log/audit access to secrets (generally used in production environments but
also of usefl to apply secure practices to non-production environemnts)

```powershell
$Env:QA_SECRETSERVER_APIKEY="2asd32%ad0skjas(#J%Jrd"
```

## Execution

From the solution folder execute

```powershell
dotnet test [--filter Category=<Tag>]
```

The tests can be selectivly executed with the following tags
- `LoginAPI`
- `UsersAPI`
- `RegisterAPI`
- `UnknownAPI`


## Results

Execute the following command to view the results using LivingDoc tooling.

```shell
livingdoc test-assembly bin\Debug\netcoreapp7.0\SampleTestFramework.dll -t bin\debug\netcoreapp7.0\TestExecution.json
```

Then using web browser load the [LivingDoc](LivingDoc.html) results report.


## Notes & Assumptions

- The request/response payloads shown on website front page do not align to the
  swagger or the actual service implemenations.  I have built based on the
  - assumption of the current schemas sent/recieved by the app are roughly as
    intended.
  - adjustment where seemed appropriate to merge the sample data.
  - negative scenarios assume a potential behavuour if the facades were richer.

- Since specification by example is not usually a good practice this would
  normally be addressed before commecing test development to close out the need
  to proceed at risk with unvalidated assumptions.

- Some tests will fail due to the services not fully performing, based on the
  behaviours exhibited they appear to be stateless facades rather than
  performing any specific function... just enough to send back a response with
  zero intelligence behind.
  - Login example where correct user email, but incorrect password should
    normally fail.
  - Users delete (get the specific resource still works).
  - Additional tests for pagination tests, but API sample doesn't model
    potential negative scenarios.

- Assumption this is the beginning of a new lifecycle of independant APIs that
  will change over time so kept seperation rather than inferring & locking in a
  common API payload structure.  If the APIs were known to be common in their
  structure you'd likely look towards having the structures combined as more
  objects that contain common properties and/or key/value pairs (such as a
  dictionary), the driver models themselves could also make more heavy use of
  inherited functionality.

- Simple manual logging applied as a minimum, logging could also be applied at
  various levels across the code and enrighted.

- The create user scenario that uses a table is an example only of the use of
  tables in

    - Scenario definitions
    - parsing in Steps code to instantiate data objects
    - comparing in Steps code to verify data

  however in this specific situation it adds unncessary verbosity to the
  scenario definition and is limited on the expansion to use examples.


## Assessment Requirements

1.	Please design and implement an automation framework which can test all the
    methods provided in the above website.
    [from the list of methods shared in email]. 
2.	Make sure to add positive and negative cases.
3.	Readme file is preferred.
4.	Verify that Lindsay Ferguson is a user by querying “List Users”. How do you
    design your framework so that we can query for another justuser with
    minimalchanges to the framework.
5.	In the framework, try to implement best practices
    a.	The framework should be able to run in multiple environments.
        ( Test, SIT, UAT/Pre-Prod etc)
    b.	The framework should be able to use secrets/passwords/sensitive
        information effectively
    c.	Ability to run as single threaded or multi threaded.
    d.	Bonus points for the ability to generate events and listen to events
