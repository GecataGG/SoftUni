﻿internal class Program
{
    private static void Main(string[] args)
    {
        int pens = int.Parse(Console.ReadLine());
        int markers = int.Parse(Console.ReadLine());
        int diluent = int.Parse(Console.ReadLine());
        int discountPercentage = int.Parse(Console.ReadLine());

        double total = (pens * 5.8 + markers * 7.2 + diluent * 1.2) * (1 - discountPercentage * 0.01);
        Console.WriteLine(total);
    }
}