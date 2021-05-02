namespace FizzBuzzWhiz.Console
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any())
            {
                args = Enumerable.Range(1, 100).Select(x => x.ToString()).ToArray();
            }

            var engine = new FizzBuzzEngine(new PrimeEvaluation());
            foreach (var arg in args)
            {
                Console.WriteLine(engine.Convert(int.Parse(arg)));
            }

            Console.ReadKey();

        }
    }
}
