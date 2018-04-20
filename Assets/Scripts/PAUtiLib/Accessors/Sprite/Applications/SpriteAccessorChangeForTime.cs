// Author(s): Paul Calande
// Changes a sprite for a certain amount of time before changing it back.

using UnityEngine;

public class SpriteAccessorChangeForTime : SingleAccessorChangeForTime
    <Sprite, SpriteAccessor, MonoTimerChangeSpriteForTime>
{ }