// Author(s): Paul Calande
// Score script for Rocket Puncher.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPScore : MonoBehaviour
{
    [SerializeField]
    [Tooltip("File to read score tuning from.")]
    TextAsset scoreFile;
    [SerializeField]
    [Tooltip("Text to use for the score.")]
    UIValueText textScore;
    [SerializeField]
    TimeScale ts;
    [SerializeField]
    Text textSummaryScore;
    [SerializeField]
    Text textSummaryTime;
    [SerializeField]
    Text textSummaryEnemiesPunched;
    [SerializeField]
    Text textSummaryBulletsBlocked;
    [SerializeField]
    GameObject summaryScreen;

    int pointsPerSecondPlaying;
    float secondsPassed = 0.0f;
    int enemiesPunched = 0;
    int bulletsBlocked = 0;
    Timer timerPointsPerSecond = new Timer(1.0f);

    private void Awake()
    {
        Tune();
    }

    private void Start()
    {
        summaryScreen.SetActive(false);
    }

    private void Tune()
    {
        JSONNodeReader reader = new JSONNodeReader(scoreFile);
        pointsPerSecondPlaying = reader.Get("points per second playing", 10);
    }

    private void TimerFinished(float secondsOverflow)
    {
        textScore.AddValue(pointsPerSecondPlaying);
    }

    private void FixedUpdate()
    {
        secondsPassed += ts.DeltaTime();
        timerPointsPerSecond.Tick(ts.DeltaTime());
    }

    public void Add(int val)
    {
        textScore.AddValue(val);
    }

    public int GetValue()
    {
        return textScore.GetValue();
    }

    public void PunchedEnemy()
    {
        ++enemiesPunched;
    }

    public void PunchedBullet()
    {
        ++bulletsBlocked;
    }

    public void PopulateSummaryScreen()
    {
        textSummaryScore.text = "Score: " + GetValue();
        textSummaryTime.text = "Time: " + UtilString.DigitalTime(secondsPassed);
        textSummaryEnemiesPunched.text = "Enemies Punched: " + enemiesPunched;
        textSummaryBulletsBlocked.text = "Bullets Blocked: " + bulletsBlocked;
        summaryScreen.SetActive(true);
    }
}