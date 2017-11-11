using UnityEngine;

/**
 * @todo should persist over different scenes
 */
public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource quack;
    /**
     * @todo player
     */
    public PlayerController player;

    void Awake()
    {
        music.Play();
    }

    void Start()
    {
        player.handleJumpEventQueue.add(new AudioPlayingEvent(quack));
    }
}