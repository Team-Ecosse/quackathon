using UnityEngine;

public class MenuOpeningEvent : IEvent
{
    private AudioSource _music;

    public MenuOpeningEvent(AudioSource music)
    {
        _music = music;
    }

    public void Trigger()
    {
        _music.Play();
    }
}
