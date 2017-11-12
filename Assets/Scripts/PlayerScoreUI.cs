using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreUI : MonoBehaviour
{

    Text text;

    public GameManager GameManager;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text = this.GetComponent<Text>();
        text.text = GameManager.ReturnPlayerSco().ToString();
    }
}