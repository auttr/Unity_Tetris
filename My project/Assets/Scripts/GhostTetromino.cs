using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace auttr
{
    public class GhostTetromino : Tetromino
    {


        void Start()
        {
            DropPosition();

        }


        void DropPosition()
        {

            for (int i = HEIGHT - 1; i >= 0; i--)
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
            // transform.position += new Vector3(0, HEIGHT-transform.position.y-2, 0);
            transform.position = new Vector3(GameManager.Instance.tetrominoIndexList[0].transform.position.x, GameManager.Instance.tetrominoIndexList[0].transform.position.y, 0.5f);

            base.MoveLeft();
            DropPosition();

        }
        public override void Rotate()
        {
            //transform.position += new Vector3(0, HEIGHT - transform.position.y-2, 0);
            transform.position = new Vector3(GameManager.Instance.tetrominoIndexList[0].transform.position.x, GameManager.Instance.tetrominoIndexList[0].transform.position.y, 0.5f);
            base.Rotate();
            DropPosition();
        }
        public override void MoveRight()
        {
            //transform.position += new Vector3(0, HEIGHT - transform.position.y-2, 0);
            transform.position = new Vector3(GameManager.Instance.tetrominoIndexList[0].transform.position.x, GameManager.Instance.tetrominoIndexList[0].transform.position.y, 0.5f);
            base.MoveRight();
            DropPosition();
        }
        public override void DestroyGhost()
        {

        }
        public override void HadGetHurt()
        {

        }
        public override void Hold()
        {

        }





    }


}
