// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Customer.Request;
using Softeq.NetKit.Payments.Service.TransportModels.User.Request;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Stripe;

namespace Softeq.NetKit.Payments.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUserDataService _userDataService;
        private readonly ICustomerProvider _customerProvider;

        public CustomerService(ICustomerProvider customerProvider, IUserDataService userDataService)
        {
            _customerProvider = customerProvider;
            _userDataService = userDataService;
        }

        public async Task<object> GetCustomerEphemeralKeyAsync(CreateEphemeralKeyRequest request)
        {
            try
            {
                var doesUserExist = await _userDataService.DoesExistAsync(request.UserId);
                if (!doesUserExist)
                {
                    var stripeCustomer = await _customerProvider.CreateCustomerAsync(request.Email);
                    await _userDataService.CreateAsync(new CreateUserRequest(request.UserId, stripeCustomer.Id, stripeCustomer.Delinquent));
                    var key = await _customerProvider.CreateEphemeralKeyAsync(stripeCustomer.Id, request.UserId, request.Email, request.StripeVersion);
                    return key;
                }

                var user = await _userDataService.GetAsync(request.UserId);
                var newKey = await _customerProvider.CreateEphemeralKeyAsync(user.StripeCustomerId, request.UserId, request.Email, request.StripeVersion);
                return newKey;
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        public async Task CreateCustomerAsync(string userId, string email)
        {
            try
            {
                var doesUserExist = await _userDataService.DoesExistAsync(userId);
                if (doesUserExist)
                {
                    throw new DuplicateUserException(new ErrorDto(ErrorCode.DuplicateError, "User already exists."));
                }

                var stripeCustomer = await _customerProvider.CreateCustomerAsync(email);
                await _userDataService.CreateAsync(new CreateUserRequest(userId, stripeCustomer.Id, stripeCustomer.Delinquent));
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }
    }
}