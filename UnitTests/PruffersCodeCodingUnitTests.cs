using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadachiKEkzamenu.КодПрюфераКодирование;

namespace UnitTests
{
    public class PruffersCodeCodingUnitTests
    {
        [Fact]
        public void PruffersCodeCodingUnitTest1()
        {
            List<int[]> _graphsEdges = new List<int[]>
            {   new int[] { 1, 2 },
                new int[] { 1, 3 },
                new int[] { 1, 4 },
                new int[] { 2, 5 },
                new int[] { 2, 6 },
                new int[] { 3, 10 },
                new int[] { 5, 7 },
                new int[] { 6, 8 },
                new int[] { 6, 9 }
            };
            Assert.Equal("1,5,2,6,6,2,1,3", (new PruffersCodeCoding(_graphsEdges)).FindSolution());
        }

        [Fact]
        public void PruffersCodeCodingUnitTest2()
        {
            List<int[]> _graphsEdges = new List<int[]>
            {   new int[] { 4, 1 },
                new int[] { 4, 2 },
                new int[] { 3, 4 },
                new int[] { 4, 5 },
                new int[] { 5, 6 },
            };
            Assert.Equal("4,4,4,5", (new PruffersCodeCoding(_graphsEdges)).FindSolution());
        }
    }
}
