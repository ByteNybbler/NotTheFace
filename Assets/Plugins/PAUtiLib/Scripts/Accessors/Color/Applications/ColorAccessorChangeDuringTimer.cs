// Author(s): Paul Calande
// Changes an accessor's color based on whether a MonoTimer is running.

using UnityEngine;

public class ColorAccessorChangeDuringTimer : SingleAccessorChangeDuringTimer
    <Color, ColorAccessor>
{ }