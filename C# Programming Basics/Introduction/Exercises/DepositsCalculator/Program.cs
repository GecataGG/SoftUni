internal class Program
{
    private static void Main(string[] args)
    {
        double depositSum = double.Parse(Console.ReadLine());
        int depositLength = int.Parse(Console.ReadLine());
        double yearlyRate = double.Parse(Console.ReadLine());

        double result = depositSum + depositLength * (depositSum * yearlyRate * 0.01 / 12);
        Console.WriteLine(result);
    }
}