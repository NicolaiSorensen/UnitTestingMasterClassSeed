using AutoFixture.Kernel;

namespace FizzBuzzWhiz.Console.Test
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using AutoFixture;
    using AutoFixture.Idioms;
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using TestUtilities;
    using Xunit;

    public class FizzBuzzEngineTest
    {
        public sealed class Constructor : FizzBuzzEngineTest
        {
            [Theory]
            [AutoDataWithCustomization(typeof(AutoMoqWithMembersCustomization))]
            public void Should_be_guarded([Frozen] Fixture fixture)
            {
                var assertion = new GuardClauseAssertion(fixture);

                assertion.Verify(typeof(FizzBuzzEngine).GetConstructors(BindingFlags.Public));
            }
        }

        public class Convert : FizzBuzzEngineTest
        {
            [Theory]
            [InlineAutoDataWithCustomization(typeof(NumberDividedByCustomization), 3)]
            public void Should_convert_numbers_dividable_by_3_to_fizz(int number, FizzBuzzEngine sut)
            {
                var result = sut.Convert(number);
                result.Should().Contain("Fizz");
            }

            [Theory]
            [InlineAutoDataWithCustomization(typeof(NumberDividedByCustomization), 5)]
            public void Should_convert_numbers_dividable_by_5_to_buzz(int number, FizzBuzzEngine sut)
            {
                var result = sut.Convert(number);
                result.Should().Contain("Buzz");
            }

            [Theory]
            [AutoDataWithCustomization(typeof(PrimeNumberCustomization))]
            public void Should_convert_prime_numbers_to_whiz(int number, FizzBuzzEngine sut)
            {
                var result = sut.Convert(number);
                result.Should().Contain("Whiz");
            }

            [Theory]
            [AutoDataWithCustomization(typeof(NumberNotDividedByCustomization))]
            public void Should_not_convert_if_number_does_not_match_rule(int number, FizzBuzzEngine sut)
            {
                var result = sut.Convert(number);
                result.Should().Be(number.ToString());
            }

            [Theory]
            [AutoDataWithCustomization(typeof(PrimeEvaluationRelay))]
            public void Should_not_treat_one_as_prime(FizzBuzzEngine sut)
            {
                var result = sut.Convert(1);
                result.Should().Be("1");
            }

        }

    }

    public class PrimeNumberCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var generator = fixture.Create<Generator<int>>();

            var primeEvaluationEngine = new PrimeEvaluation();
            var number = generator.First(primeEvaluationEngine.IsPrime);

            fixture.Inject(number);
            fixture.Register<IPrimeEvaluation>(() => new PrimeEvaluation());
        }
    }

    public class NumberDividedByCustomization : ICustomization
    {
        private readonly int _factor;

        public NumberDividedByCustomization(int factor)
        {
            _factor = factor;
        }
        public void Customize(IFixture fixture)
        {
            var generator = fixture.Create<Generator<int>>();

            var number = generator.First(x => x % _factor == 0 && x != _factor);

            fixture.Inject(number);
            fixture.Register<IPrimeEvaluation>(() => new PrimeEvaluation());
        }
    }

    public class NumberNotDividedByCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var generator = fixture.Create<Generator<int>>();

            var number = generator.First(x =>
            {
                var primeEvaluationEngine = new PrimeEvaluation();
                return x % 3 != 0 && x % 5 != 0 && !primeEvaluationEngine.IsPrime(x);
            });

            fixture.Inject(number);
            fixture.Register<IPrimeEvaluation>(() => new PrimeEvaluation());
        }
    }

    public class PrimeEvaluationRelay : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IPrimeEvaluation),
                    typeof(PrimeEvaluation)));
        }
    }
}