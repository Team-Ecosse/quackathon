using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour {

    [Header("Managed Object Ref.")]
    [Space]

    private GameObject _eventSystem;
    public PlayerManager PlayerManager;
    public ScoreManager ScoreManager;

    [Space]
    [Header("Scene Indexer")]
    [Space]

    [SerializeField]
    private int currentSceneIndex;

    [SerializeField]
    private int nextSceneIndex;

    void Awake() {
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1);

        PlayerManager.setHealth(15);
        ScoreManager.setScore(0);
    }

    /*
     * This section covers all aspects of instantiating, restarting and progressing scenes
     */

    void Start () {

        _eventSystem = GameObject.Find("EventSystem");
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadSceneAsync(0);
    }

    public void ProgressToNextScene() {
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }

    public static void ResetScene() {
        
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void PlayerTakeDamage(int damage)
    {
        PlayerManager.playerTakenDMG(damage);
    }

    public void PlayerHeal(int heal)
    {
        PlayerManager.playerGainHealth(heal);
    }

    public int ReturnPlayerHP()
    {
        return PlayerManager.returnPlayerHealth();
    }

    public void PlayerLoseSco(int lose)
    {
        ScoreManager.playerLoseScore(lose);
    }

    public void PlayerAddSco(int add)
    {
        ScoreManager.playerAddScore(add);
    }

    public int ReturnPlayerSco()
    {
        return ScoreManager.returnPlayerScore();
    }

    public int GetCurrentSceneIndex()
    {
        return currentSceneIndex;
    }
}
