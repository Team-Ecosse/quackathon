using UnityEngine;

/**
 * @todo should persist over different scenes
 */
public class SoundManager : MonoBehaviour
{
    const int QUACK_MIN = 1;
    const int QUACK_MAX = 3;

    public AudioSource music;
    public AudioSource quack;
    /**
     * @todo player
     */
    public PlayerController player;

    private System.Random _quackInterval;

    void Awake()
    {
        music.Play();
        _quackInterval = new System.Random();
    }

    void Start()
    {
        Debug.Log("Yo");
        Invoke("Quack", _quackInterval.Next(QUACK_MIN, QUACK_MAX));
    }

    void Quack()
    {
        quack.Play();
        Invoke("Quack", _quackInterval.Next(QUACK_MIN, QUACK_MAX));
    }
}