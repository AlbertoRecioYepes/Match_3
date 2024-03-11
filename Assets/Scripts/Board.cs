using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class Board : MonoBehaviour {

    public int width;
    public int height;
    public GameObject tileObject;

    public float cameraSizeOffset;
    public float cameraVerticalOffset;

    public GameObject[] avalaiblePieces;

    Tile[,] tiles;
    public Piece[,] pieces;
    Tile startTile;
    Tile endTile;

    private void Start() {

        tiles = new Tile[width, height];
        pieces = new Piece[width, height];

        SetupBoard();
        CameraPos();
        SetupPieces();

    }

    public void SetupPieces() {

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {

                Piece.type randomPiece = (Piece.type)UnityEngine.Random.Range(0, avalaiblePieces.Length);


                while(CreatesMatch(x, y, randomPiece)) {

                    randomPiece = (Piece.type)UnityEngine.Random.Range(0, avalaiblePieces.Length);
                }


                GameObject newPiece = Instantiate(avalaiblePieces[(int)randomPiece], new Vector3(x, y, -5), Quaternion.identity);
                newPiece.transform.parent = transform;

                pieces[x, y] = newPiece.GetComponent<Piece>();
                pieces[x, y]?.Setup(x, y, this);
            }
        }
    }

    //Check
    private bool CreatesMatch(int x, int y, Piece.type pieceType) {

        if(x >= 2 && pieces[x - 1, y]?.pieceType == pieceType && pieces[x - 2, y]?.pieceType == pieceType)
            return true;


        if(y >= 2 && pieces[x, y - 1]?.pieceType == pieceType && pieces[x, y - 2]?.pieceType == pieceType)
            return true;

        return false;
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

        startPiece.Move(endTile.x, endTile.y, startPiece);
        endPiece.Move(startTile.x, startTile.y, endPiece);

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

    public void CollapsePieces() {

        for(int x = 0; x < width; x++) {

            int emptySpaces = 0;

            for(int y = 0; y < height; y++) {

                if(pieces[x, y] == null) {

                    emptySpaces++;

                }
                else if(emptySpaces > 0) {

                    pieces[x, y - emptySpaces] = pieces[x, y];
                    pieces[x, y - emptySpaces].Move(x, y - emptySpaces, pieces[x, y - emptySpaces]);
                    pieces[x, y] = null;

                }
            }

            for(int i = 0; i < emptySpaces; i++) {

                GameObject selectedPiece = avalaiblePieces[UnityEngine.Random.Range(0, avalaiblePieces.Length)];
                GameObject newPiece = Instantiate(selectedPiece, new Vector3(x, height - 1 - i, -5), Quaternion.identity);

                newPiece.transform.parent = transform;

                pieces[x, height - 1 - i] = newPiece.GetComponent<Piece>();
                pieces[x, height - 1 - i].Setup(x, height - 1 - i, this);
                pieces[x, height - 1 - i].transform.position = new Vector3(x, height + i, -5);
                pieces[x, height - 1 - i].Move(x, height - 1 - i, pieces[x, height - 1 - i]);

            }
        }
    }

}
