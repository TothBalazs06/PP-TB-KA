using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntitesLib;

namespace TestLib
{
    [TestClass]
    public class RabbitTests
    {
        [TestMethod]
        public void Eat_GrassIncreasesEnergy()
        {
            var rabbit = new Rabbit();
            int initialEnergy = rabbit.Energy;

            rabbit.Eat(GrassState.Young);

            Assert.IsTrue(rabbit.Energy > initialEnergy, "Ha a nyúl megevett egy füvet akkor az energia szintnek nőnie kell.");
        }

        [TestMethod]
        public void Survive_ReturnsFalseWhenEnergyIsLow()
        {
            var rabbit = new Rabbit();

            for (int i = 0; i < 11; i++) rabbit.Survive();

            Assert.IsFalse(rabbit.IsAlive, "A nyúlnak halottnak kéne lennie ha az energia kisebb mint a megadott.");
        }
    }
}