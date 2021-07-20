using Ids.SimpleAdmin.Backend;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitTests.ContractBuilders;

namespace UnitTests.ValidatorTests
{
    public class TestBase<T>
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

        public PropertyTester ForProperty(Expression<Func<T, string>> expr)
        {
            var propertName = ExtractPropertyName(expr);
            return new PropertyTester(propertName);
        }
        private static string ExtractPropertyName(Expression<Func<T, string>> expr)
        {
            var lambdaExpression = expr as LambdaExpression;
            var body = lambdaExpression.Body.ToString();
            var lastDot = body.LastIndexOf('.');
            return body[(lastDot + 1)..];
        }
    }
    public class PropertyTester
    {
        private bool _testForMinLength = false;
        private int _minLength = 0;

        private bool _testForMaxLength = false;
        private int _maxLength = int.MaxValue;

        private bool _testNullNotAllowed = false;
        private bool _testNullAllowed = false;

        private bool _testEmptyStringNotAllowed = false;
        private bool _testEmptyStringAllowed = false;

        private string _propertyName;
        public PropertyTester(string propertyName)
        {
            if (propertyName is null) throw new  ArgumentNullException(nameof(propertyName));
            _propertyName = propertyName;
        }
        public PropertyTester TestMinLength(int minLength)
        {
            _testForMinLength = true;
            _minLength = minLength;
            return this;
        }
        public PropertyTester TestMaxLength(int maxLength)
        {
            _testForMaxLength = true;
            _maxLength = maxLength;
            return this;
        }
        public PropertyTester TestNullNotAllowed()
        {
            _testNullNotAllowed = true;
            return this;
        }
        public PropertyTester TestNullAllowed()
        {
            _testNullAllowed = true;
            return this;
        }
        public PropertyTester TestEmptyStringNotAllowed()
        {
            _testEmptyStringNotAllowed = true;
            return this;
        }
        public PropertyTester TestEmptyStringAllowed()
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

        }
        private void TestMaximumLength()
        {

        }
        private void TestWithinAcceptableLength()
        {

        }
        private void TestNullIsNotAllowed()
        {

        }
        private void TestNullIsAllowed()
        {

        }
        private void TestEmptyStringIsNotAllowed()
        {

        }
        private void TestEmptyStringIsAllowed()
        {

        }
    }
}
