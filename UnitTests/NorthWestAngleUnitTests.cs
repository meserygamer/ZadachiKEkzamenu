using ZadachiKEkzamenu.—еверо_западный”гол;

namespace UnitTests
{
    public class NorthWestAngleUnitTests
    {
        [Fact]
        public void Test1()
        {
            int[,] LossMatrix = new int[,] {{5,3,1},{3,2,4},{4,1,2}};
            int[] PowerOfConsumers = new int[] {15, 20, 25};
            int[] PowerOfSuppliars = new int[] {10, 20, 30};
            NorthWestAngle Zad = new NorthWestAngle(PowerOfSuppliars, PowerOfConsumers, LossMatrix);
            Zad._supplyMatrix = Zad.FindDistribution();
            Assert.Equal(165,Zad.FindSumOfDistribution());
        }

        [Fact]
        public void Test2()
        {
            int[,] LossMatrix = new int[,] { { 7, 6, 4 }, { 3, 8, 5 }, { 2, 3, 7 } };
            int[] PowerOfConsumers = new int[] { 120, 100, 80 };
            int[] PowerOfSuppliars = new int[] { 90, 90, 120 };
            NorthWestAngle Zad = new NorthWestAngle(PowerOfSuppliars, PowerOfConsumers, LossMatrix);
            Zad._supplyMatrix = Zad.FindDistribution();
            Assert.Equal(2050, Zad.FindSumOfDistribution());
        }
    }
}