using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour {

    public int x;
    public int y;
    public Board board;

    public enum type {

        elephant,
        giraffe,
        hippo,
        monkey,
        panda,
        parrot,
        penguin,
        pig,
        rabbit,
        snake

    };

    public type pieceType;

    public void Setup(int x_, int y_, Board board_) {

        this.x = x_;
        this.y = y_;
        this.board = board_;

    }

    Tween tw;

    void OnDestroy() {

        if(tw != null) {
            tw.Kill(false);
            tw = null;
        }
    }


    public void Move(int destX, int destY, Piece piece) {


        if(tw != null) {
            tw.Kill(false);
            tw = null;
        }

        tw = transform.DOMove(new Vector3(destX, destY, -5.0f), 0.25f).SetEase(Ease.InOutCubic).OnComplete(() => {
            x = destX;
            y = destY;
            CheckMatch(piece);
            tw = null;
        });

    }

    public void CheckMatch(Piece piece) {

        int horizontalCount = CountMatches(piece, 1, 0) + CountMatches(piece, -1, 0) + 1;
        int verticalCount = CountMatches(piece, 0, 1) + CountMatches(piece, 0, -1) + 1;

        if(horizontalCount >= 3) {

            DestroyConnectedPieces(piece, 1, 0);
            DestroyConnectedPieces(piece, -1, 0);

        }

        if(verticalCount >= 3) {

            DestroyConnectedPieces(piece, 0, 1);
            DestroyConnectedPieces(piece, 0, -1);

        }
    }

    private void DestroyConnectedPieces(Piece piece, int xOffset, int yOffset) {

        Piece currentPiece = piece;

        GameManager.instance.DestroyWithDelay(currentPiece.gameObject);

        for(int i = 1; i < 3; i++) {

            int nextX = currentPiece.x + (xOffset * i);
            int nextY = currentPiece.y + (yOffset * i);

            if(nextX < 0 || nextX >= board.pieces.GetLength(0) || nextY < 0 || nextY >= board.pieces.GetLength(1))
                break;

            Piece nextPiece = board.pieces[nextX, nextY];

            if(nextPiece.pieceType == piece.pieceType) {
                GameManager.instance.DestroyWithDelay(nextPiece.gameObject);
            }
            else {
                break;
            }
        }
    }

    private int CountMatches(Piece piece, int xOffset, int yOffset) {

        int count = 0;
        Piece currentPiece = piece;

        while(true) {

            int nextX = currentPiece.x + xOffset;
            int nextY = currentPiece.y + yOffset;

            if(nextX < 0 || nextX >= board.pieces.GetLength(0) || nextY < 0 || nextY >= board.pieces.GetLength(1))
                break;

            Piece nextPiece = board.pieces[nextX, nextY];

            if(nextPiece.pieceType == piece.pieceType) {
                count++;
                currentPiece = nextPiece;
            }
            else {
                break;
            }
        }

        return count;
    }



}
