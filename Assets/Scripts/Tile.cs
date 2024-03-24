using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int x;
    public int y;
    public Board board;

    public void Setup(int x_, int y_, Board board_)
    {
        this.x = x_;
        this.y = y_;
        this.board = board_;
    }

    private void OnMouseDown()
    {
        board.TileDown(this);
    }

    private void OnMouseEnter()
    {
        board.TileOver(this);
    }

    private void OnMouseUp()
    {
        board.TileUp(this);
    }
}
