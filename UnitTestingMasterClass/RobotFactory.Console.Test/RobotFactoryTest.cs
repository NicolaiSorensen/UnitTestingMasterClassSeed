using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TestUtilities;
using Xunit;

namespace RobotFactory.Console.Test
{
    public class RobotFactoryTest
    {
        public sealed class Constructor : RobotFactoryTest
        {
            [Theory]
            [AutoDataWithCustomization(typeof(AutoMoqWithMembersCustomization))]
            public void Should_be_guarded([Frozen] Fixture fixture)
            {
                var assertion = new GuardClauseAssertion(fixture);

                assertion.Verify(typeof(RobotFactory).GetConstructors());
            }
        }

        [Theory]
        [AutoDataWithCustomization(typeof(AutoMoqWithMembersCustomization))]
        public void Should_build_robot_with_arms_to_specification(RobotSpecification specification, RobotFactory sut)
        {
            Robot robot = sut.Build(specification);

            robot.Arms.Type.Should().BeEquivalentTo(specification.Arms.Type);
        }
    }
}