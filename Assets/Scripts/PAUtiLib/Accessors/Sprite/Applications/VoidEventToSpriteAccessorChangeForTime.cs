// Author(s): Paul Calande
// Changes a sprite for a certain amount of time when a VoidEvent is invoked.

using UnityEngine;

public class VoidEventToSpriteAccessorChangeForTime : VoidEventToSingleAccessorChangeForTime
    <Sprite, SpriteAccessor, MonoTimerChangeSpriteForTime, SpriteAccessorChangeForTime>
{ }