﻿
namespace NumberInRange
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());

            if (number != 0 && -100 <= number && number <= 100) Console.WriteLine("Yes");
            else Console.WriteLine("No");

        }
    }
}
