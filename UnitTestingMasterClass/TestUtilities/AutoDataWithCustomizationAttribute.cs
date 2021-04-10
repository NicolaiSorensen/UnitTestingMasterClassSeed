namespace TestUtilities
{
    using System;
    using System.Linq;
    using AutoFixture;
    using AutoFixture.Xunit2;

    public sealed class AutoDataWithCustomizationAttribute : AutoDataAttribute
    {
        public AutoDataWithCustomizationAttribute(params Type[] customizations)
            : base(
                () =>
                {
                    var composite = new CompositeCustomization(customizations.Select(customization =>
                                                                                         (ICustomization)Activator.CreateInstance(customization)));

                    return new Fixture().Customize(composite);
                })
        {
        }
    }
}
