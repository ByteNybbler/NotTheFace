// Author(s): Paul Calande
// Changes a color for a certain amount of time before changing it back.

using UnityEngine;

public class ColorAccessorChangeForTime : SingleAccessorChangeForTime
    <Color, ColorAccessor, MonoTimerChangeColorForTime>
{ }