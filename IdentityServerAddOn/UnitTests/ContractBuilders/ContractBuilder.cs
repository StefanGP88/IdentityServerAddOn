using System;

namespace UnitTests.ContractBuilders
{
    public class ContractBuilder<T>
    {
        public ContractBuilder(T defaultContract)
        {
            Default = defaultContract;
            Current = Default;
        }

        private T Default;
        private T Current;

        public ContractBuilder<T> With(Action<T> action)
        {
            action(Current);
            return this;
        }
        public ContractBuilder<T> SetPropertyValue<T2>(string propertyName, T2 value)
        {

            var contractProperty = typeof(T).GetProperty(propertyName);
            if (contractProperty is null) throw new Exception(propertyName + " not found");

            contractProperty.SetValue(Current, value);

            return this;
        }

        public T Build()
        {
            var current = Current;
            Current = Default;
            return current;
        }
    }
}
