//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCanTalonSRX
    {
        internal static void Initialize(IntPtr library, IDllLoader loader)
        {
            HAL_Base.HALCanTalonSRX.C_TalonSRX_Create = (HAL_Base.HALCanTalonSRX.C_TalonSRX_CreateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_Create"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_CreateDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_Destroy = (HAL_Base.HALCanTalonSRX.C_TalonSRX_DestroyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_Destroy"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_DestroyDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetParam = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetParam"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetParamDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_RequestParam = (HAL_Base.HALCanTalonSRX.C_TalonSRX_RequestParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_RequestParam"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_RequestParamDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponse = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetParamResponse"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32 = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetParamResponseInt32"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32Delegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRate = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetStatusFrameRate"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRateDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaults = (HAL_Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_ClearStickyFaults"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_ClearStickyFaultsDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTemp = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_OverTemp"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_OverTempDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltage = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_UnderVoltage"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltageDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_ForLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_RevLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailure = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailureDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_HardwareFailure"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailureDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_ForSoftLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFault_RevSoftLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTemp = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_OverTemp"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTempDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltage = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_UnderVoltage"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltageDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_ForLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_RevLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_ForSoftLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLim = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLimDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetStckyFault_RevSoftLim"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLimDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottle = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAppliedThrottle"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottleDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErr = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErrDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetCloseLoopErr"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErrDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFeedbackDeviceSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetModeSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetModeSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetModeSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetModeSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEn = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchEn"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEnDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedFor = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedForDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchClosedFor"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedForDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRev = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRevDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetLimitSwitchClosedRev"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRevDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorPosition = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetSensorPosition"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorPositionDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocity = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetSensorVelocity"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetSensorVelocityDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCurrent = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetCurrent"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetCurrentDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabled = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabledDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetBrakeIsEnabled"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabledDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncPosition = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncPosition"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncPositionDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncVel = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncVelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncVel"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncVelDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEvents = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEventsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetEncIndexRiseEvents"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEventsDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadApin = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadApinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadApin"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadApinDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpin = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadBpin"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadBpinDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpin = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetQuadIdxpin"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpinDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOv = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOvDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAnalogInWithOv"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOvDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVel = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetAnalogInVel"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetAnalogInVelDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetTemp = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetTempDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetTemp"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetTempDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBatteryV = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBatteryVDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetBatteryV"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetBatteryVDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetCount = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetResetCount"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetCountDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetFlags = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetFlagsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetResetFlags"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetResetFlagsDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFirmVers = (HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFirmVersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_GetFirmVers"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_GetFirmVersDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetDemand = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetDemandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetDemand"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetDemandDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEn = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetOverrideLimitSwitchEn"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEnDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetFeedbackDeviceSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEn = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRevMotDuringCloseLoopEn"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEnDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeType = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeTypeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetOverrideBrakeType"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeTypeDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetModeSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2 = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetModeSelect2"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetModeSelect2Delegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelect = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetProfileSlotSelect"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelectDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottle = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRampThrottle"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRampThrottleDelegate));

            HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensor = (HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_TalonSRX_SetRevFeedbackSensor"), typeof(HAL_Base.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensorDelegate));

        }

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_Create")]
        public static extern IntPtr c_TalonSRX_Create(int deviceNumber, int controlPeriodMs);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_Destroy")]
        public static extern void c_TalonSRX_Destroy(IntPtr handle);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetParam")]
        public static extern CTR_Code c_TalonSRX_SetParam(IntPtr handle, int paramEnum, double value);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_RequestParam")]
        public static extern CTR_Code c_TalonSRX_RequestParam(IntPtr handle, int paramEnum);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetParamResponse")]
        public static extern CTR_Code c_TalonSRX_GetParamResponse(IntPtr handle, int paramEnum, ref double value);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetParamResponseInt32")]
        public static extern CTR_Code c_TalonSRX_GetParamResponseInt32(IntPtr handle, int paramEnum, ref int value);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetStatusFrameRate")]
        public static extern CTR_Code c_TalonSRX_SetStatusFrameRate(IntPtr handle, uint frameEnum, uint periodMs);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_ClearStickyFaults")]
        public static extern CTR_Code c_TalonSRX_ClearStickyFaults(IntPtr handle);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_OverTemp")]
        public static extern CTR_Code c_TalonSRX_GetFault_OverTemp(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_UnderVoltage")]
        public static extern CTR_Code c_TalonSRX_GetFault_UnderVoltage(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_ForLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_ForLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_RevLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_RevLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_HardwareFailure")]
        public static extern CTR_Code c_TalonSRX_GetFault_HardwareFailure(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_ForSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_ForSoftLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFault_RevSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_RevSoftLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_OverTemp")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_OverTemp(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_UnderVoltage")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_UnderVoltage(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_ForLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_ForLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_RevLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_RevLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_ForSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_ForSoftLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetStckyFault_RevSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_RevSoftLim(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetAppliedThrottle")]
        public static extern CTR_Code c_TalonSRX_GetAppliedThrottle(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetCloseLoopErr")]
        public static extern CTR_Code c_TalonSRX_GetCloseLoopErr(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFeedbackDeviceSelect")]
        public static extern CTR_Code c_TalonSRX_GetFeedbackDeviceSelect(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetModeSelect")]
        public static extern CTR_Code c_TalonSRX_GetModeSelect(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetLimitSwitchEn")]
        public static extern CTR_Code c_TalonSRX_GetLimitSwitchEn(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetLimitSwitchClosedFor")]
        public static extern CTR_Code c_TalonSRX_GetLimitSwitchClosedFor(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetLimitSwitchClosedRev")]
        public static extern CTR_Code c_TalonSRX_GetLimitSwitchClosedRev(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetSensorPosition")]
        public static extern CTR_Code c_TalonSRX_GetSensorPosition(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetSensorVelocity")]
        public static extern CTR_Code c_TalonSRX_GetSensorVelocity(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetCurrent")]
        public static extern CTR_Code c_TalonSRX_GetCurrent(IntPtr handle, ref double param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetBrakeIsEnabled")]
        public static extern CTR_Code c_TalonSRX_GetBrakeIsEnabled(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetEncPosition")]
        public static extern CTR_Code c_TalonSRX_GetEncPosition(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetEncVel")]
        public static extern CTR_Code c_TalonSRX_GetEncVel(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetEncIndexRiseEvents")]
        public static extern CTR_Code c_TalonSRX_GetEncIndexRiseEvents(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetQuadApin")]
        public static extern CTR_Code c_TalonSRX_GetQuadApin(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetQuadBpin")]
        public static extern CTR_Code c_TalonSRX_GetQuadBpin(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetQuadIdxpin")]
        public static extern CTR_Code c_TalonSRX_GetQuadIdxpin(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetAnalogInWithOv")]
        public static extern CTR_Code c_TalonSRX_GetAnalogInWithOv(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetAnalogInVel")]
        public static extern CTR_Code c_TalonSRX_GetAnalogInVel(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetTemp")]
        public static extern CTR_Code c_TalonSRX_GetTemp(IntPtr handle, ref double param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetBatteryV")]
        public static extern CTR_Code c_TalonSRX_GetBatteryV(IntPtr handle, ref double param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetResetCount")]
        public static extern CTR_Code c_TalonSRX_GetResetCount(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetResetFlags")]
        public static extern CTR_Code c_TalonSRX_GetResetFlags(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_GetFirmVers")]
        public static extern CTR_Code c_TalonSRX_GetFirmVers(IntPtr handle, ref int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetDemand")]
        public static extern CTR_Code c_TalonSRX_SetDemand(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetOverrideLimitSwitchEn")]
        public static extern CTR_Code c_TalonSRX_SetOverrideLimitSwitchEn(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetFeedbackDeviceSelect")]
        public static extern CTR_Code c_TalonSRX_SetFeedbackDeviceSelect(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetRevMotDuringCloseLoopEn")]
        public static extern CTR_Code c_TalonSRX_SetRevMotDuringCloseLoopEn(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetOverrideBrakeType")]
        public static extern CTR_Code c_TalonSRX_SetOverrideBrakeType(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetModeSelect")]
        public static extern CTR_Code c_TalonSRX_SetModeSelect(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetModeSelect2")]
        public static extern CTR_Code c_TalonSRX_SetModeSelect2(IntPtr handle, int modeSelect, int demand);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetProfileSlotSelect")]
        public static extern CTR_Code c_TalonSRX_SetProfileSlotSelect(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetRampThrottle")]
        public static extern CTR_Code c_TalonSRX_SetRampThrottle(IntPtr handle, int param);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "c_TalonSRX_SetRevFeedbackSensor")]
        public static extern CTR_Code c_TalonSRX_SetRevFeedbackSensor(IntPtr handle, int param);
    }
}
