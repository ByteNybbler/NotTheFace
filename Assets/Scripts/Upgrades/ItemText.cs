// Author(s): Paul Calande
// Item text for Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemText : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The items JSON.")]
    TextAsset fileItems;
    [SerializeField]
    [Tooltip("The item name text.")]
    Text textItemName;
    [SerializeField]
    [Tooltip("The item description text.")]
    Text textItemDescription;

    // The timer that determines when the item-related text will exit the screen.
    Timer timerItemText;

    private void Awake()
    {
        JSONNodeReader jsonReader = new JSONNodeReader(fileItems);

        float timerItemSeconds = jsonReader.Get("seconds to display item text", 3.0f);
        timerItemText = new Timer(timerItemSeconds, Disappear, false);
    }

    private void Start()
    {
        Disappear();
    }

    public void Appear(string identifier)
    {
        gameObject.SetActive(true);
        timerItemText.Reset();
        timerItemText.Start();

        textItemName.text = UtilTranslate.ItemName(identifier);
        textItemDescription.text = UtilTranslate.ItemDescription(identifier);
    }

    private void Disappear(float secondsOverflow = 0.0f)
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        timerItemText.Tick(timeScale.DeltaTime());
    }
}