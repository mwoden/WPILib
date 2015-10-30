﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HAL_Base;
using WPILib.LiveWindows;
using static HAL_Base.HAL;
using static System.Console;

namespace WPILib.Extras
{
    /// <summary>
    /// The call context for the LabVIEW Robot Disabled and Teleop calls
    /// </summary>
    public enum CallContext
    {
        ///Called on the first time code enters the mode
        Init,
        ///Called every time after the first time.
        Execute,
    }

    public class LabVIEWRobot : RobotBase
    {
        private bool m_disabledInitialized;
        private bool m_autonomousInitialized;
        private bool m_teleopInitialized;
        private bool m_testInitialized;

        private Thread m_secondaryThread;

        public LabVIEWRobot()
        {
            m_autonomousInitialized = false;
            m_disabledInitialized = false;
            m_teleopInitialized = false;
            m_testInitialized = false;
        }

        ///<inheritdoc/>
        protected override void Prestart()
        {

        }

        ///<inheritdoc/>
        public override void StartCompetition()
        {
            Report(ResourceType.kResourceType_Framework, Instances.kFramework_Iterative);

            RobotInit();

            HALNetworkCommunicationObserveUserProgramStarting();

            LiveWindow.SetEnabled(false);

            while (true)
            {
                if (IsTest)
                {
                    // call TestInit() if we are now just entering test mode from either
                    // a different mode or from power-on
                    if (!m_testInitialized)
                    {
                        LiveWindow.SetEnabled(true);
                        CheckThread();
                        InitializeTest();
                        m_testInitialized = true;
                        m_autonomousInitialized = false;
                        m_teleopInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramTest();
                    }
                }
                else if (IsDisabled)
                {
                    // call DisabledInit() if we are now just entering disabled mode from
                    // either a different mode or from power-on
                    if (!m_disabledInitialized)
                    {
                        LiveWindow.SetEnabled(false);
                        CheckThread();
                        Disabled(CallContext.Init);
                        m_disabledInitialized = true;
                        // reset the initialization flags for the other modes
                        m_autonomousInitialized = false;
                        m_teleopInitialized = false;
                        m_testInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramDisabled();
                        Disabled(CallContext.Execute);
                    }
                }
                else if (IsAutonomous)
                {
                    // call Autonomous_Init() if this is the first time
                    // we've entered autonomous_mode
                    if (!m_autonomousInitialized)
                    {
                        LiveWindow.SetEnabled(false);
                        // KBS NOTE: old code reset all PWMs and relays to "safe values"
                        // whenever entering autonomous mode, before calling
                        // "Autonomous_Init()"
                        CheckThread();
                        InitializeAuto();
                        m_autonomousInitialized = true;
                        m_testInitialized = false;
                        m_teleopInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramAutonomous();
                    }
                }
                else
                {
                    // call Teleop_Init() if this is the first time
                    // we've entered teleop_mode
                    if (!m_teleopInitialized)
                    {
                        LiveWindow.SetEnabled(false);
                        CheckThread();
                        Teleoperated(CallContext.Init);
                        m_teleopInitialized = true;
                        m_testInitialized = false;
                        m_autonomousInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        //HAL.NetworkCommunicationObserveUserProgramTeleop();
                        HALNetworkCommunicationObserveUserProgramTeleop();
                        Teleoperated(CallContext.Execute);
                    }
                }
                m_ds.WaitForData();
            }
        }

        /// <summary>
        /// This method is called at the start of autonomous enabled and runs in a seperate thread.
        /// </summary>
        /// <remarks>
        /// This is similar to how LabVIEW does its autonomous. As soon as the robot leave autonomous mode,
        /// the thread will be aborted. So make sure to not put any code that is not safe to be aborted.
        /// </remarks>
        public virtual void Autonomous()
        {
            WriteLine($"Default {nameof(LabVIEWRobot)}.{nameof(Autonomous)} method... Overload me!");
        }

        /// <summary>
        /// This method is called for every packet sent in Teleoperated Enabled. Use the callContext parameter
        /// to determine if initial call or execution calls.
        /// </summary>
        /// <param name="callContext">Init if first call, otherwise Execute</param>
        public virtual void Teleoperated(CallContext callContext)
        {
            WriteLine($"Default {nameof(LabVIEWRobot)}.{nameof(Teleoperated)} method... Overload me!");
        }

        /// <summary>
        /// This method is called for every packet sent in Disabled. Use the callContext parameter
        /// to determine if initial call or execution calls.
        /// </summary>
        /// <param name="callContext">Init if first call, otherwise Execute</param>
        public virtual void Disabled(CallContext callContext)
        {
            WriteLine($"Default {nameof(LabVIEWRobot)}.{nameof(Disabled)} method... Overload me!");
        }

        /// <summary>
        /// This method is called at the start of test mode and runs in a seperate thread. Note that
        /// this will run in both Test Disabled and Test Enabled. 
        /// </summary>
        /// <remarks>
        /// This is similar to how LabVIEW does its test mode. As soon as the robot leave autonomous mode,
        /// the thread will be aborted. So make sure to not put any code that is not safe to be aborted.
        /// The method runs in both Test Enabled and Test Disabled, because this is similar to how LabVIEW
        /// does its test modes.
        /// </remarks>
        public virtual void Test()
        {
            WriteLine($"Default {nameof(LabVIEWRobot)}.{nameof(Test)} method... Overload me!");
        }


        private bool NextPeriodReady => m_ds.NewControlData;

        ///<inheritdoc/>
        public virtual void RobotInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(RobotInit)} method... Overload me!");
        }

        private void InitializeTest()
        {
            m_secondaryThread = new Thread(Test);
            m_secondaryThread.IsBackground = false;
            m_secondaryThread.Start();
        }

        private void InitializeAuto()
        {
            m_secondaryThread = new Thread(Autonomous);
            m_secondaryThread.IsBackground = false;
            m_secondaryThread.Start();
        }

        private void CheckThread()
        {
            m_secondaryThread?.Abort();
            m_secondaryThread?.Join();
            m_secondaryThread = null;
        }
    }
}