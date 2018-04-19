// Author(s): Paul Calande
// Script that causes a color to change when damage is taken.

using UnityEngine;

public class DamageToColorAccessorChangeForTime : DamageToSingleAccessorChangeForTime
    <Color, ColorAccessor, ColorAccessorChangeForTime>
{ }