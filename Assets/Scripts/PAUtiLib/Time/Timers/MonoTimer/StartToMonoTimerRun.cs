// Author(s): Paul Calande
// Start causes a MonoTimer to run.

using UnityEngine;

public class StartToMonoTimerRun : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The MonoTimer to run.")]
    MonoTimer timer;

    private void Start()
    {
        timer.Run();
    }
}
