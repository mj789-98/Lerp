using UnityEngine;

public class AudioListenerManager : MonoBehaviour
{
    private void Awake()
    {
        // Find all AudioListener components in the scene
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();

        // If there is more than one AudioListener, destroy the extras
        if (listeners.Length > 1)
        {
            for (int i = 1; i < listeners.Length; i++)
            {
                Destroy(listeners[i]);
            }
        }
    }
}
