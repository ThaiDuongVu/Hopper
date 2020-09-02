using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource[] audioSources;

    private float[] defaultPitches;

    private void Start()
    {
        defaultPitches = new float[audioSources.Length];

        for (int i = 0; i < defaultPitches.Length; i++)
        {
            defaultPitches[i] = audioSources[i].pitch;
        }
    }

    // Play a sound
    public void Play(string sourceName)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.name.Equals(sourceName))
            {
                audioSource.Play();
                audioSource.pitch += 0.1f;
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

    // Reset audio pitches
    public void ResetPitch()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].pitch = defaultPitches[i];
        }
    }
}
