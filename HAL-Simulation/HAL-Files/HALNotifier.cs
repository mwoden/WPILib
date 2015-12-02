﻿using System;
using System.Collections.Generic;
using System.Threading;
using HAL_Base;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALNotifier
    {
        private static readonly List<Notifier> Notifiers = new List<Notifier>();

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALNotifier.InitializeNotifier = initializeNotifier;
            HAL_Base.HALNotifier.CleanNotifier = cleanNotifier;
            HAL_Base.HALNotifier.UpdateNotifierAlarm = updateNotifierAlarm;
        }

        [CalledSimFunction]
        public static IntPtr initializeNotifier(Action<uint, IntPtr> ProcessQueue, IntPtr param, ref int status)
        {
            status = 0;
            Notifier notifier = new Notifier
            {
                Callback = ProcessQueue,
                Parameter = param
            };
            Notifiers.Add(notifier);
            return (IntPtr)Notifiers.IndexOf(notifier);
        }

        [CalledSimFunction]
        public static void cleanNotifier(IntPtr notifier_pointer, ref int status)
        {
            status = 0;
            Notifier notifier = Notifiers[notifier_pointer.ToInt32()];
            if (notifier.Alarm != null && notifier.Alarm.IsAlive)
            {
                notifier.Alarm.Abort();
            }
            notifier.Alarm?.Join();
            notifier.Alarm = null;
            notifier.Callback = null;
            Notifiers.Remove(notifier);
        }


        [CalledSimFunction]
        public static void updateNotifierAlarm(IntPtr notifier_pointer, uint triggerTime, ref int status)
        {
            status = 0;
            Notifier notifier = Notifiers[notifier_pointer.ToInt32()];
            if (notifier.Alarm != null && notifier.Alarm.IsAlive)
            {
                notifier.Alarm.Abort();
            }
            notifier.Alarm?.Join();
            notifier.Alarm = new Thread(() =>
            {
                while (triggerTime > SimHooks.GetFPGATime())
                {
                }
                if (notifier.Callback == null)
                    Console.WriteLine("Callback Null");
                notifier.Callback?.Invoke((uint)SimHooks.GetFPGATime(), IntPtr.Zero);
            });
            notifier.Alarm.Start();
        }
    }
}
