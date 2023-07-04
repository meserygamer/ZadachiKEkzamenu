using System.Diagnostics;
using ZadachiKEkzamenu.КодПрюфераКодирование;
using ZadachiKEkzamenu.Метод_джонсона;
using ZadachiKEkzamenu.Минимальный_элемент;
using ZadachiKEkzamenu.Северо_западныйУгол;

namespace ZadachiKEkzamenu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(File.Open("MinElem.txt",FileMode.Create)));
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            new NorthWestAngleReaderFromFile("NorthWestAngleInput.txt").CreateNorthWestAngle().FindSolution();
            new MinElemFromFile("MinElemInput.txt").CreateMinElemFromFile().FindSolution();
            new ZadachaJohnsonaFromFile("MethodJohnsonaInput.txt").CreateZadachaJohnsona().FindSolution();
            new PruffersCodeFromFile("PruffersCodeCodingInput.txt").CreatePruffersCodeCoding().FindSolution();
        }
    }
}