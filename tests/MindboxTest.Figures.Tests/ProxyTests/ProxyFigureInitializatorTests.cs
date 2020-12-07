using MindboxTest.Figures.Circle;
using MindboxTest.Figures.Proxy;
using MindboxTest.Figures.Triangle;
using NUnit.Framework;

using System;

namespace MindboxTest.Figures.Tests.ProxyTests
{
    public class ProxyFigureInitializatorTests
    {
        static ProxyFigureInitializatorTests()
        {
            ProxyFigureInitializator.Initialize();
        }

        [Test]
        public void GetByCircleCode_IsCircleDescription()
        {
            var storage = new ProxyFigureStorage();

            Type circleDescriptionType = storage.GetDescriptionType("circle");

            Assert.AreEqual(typeof(CircleDescription), circleDescriptionType);
        }

        [Test]
        public void GetByTriangleCode_IsTriangleDescription()
        {
            var storage = new ProxyFigureStorage();

            Type circleDescriptionType = storage.GetDescriptionType("triangle");

            Assert.AreEqual(typeof(TriangleDescription), circleDescriptionType);
        }
    }
}
