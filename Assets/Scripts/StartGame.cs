using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public void StartingGame() {

        gameObject.SetActive(false);
        GameManager.Instance.canStart = true;
    }

}
