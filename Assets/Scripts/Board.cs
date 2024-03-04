using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour {

    public int width;
    public int height;
    public GameObject tileObject;

    public float cameraSizeOffset;
    public float cameraVerticalOffset;

    public GameObject[] avalaiblePieces;

    Tile[,] tiles;
    Piece[,] pieces;
    Tile startTile;
    Tile endTile;

    private void Start() {

        tiles = new Tile[width, height];
        pieces = new Piece[width, height];

        SetupBoard();
        CameraPos();
        SetupPieces();

    }

    private void SetupPieces() {

        for (int x = 0; x < width; x++) {

            for (int y = 0; y < height; y++) {

                GameObject selectedPiece = avalaiblePieces[UnityEngine.Random.Range(0,avalaiblePieces.Length)];

                GameObject newTile = Instantiate(selectedPiece, new Vector3(x, y, -5), Quaternion.identity);

                newTile.transform.parent = transform;

                pieces[x, y] = newTile.GetComponent<Piece>();
                pieces[x, y]?.Setup(x, y, this);
            }

        }

    }

    private void SetupBoard() {

        for (int x = 0; x < width; x++) {
            
            for(int y = 0; y < height; y++) {
            
                GameObject newTile = Instantiate(tileObject, new Vector3(x,y,-5), Quaternion.identity);

                newTile.transform.parent = transform;

                tiles[x,y] = newTile.GetComponent<Tile>();
                tiles[x, y]?.Setup(x,y,this);
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

    public void TileDown(Tile tile_) {

        startTile = tile_;
    
    }

    public void TileOver(Tile tile_) {

        endTile = tile_;

    }

    public void TileUp(Tile tile_) {

        if (startTile != null && endTile != null && IsCloseTo(startTile, endTile)) {

            SwapTiles();
            
        }

    }

    private void SwapTiles() {

        Piece startPiece = pieces[startTile.x, startTile.y];
        Piece endPiece = pieces[endTile.x, endTile.y];

        startPiece.Move(endTile.x, endTile.y);
        endPiece.Move(startTile.x, startTile.y);

        pieces[startPiece.x, startPiece.y] = endPiece;
        pieces[endPiece.x, endPiece.y] = startPiece;

    }

    public bool IsCloseTo(Tile start, Tile end) {

        if (Mathf.Abs(start.x - end.x) == 1 && start.y == end.y) {
        
            return true;
        
        }

        if (Mathf.Abs(start.y - end.y) == 1 && start.x == end.x) {

            return true;

        }
      
        return false; 

    }
}
