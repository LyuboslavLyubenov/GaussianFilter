using System;
using System.IO;

namespace GaussianFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                if (args.Length == 2)
                {
                    new GaussianFilter().Apply(args[0], args[1]);
                }
                else if (args.Length == 3)
                {
                    new GaussianFilter().Apply(args[0], args[1], int.Parse(args[2]));
                }
                else if (args.Length == 4)
                {
                    new GaussianFilter().Apply(args[0], args[1], int.Parse(args[2]), float.Parse(args[3]));
                }
            }
            catch (Exception exception)
            {
                File.WriteAllText("logs" + DateTime.Now.ToBinary(), exception.ToString());
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }
    }
}