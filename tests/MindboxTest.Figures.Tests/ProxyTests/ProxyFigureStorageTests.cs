using MindboxTest.Figures.Circle;
using MindboxTest.Figures.Proxy;

using NUnit.Framework;

using System;

namespace MindboxTest.Figures.Tests.ProxyTests
{
    public class ProxyFigureStorageTests
    {
        private readonly ProxyFigureStorage _storage = new ProxyFigureStorage();

        [Test]
        public void AddCircle_GetDescriptionTypeCircle_CircleDescription()
        {
            AddCircle(); 

            Type descType = _storage.GetDescriptionType("circle");

            Assert.AreEqual(typeof(CircleDescription), descType);
        }

        [Test]
        public void AddCircle_GetCircleProcessor_NotNull()
        {
            AddCircle();

            ProxyFigureProcessors processor = _storage.GetProxyFigureProcessor(new CircleDescription());

            Assert.IsNotNull(processor);
        }

        [Test]
        public void NoInitialization_GetCircleProcessor_Null()
        {
            ProxyFigureStorage.ClearAllFigureProcessors();

            ProxyFigureProcessors processor = _storage.GetProxyFigureProcessor(new CircleDescription());

            Assert.IsNull(processor);
        }

        [Test]
        public void NoInitialization_GetDescriptionTypeCircle_Null()
        {
            ProxyFigureStorage.ClearAllFigureProcessors();

            Type descType = _storage.GetDescriptionType("circle");

            Assert.IsNull(descType);
        }

        [Test]
        public void DoubleAddCircle_GetDescriptionTypeCircle_CircleDescription()
        {
            DoubleAddCircle();

            Type descType = _storage.GetDescriptionType("circle");

            Assert.AreEqual(typeof(CircleDescription), descType);
        }

        [Test]
        public void DoubleAddCircle_GetCircleProcessor_NotNull()
        {
            DoubleAddCircle();

            ProxyFigureProcessors processor = _storage.GetProxyFigureProcessor(new CircleDescription());

            Assert.IsNotNull(processor);
        }

        private static void AddCircle()
        {
            ProxyFigureStorage.ClearAllFigureProcessors();
            ProxyFigureStorage.AddFigureProcessors(new CircleValidator(), new CircleCalculator(), "circle");
        }

        private static void DoubleAddCircle()
        {
            ProxyFigureStorage.ClearAllFigureProcessors();
            ProxyFigureStorage.AddFigureProcessors(new CircleValidator(), new CircleCalculator(), "circle"); 
            ProxyFigureStorage.AddFigureProcessors(new CircleValidator(), new CircleCalculator(), "circle");
        }
    }
}
