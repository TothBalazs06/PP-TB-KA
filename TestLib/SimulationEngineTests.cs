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

            Assert.IsNotNull(engine.GetCell(2, 2).Rabbit, "Egy ny�lnak kell lennie a (2,2) koordin�t�n.");
        }

        [TestMethod]
        public void AddFox_AddsFoxAtCorrectLocation()
        {
            var engine = new SimulationEngine(5, 5);

            engine.AddFox(3, 3);

            Assert.IsNotNull(engine.GetCell(3, 3).Fox, "Egy t�k�nak kell lennie a (3,3) koordin�t�n.");
        }

        [TestMethod]
        public void IsWithinBounds_ReturnsTrueForValidCoordinates()
        {
            var engine = new SimulationEngine(5, 5);

            Assert.IsTrue(engine.IsWithinBounds(0, 0), "Bal-Fels� koordin�t�k a hat�ron bel�l kell hogy legyenek.");
            Assert.IsTrue(engine.IsWithinBounds(4, 4), "Jobb-Als� koordin�t�k a hat�ron bel�l kell hogy legyenek.");
        }

        [TestMethod]
        public void IsWithinBounds_ReturnsFalseForInvalidCoordinates()
        {
            var engine = new SimulationEngine(5, 5);

            Assert.IsFalse(engine.IsWithinBounds(-1, 0), "Negat�v X nem szabad hogy a hat�ron bel�l legyen.");
            Assert.IsFalse(engine.IsWithinBounds(0, 5), "Y koordin�ta a hat�ron k�v�l kell lennie.");
        }
    }
}