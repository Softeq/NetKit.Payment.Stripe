// Developed by Softeq Development Corporation
// http://www.softeq.com

using Autofac;
using Softeq.NetKit.Payments.Service.Test.Abstract;
using Softeq.NetKit.Payments.Service.Test.Data;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.User.Request;
using Xunit;

namespace Softeq.NetKit.Payments.Service.Test.Services
{
    public class CustomerTests : BaseTest
    {
        private const string SaasUserId = "d865176a-f08f-4123-8db2-ec2e35f4d4a4";
        private const string SaasUserId2 = "07c3b9dc-1813-410f-89b0-c12cde00de66";
        private const string StripeCustomerId = "cus_CiN8cL9Q4OVhiC";

        private readonly IUserDataService _userDataService;

        public CustomerTests()
        {
            _userDataService = LifetimeScope.Resolve<IUserDataService>();
        }

        [Fact]
        public async void GetUserByIdAsyncTest()
        {
            var user = TestData.CreateUserRequest(SaasUserId);
            await _userDataService.CreateAsync(new CreateUserRequest(SaasUserId, user.StripeCustomerId, user.Delinquent));
            var newUser = await _userDataService.GetAsync(SaasUserId);
            Assert.Equal(user.SaasUserId, newUser.SaasUserId);
            Assert.Equal(user.Delinquent, newUser.Delinquent);
            Assert.Equal(user.StripeCustomerId, newUser.StripeCustomerId);
        }

        [Fact]
        public async void AddUserAsyncTest()
        {
            var user = TestData.CreateUserRequest(SaasUserId);
            await _userDataService.CreateAsync(new CreateUserRequest(SaasUserId, user.StripeCustomerId, user.Delinquent));
            var newUser = await _userDataService.GetAsync(SaasUserId);
            Assert.Equal(user.SaasUserId, newUser.SaasUserId);
            Assert.Equal(user.Delinquent, newUser.Delinquent);
            Assert.Equal(user.StripeCustomerId, newUser.StripeCustomerId);
        }

        [Fact]
        public async void UpdateUserAsyncTest()
        {
            var user = TestData.CreateUserRequest(SaasUserId);
            await _userDataService.CreateAsync(new CreateUserRequest(SaasUserId, user.StripeCustomerId, user.Delinquent));
            await _userDataService.UpdateAsync(new UpdateUserRequest(SaasUserId, StripeCustomerId));
            var newUser = await _userDataService.GetAsync(SaasUserId);
            Assert.Equal(newUser.StripeCustomerId, StripeCustomerId);
        }

        [Fact]
        public async void CheckIfUserExistAsyncTest()
        {
            var user = TestData.CreateUserRequest(SaasUserId);
            await _userDataService.CreateAsync(new CreateUserRequest(SaasUserId, user.StripeCustomerId, user.Delinquent));
            var isExist = await _userDataService.DoesExistAsync(SaasUserId);
            Assert.True(isExist);
            var newUser = await _userDataService.DoesExistAsync(SaasUserId2);
            Assert.False(newUser);
        }
    }
}