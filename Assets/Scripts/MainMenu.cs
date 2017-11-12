using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviour {

    [Header("Required Object Refs.")]
    [Space]

    private GameObject _eventSystem;
    private GameManager _gameManager;

    [Space]
    [Header("UI Container Objects")]
    [Space]

    public GameObject _mainMenuUI;

    void Start () {

        _eventSystem = GameObject.Find("EventSystem");
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _mainMenuUI.SetActive(true);
	}

    public void StartGame() {
        _gameManager.ProgressToNextScene();
    }

    public void ViewHighScores() {

        if(_mainMenuUI.activeInHierarchy) {
            _mainMenuUI.SetActive(false);

            _eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>()
                .SetSelectedGameObject(GameObject.Find("ReturnToMainMenu"));
        }
    }

    public void ExitGame() {
        _gameManager.ExitGame();
    }

    public void ReturnToMainMenu() {
        
        _mainMenuUI.SetActive(true);

        _eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>()
                .SetSelectedGameObject(GameObject.Find("StartGame"));
    }
}
