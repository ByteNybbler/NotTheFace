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

    // The translator for translating the item text.
    Translator translator;
    // The timer that determines when the item-related text will exit the screen.
    Timer timerItemText;

    private void Awake()
    {
        JSONNodeReader jsonReader = new JSONNodeReader(fileItems);

        float timerItemSeconds = jsonReader.Get("seconds to display item text", 3.0f);
        timerItemText = new Timer(timerItemSeconds, Disappear, false, false);
    }

    private void Start()
    {
        Disappear();
        translator = ServiceLocator.GetTranslator();
    }

    public void Appear(string identifier)
    {
        gameObject.SetActive(true);
        timerItemText.Reset();
        timerItemText.Start();

        textItemName.text = translator.Translate("Item", identifier, "Name");
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