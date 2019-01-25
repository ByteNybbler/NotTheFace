// Author(s): Paul Calande
// Angle class with several different read-only repesentations,
// including degrees and radians.
// This class also includes various other angle-related utility functions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Angle : IDeepCopyable<Angle>
{
    // The common constant, 2*pi, is the full circumference of the unit circle.
    public const float TWO_PI = Mathf.PI * 2;

    // Unsigned interval for angles, in degrees: [0, 360).
    public static readonly IntervalFloat INTERVAL_UNSIGNED_DEGREES
        = IntervalFloat.FromStartEnd(0.0f, 360.0f);

    // Signed interval for angles, in degrees: [-180, 180).
    public static readonly IntervalFloat INTERVAL_SIGNED_DEGREES
        = IntervalFloat.FromStartEnd(-180.0f, 180.0f);

    // Unsigned interval for angles, in radians: [0, 2*pi).
    public static readonly IntervalFloat INTERVAL_UNSIGNED_RADIANS
        = IntervalFloat.FromStartEnd(0.0f, Mathf.PI * 2);

    // Signed interval for angles, in radians: [-pi. pi).
    public static readonly IntervalFloat INTERVAL_SIGNED_RADIANS
        = IntervalFloat.FromStartEnd(-Mathf.PI, Mathf.PI);

    [SerializeField]
    [Tooltip("The degree measure of the angle."
        + "The radian measure is also obtained using this value.")]
    float degrees;

    // Private constructors means that a factory method must be used
    // to construct an instance of this class.
    private Angle(float degrees)
    {
        this.degrees = degrees;
    }

    // Factory methods.
    // Constructs an angle from a degree measure.
    public static Angle FromDegrees(float degrees)
    {
        return new Angle(degrees);
    }
    // Constructs an angle from a radian measure.
    public static Angle FromRadians(float radians)
    {
        return new Angle(radians * Mathf.Rad2Deg);
    }
    // Constructs an angle (in signed degrees) from the given heading vector.
    public static Angle FromHeadingVector(Vector2 heading)
    {
        return new Angle(Vector2.SignedAngle(Vector2.right, heading));
    }
    // Returns a wheel's angular velocity based on its linear velocity and radius.
    public static Angle FromAngularVelocity(float linearVelocity, float radius)
    {
        return Angle.FromRadians(linearVelocity / radius);
    }
    // Returns the signed angle that faces the end point from the start point.
    public static Angle FromPoint(Vector2 startPoint, Vector2 endPoint)
    {
        return Angle.FromHeadingVector(endPoint - startPoint);
    }
    /*
    // Returns the angle to a moving point, given the point's velocity and the
    // theoretical projectile's velocity.
    public static Angle FromMovingPoint(Vector2 startPosition,
        Vector2 targetPosition, Vector2 targetVelocity, Vector2 projectileVelocity)
    {

    }
    */

    // Constructs an angle from another angle.
    public static Angle DeepCopy(Angle otherAngle)
    {
        return new Angle(otherAngle.degrees);
    }
    public Angle DeepCopy()
    {
        return new Angle(degrees);
    }

    // Returns the degree measure of the angle.
    public float GetDegrees()
    {
        return degrees;
    }

    // Returns the unsigned degree measure of the angle.
    public float GetDegreesUnsigned()
    {
        return UtilPeriodic.MoveIntoInterval(degrees, INTERVAL_UNSIGNED_DEGREES);
    }

    // Returns the signed degree measure of the angle.
    public float GetDegreesSigned()
    {
        return UtilPeriodic.MoveIntoInterval(degrees, INTERVAL_SIGNED_DEGREES);
    }

    // Returns the radian measure of the angle.
    public float GetRadians()
    {
        return degrees * Mathf.Deg2Rad;
    }

    // Returns a unit vector pointing in the direction given by the angle.
    public Vector2 GetHeadingVector()
    {
        float radians = GetRadians();
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    // Returns a wheel's linear velocity based on its angular velocity and radius.
    // The calling angle is assumed to be the angular velocity.
    public float GetLinearVelocity(float radius)
    {
        return GetRadians() * radius;
    }

    // Adds this many degrees to the angle.
    public Angle AddDegrees(float degreesAdditional)
    {
        degrees += degreesAdditional;
        return this;
    }

    // Adds this many radians to the angle.
    public Angle AddRadians(float radiansAdditional)
    {
        degrees += radiansAdditional * Mathf.Rad2Deg;
        return this;
    }

    // Adds a different angle to this angle.
    public Angle AddAngle(Angle other)
    {
        degrees += other.GetDegrees();
        return this;
    }

    // Rotates the angle by 180 degrees, effectively reversing its direction.
    public Angle Reverse()
    {
        degrees = UtilPeriodic.Reverse(degrees, INTERVAL_UNSIGNED_DEGREES);
        return this;
    }

    // Mirrors an angle across the y-axis of a circle.
    public Angle MirrorHorizontal()
    {
        degrees = UtilPeriodic.MirrorHorizontal(degrees, INTERVAL_UNSIGNED_DEGREES);
        return this;
    }

    // Mirrors an angle across the x-axis of a circle.
    public Angle MirrorVertical()
    {
        degrees = UtilPeriodic.MirrorVertical(degrees, INTERVAL_UNSIGNED_DEGREES);
        return this;
    }

    // Converts an angle to an equivalent angle within the range with the given center.
    // The range will always be 360 degrees in size.
    public Angle MoveIntoInterval(Angle centerOfTheInterval)
    {
        IntervalFloat interval = IntervalFloat.FromCenterRadius(
            centerOfTheInterval.GetDegrees(), 180.0f);
        degrees = UtilPeriodic.MoveIntoInterval(degrees, interval);
        return this;
    }

    // Converts an angle to an equivalent angle within the [-180, 180) range.
    public Angle MoveIntoSignedInterval()
    {
        degrees = UtilPeriodic.MoveIntoInterval(degrees, INTERVAL_SIGNED_DEGREES);
        return this;
    }

    // Converts an angle to an equivalent angle within the [0, 360) range.
    public Angle MoveIntoUnsignedInterval()
    {
        degrees = UtilPeriodic.MoveIntoInterval(degrees, INTERVAL_UNSIGNED_DEGREES);
        return this;
    }

    // Returns the smaller distance between the two angles.
    public static Angle GetSmallerDistance(Angle angle1, Angle angle2)
    {
        return Angle.FromDegrees(UtilPeriodic.GetSmallerDistance(
            angle1.GetDegrees(), angle2.GetDegrees(), INTERVAL_UNSIGNED_DEGREES));
    }

    // Returns the larger distance between the two angles.
    public static Angle GetLargerDistance(Angle angle1, Angle angle2)
    {
        return Angle.FromDegrees(UtilPeriodic.GetLargerDistance(
            angle1.GetDegrees(), angle2.GetDegrees(), INTERVAL_UNSIGNED_DEGREES));
    }

    // Returns true if the shortest path between the two given angles is a
    // positive (counterclockwise on the unit circle) rotation from the start angle.
    public static bool IsShortestRotationPositive(Angle start, Angle end)
    {
        return UtilPeriodic.IsShortestRotationPositive(
            start.GetDegrees(), end.GetDegrees(), INTERVAL_UNSIGNED_DEGREES);
    }

    // Like the approach float function, but rotates current along the shortest path
    // to the target, like an angle moving along a circle towards a different angle.
    public Angle Approach(Angle target, Angle stepSize)
    {
        degrees = UtilPeriodic.Approach(degrees, target.GetDegrees(),
            stepSize.GetDegrees(), INTERVAL_UNSIGNED_DEGREES);
        return this;
    }

    // Returns the sign of the shortest rotation between the two angles.
    public static int SignShortestRotation(Angle start, Angle end)
    {
        return UtilPeriodic.SignShortestRotation(
            start.GetDegrees(), end.GetDegrees(), INTERVAL_UNSIGNED_DEGREES);
    }

    // The multiplication operator scales the angle.
    public static Angle operator *(Angle first, float second)
    {
        return Angle.FromDegrees(first.degrees * second);
    }

    // Add two angles together, combining their measures.
    public static Angle operator +(Angle first, Angle second)
    {
        return Angle.FromDegrees(first.degrees + second.degrees);
    }

    // Angle measure comparisons.
    public static bool operator >=(Angle first, Angle second)
    {
        return first.GetDegrees() >= second.GetDegrees();
    }
    public static bool operator <=(Angle first, Angle second)
    {
        return first.GetDegrees() <= second.GetDegrees();
    }
    public static bool operator >(Angle first, Angle second)
    {
        return first.GetDegrees() > second.GetDegrees();
    }
    public static bool operator <(Angle first, Angle second)
    {
        return first.GetDegrees() < second.GetDegrees();
    }
}