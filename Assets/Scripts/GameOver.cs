using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public TMP_Text scoreText;

    private void Start() {

        scoreText.text = GameManager.Instance.GetPoints() + " Points";

    }

    public void PlayAgain() {

        GameManager.Instance.ResetPoints();

        SceneManager.LoadScene("GameScene");
    
    }

    public void ExitGame() {

        Application.Quit();
    
    }

}
