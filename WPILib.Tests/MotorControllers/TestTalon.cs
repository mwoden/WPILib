﻿using System.Collections.Generic;
using HAL_Simulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests.MotorControllers
{
    [TestClass]
    public class TestTalon
    {
        [ClassInitialize]
        public static void Initialize(TestContext c)
        {
            TestBase.StartCode();
        }

        [ClassCleanup]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [TestMethod]
        public void TestTalonInitialized()
        {
            using (Talon t = new Talon(2))
                Assert.AreEqual(HalData()["pwm"][2]["type"], "talon");
        }

        [TestMethod]
        public void TestTalonStarts0()
        {
            using (Talon t = new Talon(2))
            {
                Assert.AreEqual(t.Get(), 0);
            }
        }

        [TestMethod]
        public void TestTalonSet()
        {
            using (Talon t = new Talon(2))
            {
                t.Set(1);
                Assert.AreEqual(t.Get(), 1);
            }
        }

        [TestMethod]
        public void TestPWMHelpers()
        {
            using (Talon t = new Talon(2))
            {
                t.Set(1);
                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(Talon), HalData()["pwm"][2]["raw_value"]), 1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), 1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], 1);
            }
        }

        [TestMethod]
        public void TestPIDWrite()
        {
            using (Talon t = new Talon(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(t.Get(), -1);
            }
        }

        [TestMethod]
        public void TestPWMHelpersPID()
        {
            using (Talon t = new Talon(2))
            {
                t.PidWrite(-1);

                Assert.AreEqual(PWMHelpers.ReverseByType(nameof(Talon), HalData()["pwm"][2]["raw_value"]), -1);
                Assert.AreEqual(PWMHelpers.ReverseByType(2), -1);
                Assert.AreEqual(HalData()["pwm"][2]["value"], -1);
            }
        }
    }
}
