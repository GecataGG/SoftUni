

namespace EvenPowersOf2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = 0, val = 1; i <= n; i += 2, val *= 4) Console.WriteLine(val);
        }
    }
}
