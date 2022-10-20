using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GhostTetromino : Tetromino
{

    void Start()
    {
        DropPosition();
    }
    
  
    void DropPosition()
    {

        for (int i = HEIGHT; i >= 0; i--)
        {
            if (Movable)
            {
                transform.position += Vector3.down;

            }

        }

        transform.position += Vector3.up;

    }
    public override void MoveDown()
    {
    }
    public override void MoveLeft()
    {
        transform.position += new Vector3(0, HEIGHT-transform.position.y-2, 0);
        base.MoveLeft();
        DropPosition();

    }
    public override void Rotate()
    {
        transform.position += new Vector3(0, HEIGHT - transform.position.y-2, 0);
        base.Rotate();
        DropPosition();
    }
    public override void MoveRight()
    {
        transform.position += new Vector3(0, HEIGHT - transform.position.y-2, 0);
        base.MoveRight();
        DropPosition();
    }
    public override void DestroyGhost()
    {
        
    }
    public override void HadGetHurt()
    {
        
    }
   




}

