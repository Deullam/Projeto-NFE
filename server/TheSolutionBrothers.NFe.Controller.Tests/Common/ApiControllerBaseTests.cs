using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TheSolutionBrothers.Nfe.API.Exceptions;
using TheSolutionBrothers.NFe.Controller.Tests.Initializer;
using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.NFe.Controller.Tests.Common
{
    [TestFixture]
    public class ApiControllerBaseTests : TestControllerBase
    {
        private ApiControllerBaseFake _apiControllerBase;
        private Mock<ApiControllerBaseDummy> _dummy;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _apiControllerBase = new ApiControllerBaseFake()
            {
                Request = request
            };
            _dummy = new Mock<ApiControllerBaseDummy>();
        }

        [Test]
        public void Base_Controller_HandleCallback_ShouldHandleBusinessException()
        {
            var message = "message error test";
            var exception = new BusinessException(ErrorCodes.AlreadyExists, message);

            var callback = _apiControllerBase.HandleCallback<ApiControllerBaseDummy>(() => throw exception);

            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            httpResponse.Content.ErrorCode.Should().Be((int)ErrorCodes.AlreadyExists);
            httpResponse.Content.ErrorMessage.Should().Be(message);
        }

        [Test]
        public void Base_Controller_HandleCallback_ShouldHandleRuntimeException()
        {
            var message = "message error test";
            var exception = new Exception(message);

            var callback = _apiControllerBase.HandleCallback<ApiControllerBaseDummy>(() => throw exception);

            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            httpResponse.Content.ErrorCode.Should().Be((int)ErrorCodes.Unauthorized);
            httpResponse.Content.ErrorMessage.Should().Be(message);
        }

        [Test]
        public void Base_Controller_HandleQuery_ShouldBeOk()
        {
            var query = new List<ApiControllerBaseDummy>() { _dummy.Object }.AsQueryable();

            var callback = _apiControllerBase.HandleQuery(query);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ApiControllerBaseDummy>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
        }

        [Test]
        public void Base_Controller_HandleQueryable_ShouldBeOk()
        {
            var query = new List<ApiControllerBaseDummy>() { _dummy.Object }.AsQueryable();

            var callback = _apiControllerBase.HandleQueryable<ApiControllerBaseDummy>(query);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ApiControllerBaseDummy>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
        }

        [Test]
        public void Base_Controller_HandleValidationFailure_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var validationFailure = new ValidationFailure("", ((int)ErrorCodes.Unhandled).ToString());
            IList<ValidationFailure> errors = new List<ValidationFailure>() { validationFailure };
            // Action
            var callback = _apiControllerBase.HandleValidationFailure(errors);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.FirstOrDefault().Should().Be(validationFailure);
        }

        //[Test]
        //public void Base_Controller_HandlePageResult_ShouldBeOk()
        //{
        //    var query = new List<ApiControllerBaseDummy>() { _dummy.Object }.AsQueryable();
        //    var odataOptions = GetOdataQueryOptions<ApiControllerBaseDummy>(_apiControllerBase);
            
        //    var callback = _apiControllerBase.HandleQueryable<ApiControllerBaseDummy, ApiControllerBaseDummyViewModel>(query, odataOptions);
            
        //    var contentResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ApiControllerBaseDummyViewModel>>>().Subject;
        //    contentResponse.Should().NotBeNull();
        //}
    }
}
