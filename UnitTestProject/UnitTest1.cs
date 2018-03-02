using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreRobots;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NextSimpleInput()
        {
            LineTemplate index = new LineTemplate();
            index.AddTemplate("ore/cuprum");
            index.AddTemplate("ore/cuprum/ore/cuprum/oil");
            index.AddTemplate("ore/cuprum/ore/tail");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
        }

        [TestMethod]
        public void Revert()
        {
            LineTemplate index = new LineTemplate();
            index.AddTemplate("ore/cuprum");
            index.AddTemplate("ore/cuprum/ore/cuprum/oil");
            index.AddTemplate("ore/cuprum/ore/tail");
            index.Next("ore");
            index.Preverios();
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
            index.Preverios();
            index.Next("tail");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("tail");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("tail");
            index.Preverios();
            index.Next("tail");
            Assert.AreEqual<int>(3, index.Cicles);
        }
        [TestMethod]
        public void FullRevert()
        {
            LineTemplate index = new LineTemplate();
            index.AddTemplate("ore/cuprum");
            index.AddTemplate("ore/cuprum/ore/cuprum/oil");
            index.AddTemplate("ore/cuprum/ore/tail");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Preverios();
            index.Preverios();
            index.Preverios();
            index.Preverios();
            index.Preverios();
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("oil");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("ore");
            index.Next("cuprum");
            index.Next("oil");
            Assert.AreEqual<int>(2, index.Cicles);
        }
    }
}
