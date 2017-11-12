using UnityEngine;

/**
 * @todo restore quacks
 */
public class GameStartEvent : IEvent
{
    private AudioSource _music;
    private AudioSource _quack;
    private SoundManager _sm;


    public GameStartEvent(AudioSource music, AudioSource quack, SoundManager sm)
    {
        _music = music;
        _quack = quack;
        _sm = sm;
    }

    public void Trigger()
    {
        _music.Play();
        _sm.StartQuacks();
    }
}
