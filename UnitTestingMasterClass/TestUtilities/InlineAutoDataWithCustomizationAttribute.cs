namespace TestUtilities
{
    using System;
    using System.Linq;
    using AutoFixture;
    using AutoFixture.Xunit2;

    public sealed class InlineAutoDataWithCustomizationAttribute : InlineAutoDataAttribute
    {
        public InlineAutoDataWithCustomizationAttribute(Type inlineDataCustomization, Type[] otherCustomizations, params object[] arguments)
            : base(
                new AutoDataWithFactoryAttribute(
                    () =>
                    {
                        var compositeCustomization = new CompositeCustomization(otherCustomizations.Select(customization =>
                                                                                        (ICustomization)Activator.CreateInstance(customization))
                                                                                    .Append((ICustomization)Activator.CreateInstance(inlineDataCustomization, arguments)));

                        return new Fixture().Customize(compositeCustomization);
                    }), arguments)
        {
        }

        public InlineAutoDataWithCustomizationAttribute(Type inlineDataCustomization, params object[] arguments)
            : base(
                new AutoDataWithFactoryAttribute(
                    () => new Fixture().Customize((ICustomization)Activator.CreateInstance(inlineDataCustomization, arguments))))
        {
        }
    }
}