using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntitesLib;
using FoxAndRabbit_PPTBKA;

namespace TestLib
{
    [TestClass]
    public class SimulationEngineTests
    {
        [TestMethod]
        public void AddRabbit_AddsRabbitAtCorrectLocation()
        {
            var engine = new SimulationEngine(5, 5);

            engine.AddRabbit(2, 2);

            Assert.IsNotNull(engine.GetCell(2, 2).Rabbit, "Egy nyúlnak kell lennie a (2,2) koordinátán.");
        }

        [TestMethod]
        public void AddFox_AddsFoxAtCorrectLocation()
        {
            var engine = new SimulationEngine(5, 5);

            engine.AddFox(3, 3);

            Assert.IsNotNull(engine.GetCell(3, 3).Fox, "Egy tókának kell lennie a (3,3) koordinátán.");
        }

        [TestMethod]
        public void IsWithinBounds_ReturnsTrueForValidCoordinates()
        {
            var engine = new SimulationEngine(5, 5);

            Assert.IsTrue(engine.IsWithinBounds(0, 0), "Bal-Felsõ koordináták a határon belül kell hogy legyenek.");
            Assert.IsTrue(engine.IsWithinBounds(4, 4), "Jobb-Alsó koordináták a határon belül kell hogy legyenek.");
        }

        [TestMethod]
        public void IsWithinBounds_ReturnsFalseForInvalidCoordinates()
        {
            var engine = new SimulationEngine(5, 5);

            Assert.IsFalse(engine.IsWithinBounds(-1, 0), "Negatív X nem szabad hogy a határon belül legyen.");
            Assert.IsFalse(engine.IsWithinBounds(0, 5), "Y koordináta a határon kívül kell lennie.");
        }
    }
}