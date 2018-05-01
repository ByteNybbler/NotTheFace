// Author(s): Unity Answers
// https://answers.unity.com/questions/242794/inspector-field-for-scene-asset.html
// Provides a serializable field for scene assets.

using UnityEngine;

[System.Serializable]
public class SceneField
{
    [SerializeField]
    private UnityEngine.Object sceneAsset;
    [SerializeField]
    private string scenePath = "";

    public string GetPath()
    {
        return scenePath;
    }

    // makes it work with the existing Unity methods (LoadLevel/LoadScene)
    public static implicit operator string(SceneField sceneField)
    {
        return sceneField.scenePath;
    }
}