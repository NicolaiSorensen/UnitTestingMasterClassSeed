namespace FizzBuzzWhiz.Console
{
    using System.Reflection.Metadata.Ecma335;

    public class FizzBuzzEngine
    {
        public string Convert(int number)
        {
            var result = string.Empty;
            result += number % 3 == 0 ? "Fizz" : string.Empty;
            result += number % 5 == 0 ? "Buzz" : string.Empty;
            result += IsPrime(number) ? "Whiz" : string.Empty;

            if (result == string.Empty)
            {
                result = number.ToString();
            }
            return result;
        }

        
        public static bool IsPrime(int number)
        {
            if (number == 1)
            {
                return false;
            }

            for (int i = 2; i <= number/2; i++)
                if (number % i == 0)
                    return false;
            return true;
        }
    }
}   