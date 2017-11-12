using UnityEngine;

/**
 * @todo should persist over different scenes
 */
public class SoundManager : MonoBehaviour
{
    const int QUACK_MIN = 1;
    const int QUACK_MAX = 3;

    public AudioSource gameMusic;
    public AudioSource menuMusic;
    public AudioSource quack;
    public GameManager gameManager;
    public MainMenu mainMenu;
    /**
     * @todo player
     */
    public PlayerController player;

    /*
     * @todo alphabetical order
     */
    private System.Random _quackInterval;
    
    private GameStartEvent _gameResuming;
    private GamePausingEvent _gamePausing;
    private MenuOpeningEvent _menuOpening;
    private MenuClosingEvent _menuClosing;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _quackInterval = new System.Random();
        _menuOpening = new MenuOpeningEvent(menuMusic);
        _menuClosing = new MenuClosingEvent(menuMusic);
        _gameResuming = new GameStartEvent(gameMusic, quack, this);
        _gamePausing = new GamePausingEvent(gameMusic);
    }

    void Start()
    {
        mainMenu.startGameEventList.Add(_gameResuming);
        mainMenu.startGameEventList.Add(_menuClosing);
        mainMenu.returnToMainMenuEventList.Add(_gamePausing);
        _menuOpening.Trigger();
        //InitialiseMenu();
    }

    public void StartQuacks()
    {
        Invoke("Quack", _quackInterval.Next(QUACK_MIN, QUACK_MAX));
    }

    void Quack()
    {
            quack.Play();
            Invoke("Quack", _quackInterval.Next(QUACK_MIN, QUACK_MAX));
        if (1 == gameManager.GetCurrentSceneIndex())
        {
        }
    }
}
