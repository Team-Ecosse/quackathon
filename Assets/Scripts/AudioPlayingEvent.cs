using UnityEngine;

public class AudioPlayingEvent : IEvent
{
    private AudioSource audio_;
    
    public AudioPlayingEvent(AudioSource audio)
    {
        audio_ = audio;
    }

    public void trigger()
    {
        Debug.Log("music playing");
        audio_.Play();
    }
}