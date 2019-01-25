// Author(s): Paul Calande
// Unit test script for PAUtiLib.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAUtiLibUnitTest : MonoBehaviour
{
    // The number of the current test in the current suite.
    int currentTestNumber = 0;
    // The name of the current test suite.
    string currentSuiteName = "UNNAMED";

    // Test an assertion.
    void Test(bool assertion, string extraInfo = "")
    {
        ++currentTestNumber;
        if (!assertion)
        {
            Debug.LogError("PAUtiLib Unit Test: "
                + currentSuiteName
                + currentTestNumber + " failed to pass.\n"
                + extraInfo);
        }
    }

    void TestEqual(float lhs, float rhs)
    {
        Test(lhs == rhs, "LHS = " + lhs + "; RHS = " + rhs);
    }

    void TestEqual(int lhs, int rhs)
    {
        Test(lhs == rhs, "LHS = " + lhs + "; RHS = " + rhs);
    }

    // Enter a new test suite.
    void EnterNewSuite(string newSuiteName)
    {
        currentSuiteName = newSuiteName;
        currentTestNumber = 0;
    }

    void Start()
    {
        EnterNewSuite("Angles");
        Angle a = Angle.FromDegrees(20.0f);
        Angle b = Angle.FromDegrees(310.0f);
        TestEqual(Angle.GetLargerDistance(a, b).GetDegrees(), 290.0f);
        TestEqual(Angle.GetSmallerDistance(b, a).GetDegrees(), 70.0f);
    }
}