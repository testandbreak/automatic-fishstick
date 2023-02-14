using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Assist;
using Newtonsoft.Json;
using FluentAssertions;

using SampleTestFramework.Drivers;

namespace SampleTestFramework.Steps
{
    [Binding]
    public sealed class UsersSteps : BaseSteps
    {
        private UsersAPI api;

        public UsersSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper) : base(scenarioContext,outputHelper)
        {
            api = new UsersAPI();
        }

        [Given(@"the user id (\d+)")]
        public void TheUserId(int userId)
        {
            AddScenarioData("userId", userId);
        }

        [Given(@"requesting to get list of users without pagenation")]
        public void RequestingToGetListOfUsersWithoutPagenation()
        {
            (var response, var payload) = api.List(null, null);

            Log(api.AsString(response.Request));
            Log(api.AsString(response));

            AddScenarioData("response", response);
            if (payload is UserPageData)
                AddScenarioData("usersList", payload);
            else
                AddScenarioData("payload", payload);
        }


        [Given(@"requesting to get list of users with default pagenation")]
        public void RequestingToGetListOfUsersWithDefaultPagenation()
        {
            // Get First Page
            (var response, var payload) = api.List(null, null);

            Log(api.AsString(response.Request));
            Log(api.AsString(response));


            response.StatusCode.Should().Be(HttpStatusCode.OK);
            payload.Should().BeOfType<UserPageData>();
            var totalPages = ((UserPageData)payload).TotalPages;

            UserPageData allUsers = new UserPageData();
            allUsers.TotalPages = ((UserPageData)payload).TotalPages;
            allUsers.Total = ((UserPageData)payload).Total;
            allUsers.Data = (((UserPageData)payload).Data);

            totalPages.Should().BeGreaterThan(1);

            for (int pageNumber = 2; pageNumber <= totalPages; pageNumber++)
            {
                (response, payload) = api.List(pageNumber, null);

                Log(api.AsString(response.Request));
                Log(api.AsString(response));

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                payload.Should().BeOfType<UserPageData>();
                allUsers.Data.AddRange(((UserPageData)payload).Data);
            }

            Log(api.AsString(allUsers));

            AddScenarioData("usersList", allUsers);
        }

        [Given(@"requesting to get list of users on page (\d+) with default pagenation")]
        public void RequestingToGetListOfUsersOnPageWithDefaultPagenation(int page)
        {
            (var response, var payload) = api.List(page, null);

            Log(api.AsString(response.Request));
            Log(api.AsString(response));

            AddScenarioData("response", response);
            if (payload is UserPageData)
                AddScenarioData("usersList", payload);
            else
                AddScenarioData("payload", payload);
        }

        [Given(@"requesting to get list of users on page (\d+) with (\d+) users per page")]
        public void RequestingToGetListOfUsersOnPageWithUsersPerPage(int page, int perPage)
        {
            (var response, var payload) = api.List(page, perPage);

            Log(api.AsString(response.Request));
            Log(api.AsString(response));

            AddScenarioData("response", response);
            if (payload is UserPageData)
                AddScenarioData("usersList", payload);
            else
                AddScenarioData("payload", payload);
        }

        [When(@"requesting to get the data for the specific user")]
        public void RequestingToGetTheDataForTheSpecificUser()
        {
            (var response, var payload) = api.Get(GetScenarioData<int>("userId"));

            Log(api.AsString(response.Request));
            Log(api.AsString(response));

            SetScenarioData("response", response);
            if (payload is UserSingleData)
                AddScenarioData("userData", ((UserSingleData)payload).Data);
            else
                AddScenarioData("payload", payload);
        }

        [When(@"requesting to delete the specific user")]
        public void RequestingToDeleteTheSpecificUser()
        {
            var response = api.Delete(GetScenarioData<int>("userId"));

            Log(api.AsString(response.Request));
            Log(api.AsString(response));

            AddScenarioData("response", response);
        }

        [Given(@"new user data:")]
        public void NewUserData(Table userTable)
        {
            UserData userData = userTable.CreateInstance<UserData>();
            userTable.CompareToInstance<UserData>(userData);
            Log(api.AsString(userData));
            SetScenarioData<UserData>("newUserData", userData);
        }

        [Then(@"the new users data is:")]
        public void TheNewUsersDataIs(Table userTable)
        {
            UserData userData = GetScenarioData<UserData>("newUserData");
            Log(api.AsString(userData));
            userTable.CompareToInstance<UserData>(userData);
        }

        
        [Given(@"new user with name (.*) and job (.*)")]
        public void NewUserData(string name, string job)
        {
            UserData userData = new UserData();
            userData.Name = name;
            userData.Job = job;
            
            SetScenarioData<UserData>("newUserData", userData);
        }

        [When(@"requesting to create a new user")]
        public void RequestingToCreateANewUser()
        {
            UserData userData = GetScenarioData<UserData>("newUserData");
            (var response, var payload) = api.Create(userData);

            Log(api.AsString(response.Request));
            Log(api.AsString(userData));
            Log(api.AsString(response));

            AddScenarioData("response", response);

            if (payload is UserData)
                AddScenarioData("userData", payload);
            else
                AddScenarioData("payload", payload);
        }

        [Then(@"there are more than (\d+) users in total")]
        public void ThereAreMoreThanUsersInTotal(int totalUsers)
        {
            var userListData = GetScenarioData<UserPageData>("usersList");
            userListData.Total.Should().BeGreaterThan(totalUsers);
        }

        [Then(@"there are (\d+) pages in total")]
        public void ThereArePagesInTotal(int totalPages)
        {
            var userListData = GetScenarioData<UserPageData>("usersList");
            userListData.TotalPages.Should().Be(totalPages);
        }

        [Then(@"there are (\d+) users in total")]
        public void ThereAreUsersInTotal(int totalUsers)
        {
            var userListData = GetScenarioData<UserPageData>("usersList");
            userListData.Total.Should().Be(totalUsers);
        }

        [Then(@"there are more than (\d+) pages in total")]
        public void ThereAreMoreThanPagesInTotal(int totalPages)
        {
            var userListData = GetScenarioData<UserPageData>("usersList");
            userListData.TotalPages.Should().BeGreaterThan(totalPages);
        }

        [Then(@"there are (\d+) users returned")]
        public void ThereAreUsersreturned(int users)
        {
            var userListData = GetScenarioData<UserPageData>("usersList");
            userListData.Data.Count.Should().Be(users);
        }

        [Then(@"the user with full name (.*) (.*) is in the list")]
        public void TheUserWithFullNameIsInTheListIsInTheList(string firstName, string lastName)
        {
            var userListData = GetScenarioData<UserPageData>("usersList");
            UserData user = userListData.Data.Find(user => user.FirstName == firstName && user.LastName == lastName );
            user.Should().NotBeNull();
            JsonConvert.SerializeObject(user, Formatting.Indented);
            user.FirstName.Should().Be(firstName);
            user.LastName.Should().Be(lastName);
        }
    
        [Then(@"the user with full name (.*) (.*) is not in the list")]
        public void TheUserWithFullNameIsInTheListIsNotInTheList(string firstName, string lastName)
        {
            var userListData = GetScenarioData<UserPageData>("usersList");
            UserData user = userListData.Data.Find(user => user.FirstName == firstName && user.LastName == lastName );
            user.Should().BeNull();
            JsonConvert.SerializeObject(user, Formatting.Indented);
        }

        [Then(@"the users name is (.*)")]
        public void TheUsersNameIs(string name)
        {
            var userData = GetScenarioData<UserData>("userData");
            userData.Name.Should().Be(name);
        }

        [Then(@"the users job is (.*)")]
        public void TheUsersJobIs(string job)
        {
            var userData = GetScenarioData<UserData>("userData");
            userData.Job.Should().Be(job);
        }

        [Then(@"the users first name is (.*)")]
        public void TheUsersFirstNameIs(string firstName)
        {
            var userData = GetScenarioData<UserData>("userData");
            userData.FirstName.Should().Be(firstName);
        }

        [Then(@"the users last name is (.*)")]
        public void TheUsersLastNameIs(string lastName)
        {
            var userData = GetScenarioData<UserData>("userData");
            userData.LastName.Should().Be(lastName);
        }
        [Then(@"the users email address is (.*)")]
        public void TheUsersEmailAddressIs(string emailAddress)
        {
            var userData = GetScenarioData<UserData>("userData");
            userData.Email.Should().Be(emailAddress);
        }

        [Then(@"the users avatar is (.*)")]
        public void TheUsersAvatarIs(string avatar)
        {
            var userData = GetScenarioData<UserData>("userData");
            userData.Avatar.Should().Be(avatar);
        }

        [Then(@"the user has an Id")]
        public void TheCreatedUserHasAnId()
        {
            var userData = GetScenarioData<UserData>("userData");
            userData.Id.Should().NotBeNull().And.BePositive();
        }

    }
}