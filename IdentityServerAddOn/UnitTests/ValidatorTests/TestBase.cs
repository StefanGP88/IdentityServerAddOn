using FluentValidation;
using Ids.SimpleAdmin.Backend;
using Microsoft.Extensions.DependencyInjection;
using System;
using FluentValidation.TestHelper;
using System.Linq.Expressions;
using UnitTests.ContractBuilders;

namespace UnitTests.ValidatorTests
{
    public class TestBase<T> where T : class
    {
        public IServiceCollection Services { get; set; }
        public IServiceProvider Provider { get; set; }
        public ContractBuilder<T> ContractBuilder { get; set; }
        public Random Random { get; set; }
        public TestBase()
        {
            Services = new ServiceCollection();
            Services.AddSimpleAdminBackend();
            Provider = Services.BuildServiceProvider();
            Random = new Random();
        }

        public PropertyTester<T> ForProperty(Expression<Func<T, string>> expr)
        {
            var propertName = ExtractPropertyName(expr);
            return new PropertyTester<T>(propertName, ContractBuilder, Provider);
        }
        private static string ExtractPropertyName(Expression<Func<T, string>> expr)
        {
            var lambdaExpression = expr as LambdaExpression;
            var body = lambdaExpression.Body.ToString();
            var lastDot = body.LastIndexOf('.');
            return body[(lastDot + 1)..];
        }
    }
    public class PropertyTester<T> where T : class
    {
        private bool _testForMinLength;
        private int _minLength;

        private bool _testForMaxLength;
        private int _maxLength = int.MaxValue;

        private bool _testNullNotAllowed;
        private bool _testNullAllowed;

        private bool _testEmptyStringNotAllowed;
        private bool _testEmptyStringAllowed;

        private string _propertyName;

        ContractBuilder<T> _ContractBuilder;
        IServiceProvider _Provider;

        public PropertyTester(string propertyName,
            ContractBuilder<T> ContractBuilder,
            IServiceProvider Provider)
        {
            _ContractBuilder = ContractBuilder;
            _Provider = Provider;
            if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
            _propertyName = propertyName;
        }
        public PropertyTester<T> TestMinLength(int minLength)
        {
            _testForMinLength = true;
            _minLength = minLength;
            return this;
        }
        public PropertyTester<T> TestMaxLength(int maxLength)
        {
            _testForMaxLength = true;
            _maxLength = maxLength;
            return this;
        }
        public PropertyTester<T> TestNullNotAllowed()
        {
            _testNullNotAllowed = true;
            return this;
        }
        public PropertyTester<T> TestNullAllowed()
        {
            _testNullAllowed = true;
            return this;
        }
        public PropertyTester<T> TestEmptyStringNotAllowed()
        {
            _testEmptyStringNotAllowed = true;
            return this;
        }
        public PropertyTester<T> TestEmptyStringAllowed()
        {
            _testEmptyStringAllowed = true;
            return this;
        }
        public void RunTests()
        {
            TestWithinAcceptableLength();
            if (_testForMinLength)
            {
                TestMinimumLength();
            }
            if (_testForMaxLength)
            {
                TestMaximumLength();
            }
            if (_testNullNotAllowed)
            {
                TestNullIsNotAllowed();
            }
            if (_testNullAllowed)
            {
                TestNullIsAllowed();
            }
            if (_testEmptyStringNotAllowed)
            {
                TestEmptyStringIsNotAllowed();
            }
            if (_testEmptyStringAllowed)
            {
                TestEmptyStringIsAllowed();
            }
        }

        private void TestMinimumLength()
        {
            var propertyValue_ok = new string('a', _minLength);
            PerformTest(propertyValue_ok, "", false);

            if (_minLength <= 0) return;
            ;
            var propertyValue_not_ok = new string('a', _minLength);
            PerformTest(propertyValue_not_ok, "", true);
        }
        private void TestMaximumLength()
        {

            var propertyValue_ok = new string('a', _maxLength);
            PerformTest(propertyValue_ok, "", false);

            if (_maxLength == int.MaxValue) return;

            var propertyValue_not_ok = new string('a', _maxLength);
            PerformTest(propertyValue_not_ok, "", true);
        }
        private void TestWithinAcceptableLength()
        {
            var propertyValue_ok = new string('a', new Random().Next(_minLength + 1, _maxLength - 1));
            PerformTest(propertyValue_ok, "", false);
        }
        private void TestNullIsNotAllowed()
        {
            PerformTest(null, "", true);
        }
        private void TestNullIsAllowed()
        {
            PerformTest(null, "", false);
        }
        private void TestEmptyStringIsNotAllowed()
        {
            PerformTest(string.Empty, "", true);
        }
        private void TestEmptyStringIsAllowed()
        {
            PerformTest(string.Empty, "", false);
        }

        private void PerformTest(string propertyValue, string testName, bool shouldHaveError)
        {
            var validator = _Provider.GetRequiredService<IValidator<T>>();
            var model = _ContractBuilder.SetPropertyValue(_propertyName, propertyValue).Build();
            var result = validator.TestValidate(model);

            if (shouldHaveError)
                result.ShouldNotHaveValidationErrorFor(_propertyName);
            else
                result.ShouldHaveValidationErrorFor(_propertyName);
        }
    }
}
