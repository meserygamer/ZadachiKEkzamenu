using ZadachiKEkzamenu.Северо_западныйУгол;

namespace ZadachiKEkzamenu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new NorthWestAngleReaderFromFile("NorthWestAngleInput.txt").CreateNorthWestAngle().FindSolution();
        }
    }
}