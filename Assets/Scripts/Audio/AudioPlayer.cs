using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource[] audioSources;

    // Play a sound
    public void Play(string sourceName)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.name.Equals(sourceName))
            {
                audioSource.Play();
                break;
            }
        }
    }

    // Stop a sound
    public void Stop(string sourceName)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.name.Equals(sourceName))
            {
                audioSource.Stop();
                break;
            }
        }
    }
}
