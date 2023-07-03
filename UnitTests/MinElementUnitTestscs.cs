using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadachiKEkzamenu.Минимальный_элемент;

namespace UnitTests
{
    public class MinElementUnitTestscs
    {
        [Fact]
        public void MinElementTest1()
        {
            int[] suppliarsPower = new int[]{10,20,30};
            int[] consumerPower = new int[] {15,20,25};
            int[,] lossMatrix = new int[,] { {5,3,1},{3,2,4},{4,1,2}};
            Assert.Equal(115,(new MinElem(suppliarsPower, consumerPower, lossMatrix)).FindSolution());
        }

        [Fact]
        public void MinElementTest3()
        {
            int[] suppliarsPower = new int[] { 90, 90, 120 };
            int[] consumerPower = new int[] { 120, 100, 80 };
            int[,] lossMatrix = new int[,] { { 7, 6, 4 }, { 3, 8, 5 }, { 2, 3, 7 } };
            Assert.Equal(1390, (new MinElem(suppliarsPower, consumerPower, lossMatrix)).FindSolution());
        }
    }
}
