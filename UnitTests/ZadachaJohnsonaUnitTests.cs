using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadachiKEkzamenu.Метод_джонсона;

namespace UnitTests
{
    public class ZadachaJohnsonaUnitTests
    {
        [Fact]
        public void ZadachaJohnsonaTest1()
        {
            List<string> TestData = new List<string> {"2 3", "8 3", "4 6", "9 5", "6 8", "9 7"};
            Assert.Equal(9, (new ZadachaJohnsona(TestData.ConvertAll(x => new Detal(x.Split(" "))))).FindSolution());
        }

        [Fact]
        public void ZadachaJohnsonaTest2()
        {
            List<string> TestData = new List<string> { "6 3", "8 5", "4 5", "4 2", "5 8", "8 4" };
            Assert.Equal(10, (new ZadachaJohnsona(TestData.ConvertAll(x => new Detal(x.Split(" "))))).FindSolution());
        }
    }
}
