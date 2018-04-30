// Author(s): Paul Calande
// Rocket Puncher main menu.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RPMainMenu : MonoBehaviour
{
    [SerializeField]
    Text textHighScore;
    [SerializeField]
    GameObject credits;
    [SerializeField]
    [Tooltip("The sound to play during menu transitions.")]
    AudioClip soundTransition;

    AudioController ac;

    private void Start()
    {
        textHighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0);
        credits.SetActive(false);
        ac = ServiceLocator.GetAudioController();
    }

    private void TransitionSound()
    {
        ac.PlaySFX(soundTransition);
    }

    public void PlayGame()
    {
        //ac.StopMusicGroup(1.0f);
        TransitionSound();
        SceneManager.LoadScene("MainGame");
    }

    public void Quit()
    {
        UtilScene.ExitGame();
    }

    public void CreditsShow()
    {
        credits.SetActive(true);
    }

    public void CreditsHide()
    {
        credits.SetActive(false);
    }
}