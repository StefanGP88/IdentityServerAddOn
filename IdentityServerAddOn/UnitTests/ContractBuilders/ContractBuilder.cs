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

        public T Build()
        {
            var current = Current;
            Current = Default;
            return current;
        }
    }
}
