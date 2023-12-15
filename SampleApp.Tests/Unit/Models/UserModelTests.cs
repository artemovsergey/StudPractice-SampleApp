using SampleApp.RazorPage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Tests.Unit.Models
{
    public class UserModelTests
    {
        private User _user;
        private List<ValidationResult> _validationResults;

        public UserModelTests()
        {
            _user = new User { Name = "Example User", Email = "user@example.com", Password = "123" };
            _validationResults = new List<ValidationResult>();
        }

        [Fact]
        public void UserModel_ShouldBeValid()
        {
            var context = new ValidationContext(_user, null, null);
            bool isValid = Validator.TryValidateObject(_user, context, _validationResults, true);

            Assert.True(isValid);
            Assert.Empty(_validationResults);
        }

        [Fact]
        public void UserModel_ShouldNotBeValid_WhenNameIsEmpty()
        {
            _user.Name = string.Empty;

            var context = new ValidationContext(_user, null, null);
            bool isValid = Validator.TryValidateObject(_user, context, _validationResults, true);

            Assert.False(isValid);
            Assert.NotEmpty(_validationResults);
        }

        [Fact]
        public void UserModel_ShouldNotBeValid_WhenEmailIsEmpty()
        {
            _user.Email = string.Empty;

            var context = new ValidationContext(_user, null, null);
            bool isValid = Validator.TryValidateObject(_user, context, _validationResults, true);

            Assert.False(isValid);
            Assert.NotEmpty(_validationResults);
        }

        [Fact]
        public void UserModel_ShouldNotBeValid_WhenEmailIsInvalid()
        {
            _user.Email = "invalid_email";

            var context = new ValidationContext(_user, null, null);
            bool isValid = Validator.TryValidateObject(_user, context, _validationResults, true);

            Assert.False(isValid);
            Assert.NotEmpty(_validationResults);
        }
    }
}
