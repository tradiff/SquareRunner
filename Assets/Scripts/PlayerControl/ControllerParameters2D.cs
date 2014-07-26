using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class ControllerParameters2D
{
    public enum JumpBehavior
    {
        CanJumpOnground,
        CanJumpAnywhere,
        CantJump
    }

    public Vector2 MaxVelocity = new Vector2(float.MaxValue, float.MaxValue);

    // character can climb 30 degree angles, but nothing more
    [Range(0, 90)]
    public float SlopeLimit = 30;

    public float Gravity = -25f;

    public JumpBehavior JumpRestrictions;

    public float JumpFrequency = .25f;

    public float JumpMagnitude = 12;
}
