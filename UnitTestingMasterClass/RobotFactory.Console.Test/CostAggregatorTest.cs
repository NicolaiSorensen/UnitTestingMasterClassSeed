using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TestUtilities;
using Xunit;

namespace RobotFactory.Console.Test
{
    public class CostAggregatorTest
    {
        public sealed class Constructor : CostAggregatorTest
        {
            [Theory]
            [AutoDataWithCustomization(typeof(AutoMoqWithMembersCustomization))]
            public void Should_be_guarded([Frozen] Fixture fixture)
            {
                var assertion = new GuardClauseAssertion(fixture);

                assertion.Verify(typeof(CostAggregator).GetConstructors());
            }
        }



        [Theory]
        [AutoDataWithCustomization(typeof(PartsMatchingSpecificationCustomization) ,typeof(AutoMoqWithMembersCustomization))]
        public void Should_find_lowest_price_from_suppliers(PartSpecification specification, Part expectedPart, [Frozen]IEnumerable<ISupplier> suppliers, CostAggregator sut)
        {
            expectedPart = expectedPart with {Price = 99};
            Mock.Get(suppliers.First()).Setup(x => x.GetPart(specification)).Returns(expectedPart);

            var part = sut.FindLowestPrice(specification);
            
            part.Should().BeEquivalentTo(expectedPart);
        }

        [Theory]
        [AutoDataWithCustomization(typeof(PartsMatchingSpecificationCustomization), typeof(AutoMoqWithMembersCustomization))]
        public void Should_find_all_parts_from_suppliers(PartSpecification specification, [Frozen] IEnumerable<ISupplier> suppliers, CostAggregator sut)
        {
            var expectedParts = suppliers.SelectMany(x => x.GetParts(specification));

            var parts = sut.GetParts(specification);

            parts.Should().BeEquivalentTo(expectedParts);
        }

    }


    public class PartsMatchingSpecificationCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var specification = fixture.Freeze<PartSpecification>();

            fixture.Customize<Part>(composer => composer.With(x => x.Price, 100).With(x => x.Type, specification.Type));
        }
    }
}
