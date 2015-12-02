﻿using System.Collections.Generic;
using HAL.Simulator;
using NUnit.Framework;
using WPILib.Exceptions;
using WPILib.Interfaces;
// ReSharper disable UnusedVariable

namespace WPILib.Tests
{
    [TestFixture]
    public class TestEncoders : TestBase
    {

        public void CheckConfig(dynamic config, uint aMod, uint aPin, bool aAtr, uint bMod, uint bPin, bool bAtr)
        {
            Assert.AreEqual(aMod, config["ASource_Module"]);
            Assert.AreEqual(aPin, config["ASource_Channel"]);
            Assert.AreEqual(aAtr, config["ASource_AnalogTrigger"]);
            Assert.AreEqual(bMod, config["BSource_Module"]);
            Assert.AreEqual(bPin, config["BSource_Channel"]);
            Assert.AreEqual(bAtr, config["BSource_AnalogTrigger"]);

        }

        [Test]
        public void TestEncoderOverAllocation()
        {
            List<Encoder> encoders = new List<Encoder>();
            for (int i = 0; i < NumEncoders; i++)
            {
                encoders.Add(new Encoder(i, i + NumEncoders));
            }

            Encoder enc = null;
            Assert.Throws<AllocationException>(() =>
            {
                enc = new Encoder(NumEncoders, NumEncoders + NumEncoders);
            });
            enc?.Dispose();

            foreach (var encoder in encoders)
            {
                encoder.Dispose();
            }

        }

        [Test]
        public void TestEncoderAllocateAll()
        {
            List<Encoder> encoders = new List<Encoder>();
            for (int i = 0; i < NumEncoders; i++)
            {
                encoders.Add(new Encoder(i, i + NumEncoders));
            }

            foreach (var encoder in encoders)
            {
                encoder.Dispose();
            }

        }

        [Test]
        public void TestChannelChannelInit()
        {
            using (Encoder x = new Encoder(1, 2))
            {
                Assert.IsTrue(SimData.Encoder[0].Initialized);
                CheckConfig(SimData.Encoder[0].Config, 0 ,1 , false, 0 ,2, false);
                Assert.IsFalse(SimData.Encoder[0].ReverseDirection);
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsTrue(SimData.DIO[2].Initialized);
            }
            Assert.IsFalse(SimData.Encoder[0].Initialized);
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
        }

        [Test]
        public void TestChannelChannelReverseInit()
        {
            using (Encoder x = new Encoder(1, 2, true))
            {
                Assert.IsTrue(SimData.Encoder[0].Initialized);
                CheckConfig(SimData.Encoder[0].Config, 0, 1, false, 0, 2, false);
                Assert.IsTrue(SimData.Encoder[0].ReverseDirection);
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsTrue(SimData.DIO[2].Initialized);
            }
            Assert.IsFalse(SimData.Encoder[0].Initialized);
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
        }

        [Test]
        public void TestChannelChannelReverseTypeInit()
        {
            using (Encoder x = new Encoder(1, 2, true, EncodingType.K4X))
            {
                Assert.IsTrue(SimData.Encoder[0].Initialized);
                CheckConfig(SimData.Encoder[0].Config, 0, 1, false, 0, 2, false);
                Assert.IsTrue(SimData.Encoder[0].ReverseDirection);
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsTrue(SimData.DIO[2].Initialized);
            }
            Assert.IsFalse(SimData.Encoder[0].Initialized);
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
        }

        [Test]
        public void TestChannelChannelChannelReverseInit()
        {
            using (Encoder x = new Encoder(1, 2, 3, true))
            {
                Assert.IsTrue(SimData.Encoder[0].Initialized);
                CheckConfig(SimData.Encoder[0].Config, 0, 1, false, 0, 2, false);
                Assert.IsTrue(SimData.Encoder[0].ReverseDirection);
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsTrue(SimData.DIO[2].Initialized);
                Assert.IsTrue(SimData.DIO[3].Initialized);
            }
            Assert.IsFalse(SimData.Encoder[0].Initialized);
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
            Assert.IsFalse(SimData.DIO[3].Initialized);
        }

        [Test]
        public void TestChannelChannelChannelInit()
        {
            using (Encoder x = new Encoder(1, 2, 3))
            {
                Assert.IsTrue(SimData.Encoder[0].Initialized);
                CheckConfig(SimData.Encoder[0].Config, 0, 1, false, 0, 2, false);
                Assert.IsFalse(SimData.Encoder[0].ReverseDirection);
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsTrue(SimData.DIO[2].Initialized);
                Assert.IsTrue(SimData.DIO[3].Initialized);
            }
            Assert.IsFalse(SimData.Encoder[0].Initialized);
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
            Assert.IsFalse(SimData.DIO[3].Initialized);
        }

        [Test]
        public void TestSourceSourceInit()
        {
            using (var s1 = new DigitalInput(1))
            {
                using (var s2 = new DigitalInput(2))
                {
                    using (var x = new Encoder(s1, s2))
                    {
                        Assert.IsTrue(SimData.Encoder[0].Initialized);
                        CheckConfig(SimData.Encoder[0].Config, 0, 1, false, 0, 2, false);
                        Assert.IsFalse(SimData.Encoder[0].ReverseDirection);
                    }
                    Assert.IsFalse(SimData.Encoder[0].Initialized);
                    Assert.IsTrue(SimData.DIO[1].Initialized);
                    Assert.IsTrue(SimData.DIO[2].Initialized);
                }
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsFalse(SimData.DIO[2].Initialized);
            }
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
        }

        [Test]
        public void TestSourceSourceReverseTypeInit()
        {
            using (var s1 = new DigitalInput(1))
            {
                using (var s2 = new DigitalInput(2))
                {
                    using (var x = new Encoder(s1, s2, true, EncodingType.K4X))
                    {
                        Assert.IsTrue(SimData.Encoder[0].Initialized);
                        CheckConfig(SimData.Encoder[0].Config, 0, 1, false, 0, 2, false);
                        Assert.IsTrue(SimData.Encoder[0].ReverseDirection);
                    }
                    Assert.IsFalse(SimData.Encoder[0].Initialized);
                    Assert.IsTrue(SimData.DIO[1].Initialized);
                    Assert.IsTrue(SimData.DIO[2].Initialized);
                }
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsFalse(SimData.DIO[2].Initialized);
            }
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
        }

        [Test]
        public void TestSourceSourceSourceReverseInit()
        {
            using (var s1 = new DigitalInput(1))
            {
                using (var s2 = new DigitalInput(2))
                {
                    using (var s3 = new DigitalInput(3))
                    {
                        using (var x = new Encoder(s1, s2, s3, true))
                        {
                            Assert.IsTrue(SimData.Encoder[0].Initialized);
                            CheckConfig(SimData.Encoder[0].Config, 0, 1, false, 0, 2, false);
                            Assert.IsTrue(SimData.Encoder[0].ReverseDirection);
                        }
                        Assert.IsFalse(SimData.Encoder[0].Initialized);
                        Assert.IsTrue(SimData.DIO[1].Initialized);
                        Assert.IsTrue(SimData.DIO[2].Initialized);
                        Assert.IsTrue(SimData.DIO[3].Initialized);
                    }
                }
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsFalse(SimData.DIO[2].Initialized);
                Assert.IsFalse(SimData.DIO[3].Initialized);
            }
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
            Assert.IsFalse(SimData.DIO[3].Initialized);
        }

        [Test]
        public void TestSourceSourceSourceInit()
        {
            using (var s1 = new DigitalInput(1))
            {
                using (var s2 = new DigitalInput(2))
                {
                    using (var s3 = new DigitalInput(3))
                    {
                        using (var x = new Encoder(s1, s2, s3))
                        {
                            Assert.IsTrue(SimData.Encoder[0].Initialized);
                            CheckConfig(SimData.Encoder[0].Config, 0, 1, false, 0, 2, false);
                            Assert.IsFalse(SimData.Encoder[0].ReverseDirection);
                        }
                        Assert.IsFalse(SimData.Encoder[0].Initialized);
                        Assert.IsTrue(SimData.DIO[1].Initialized);
                        Assert.IsTrue(SimData.DIO[2].Initialized);
                        Assert.IsTrue(SimData.DIO[3].Initialized);
                    }
                }
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsFalse(SimData.DIO[2].Initialized);
                Assert.IsFalse(SimData.DIO[3].Initialized);
            }
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
            Assert.IsFalse(SimData.DIO[3].Initialized);
        }

        [Test]
        public void TestEncoderCreate2X()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K2X))
            {
                Assert.IsFalse(SimData.Encoder[0].Initialized);
                Assert.IsTrue(SimData.Counter[0].Initialized);
                Assert.AreEqual(2, SimData.Counter[0].AverageSize);

                Assert.AreEqual(true, SimData.Counter[0].UpRisingEdge);
                Assert.AreEqual(true, SimData.Counter[0].UpFallingEdge);

                Assert.AreEqual(false, SimData.Counter[0].DownRisingEdge);
                Assert.AreEqual(true, SimData.Counter[0].DownFallingEdge);
                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsTrue(SimData.DIO[2].Initialized);
            }
            Assert.IsFalse(SimData.Encoder[0].Initialized);
            Assert.IsFalse(SimData.Counter[0].Initialized);
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
        }

        [Test]
        public void TestEncoderCreate1X()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K1X))
            {
                Assert.IsFalse(SimData.Encoder[0].Initialized);
                Assert.IsTrue(SimData.Counter[0].Initialized);
                Assert.AreEqual(1, SimData.Counter[0].AverageSize);

                Assert.AreEqual(true, SimData.Counter[0].UpRisingEdge);
                Assert.AreEqual(false, SimData.Counter[0].UpFallingEdge);

                Assert.AreEqual(false, SimData.Counter[0].DownRisingEdge);
                Assert.AreEqual(true, SimData.Counter[0].DownFallingEdge);

                Assert.IsTrue(SimData.DIO[1].Initialized);
                Assert.IsTrue(SimData.DIO[2].Initialized);
            }
            Assert.IsFalse(SimData.Encoder[0].Initialized);
            Assert.IsFalse(SimData.Counter[0].Initialized);
            Assert.IsFalse(SimData.DIO[1].Initialized);
            Assert.IsFalse(SimData.DIO[2].Initialized);
        }

        [Test]
        public void TestEncoderGet4X()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K4X))
            {
                SimData.Encoder[0].Count = 1234;
                Assert.AreEqual(1234 / 4, x.Get());
            }
        }

        [Test]
        public void TestEncoderGet2X()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K2X))
            {
                
                SimData.Counter[0].Count = 1234;
                Assert.AreEqual(1234 / 2, x.Get());
            }
        }

        [Test]
        public void TestEncoderGet1X()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K1X))
            {
                SimData.Counter[0].Count = 1234;
                Assert.AreEqual(1234, x.Get());
            }
        }
    }
}
