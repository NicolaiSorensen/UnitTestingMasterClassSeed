namespace TestUtilities
{
    using System;
    using AutoFixture;
    using AutoFixture.Xunit2;

    public class AutoDataWithFactoryAttribute : AutoDataAttribute
    {
        public AutoDataWithFactoryAttribute(Func<IFixture> fixtureFactory)
            : base(fixtureFactory)
        {
        }
    }
}