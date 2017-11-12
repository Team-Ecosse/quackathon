using UnityEngine;

public class GamePausingEvent: IEvent
{
    private AudioSource _music;

    public GamePausingEvent(AudioSource music)
    {
        _music = music;
    }

    public void Trigger()
    {
        _music.Pause();
    }
}
