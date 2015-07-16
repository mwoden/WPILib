﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPILib.Tests
{
    [TestClass]
    public class TestUtility
    {
        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            TestBase.StartCode();
        }

        [ClassCleanup]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private Dictionary<dynamic, dynamic> HalData()
        {
            return HAL_Base.HAL.halData;
        }

        [TestMethod]
        public void TestGetFPGAVersion()
        {
            Assert.AreEqual(2015, Utility.GetFPGAVersion());
        }

        public void TestFPGAGetRevision()
        {
            Assert.AreEqual(0, Utility.GetFPGARevision());
        }

        public void TestGetUserButton()
        {
            HalData()["fpga_button"] = true;
            Assert.IsTrue(Utility.GetUserButton());
        }
    }
}