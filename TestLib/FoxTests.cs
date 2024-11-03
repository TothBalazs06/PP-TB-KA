using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntitesLib;

namespace TestLib
{
    [TestClass]
    public class FoxTests
    {
        [TestMethod]
        public void EatRabbit_IncreasesFoxEnergy()
        {
            var fox = new Fox();
            int initialEnergy = fox.Energy;

            fox.EatRabbit();

            Assert.IsTrue(fox.Energy > initialEnergy, "Ha a róka evett az energiának nőnie kell.");
        }

        [TestMethod]
        public void Survive_ReturnsFalseWhenEnergyIsLow()
        {
            var fox = new Fox();

            for (int i = 0; i < 11; i++) fox.Survive();

            Assert.IsFalse(fox.IsAlive, "Ha az energia a megadott alá esik, akkor a rókának meg kell halnia.");
        }
    }
}