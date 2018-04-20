// Author(s): Paul Calande
// Changes a color for a certain amount of time when a VoidEvent is invoked.

using UnityEngine;

public class VoidEventToColorAccessorChangeForTime : VoidEventToSingleAccessorChangeForTime
    <Color, ColorAccessor, MonoTimerChangeColorForTime, ColorAccessorChangeForTime>
{ }