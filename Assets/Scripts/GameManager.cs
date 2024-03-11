using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }


    public void DestroyWithDelay(GameObject gameObject) {

        Debug.Log(gameObject.GetComponent<Piece>().pieceType + "Destroy");

        Destroy(gameObject, 0.3f);
        Invoke("CollapsePiecesAfterMatch3", 0.3f);
    }

    public void CollapsePiecesAfterMatch3() {
        Board board = FindObjectOfType<Board>();
        if(board != null) {
            board.CollapsePieces();
        }
    }


}
