using UnityEngine;

public class MenuClosingEvent: IEvent
{
    private AudioSource _music;

    public MenuClosingEvent(AudioSource music)
    {
         _music = music;
    }

    public void Trigger()
    {
        _music.Stop();
    }
}
