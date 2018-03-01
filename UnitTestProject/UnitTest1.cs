using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreRobots;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod()
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


            // LineTemplate l = new LineTemplate();
            // l.Check2();

        }
    }
}
