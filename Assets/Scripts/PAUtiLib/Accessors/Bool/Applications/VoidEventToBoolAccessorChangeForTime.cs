// Author(s): Paul Calande
// Changes a boolean for a certain amount of time when a VoidEvent is invoked.

public class VoidEventToBoolAccessorChangeForTime : VoidEventToSingleAccessorChangeForTime
    <bool, BoolAccessor, MonoTimerChangeBoolForTime, BoolAccessorChangeForTime>
{ }