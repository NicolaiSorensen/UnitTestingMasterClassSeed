using System;

namespace FizzBuzzWhiz.Console
{
    public class FizzBuzzEngine
    {
        private readonly IPrimeEvaluation _primeEvaluation;

        public FizzBuzzEngine(IPrimeEvaluation primeEvaluation)
        {
            _primeEvaluation = primeEvaluation ?? throw new ArgumentNullException(nameof(primeEvaluation));
        }

        public string Convert(int number)
        {
            var result = string.Empty;
            result += number % 3 == 0 ? "Fizz" : string.Empty;
            result += number % 5 == 0 ? "Buzz" : string.Empty;
            result += _primeEvaluation.IsPrime(number) ? "Whiz" : string.Empty;

            if (result == string.Empty)
            {
                result = number.ToString();
            }
            return result;
        }
    }
}   