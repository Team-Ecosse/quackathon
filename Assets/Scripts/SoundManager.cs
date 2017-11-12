using UnityEngine;

/**
 * @todo should persist over different scenes
 */
public class SoundManager : MonoBehaviour
{

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
    private GameStartEvent _gameResuming;
    private GamePausingEvent _gamePausing;
    private MenuOpeningEvent _menuOpening;
    private MenuClosingEvent _menuClosing;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _menuOpening = new MenuOpeningEvent(menuMusic);
        _menuClosing = new MenuClosingEvent(menuMusic);
        _gameResuming = new GameStartEvent(gameMusic, quack, gameManager);
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
}