using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public int width;
    public int height;
    public GameObject tileObject;

    public float cameraSizeOffset;
    public float cameraVerticalOffset;

    private void Start() {

        SetupBoard();
        CameraPos();

    }

    private void SetupBoard() {

        for (int x = 0; x < width; x++) {
            
            for(int y = 0; y < height; y++) {
            
                GameObject newTile = Instantiate(tileObject, new Vector3(x,y,-5), Quaternion.identity);

                newTile.transform.parent = transform;
            
            }

        }
    
    }

    private void CameraPos() {
    
        float newPosX = (float) width / 2.0f;
        float newPosY = (float) height / 2.0f;

        Camera.main.transform.position = new Vector3 (newPosX - 0.5f, newPosY - 0.5f + cameraVerticalOffset, -10.0f);

        float horizontal = width + 1;
        float vertical = (height/2) + 1;

        Camera.main.orthographicSize  = horizontal > vertical ? horizontal + cameraSizeOffset : vertical;
    
    }

}
