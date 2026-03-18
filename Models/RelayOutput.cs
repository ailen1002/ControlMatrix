// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 03月10日 10:03
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ControlMatrix\Models\RelayOutput.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace ControlMatrix.Models;

public enum RelayOutput
{
    ApSwitch = 0,
    TestSwitch = 1,
    SnowSwitch = 2,
    SilentSwitch = 3,
    Drm1Switch = 4,
    Drm2Switch = 5,
    ForcedStopSwitch = 6,
    NumCompSwitch = 7,
    Boot = 8,
    HicPower = 9,
    MiconResetSwitch = 10,
    HicSwitch = 11,
    HighPressureSwitch = 12,
    CompTripSwitch = 13,
    ConstantSpeedMotor = 14,
    AcVoltageDetectionSwitch = 15
}