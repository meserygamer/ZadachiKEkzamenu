using ZadachiKEkzamenu.Метод_джонсона;
using ZadachiKEkzamenu.Минимальный_элемент;
using ZadachiKEkzamenu.Северо_западныйУгол;

namespace ZadachiKEkzamenu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new NorthWestAngleReaderFromFile("NorthWestAngleInput.txt").CreateNorthWestAngle().FindSolution();
            new MinElemFromFile("MinElemInput.txt").CreateMinElemFromFile().FindSolution();
            new ZadachaJohnsonaFromFile("MethodJohnsonaInput.txt").CreateZadachaJohnsona().FindSolution();
        }
    }
}