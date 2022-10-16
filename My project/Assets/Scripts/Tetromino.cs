using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    #region Feilds
    static Transform[,] grid = new Transform[WIDTH, HEIGHT];

    const int WIDTH = 10;
    const int HEIGHT = 22;
    const float ROTATE_ANGEL = 90f;

    float dropTime;
    float dropTimer;
    #endregion
    #region Unity Event Func
    private void OnEnable()
    {
        PlayerInput.onMoveLeft += MoveLeft;
        PlayerInput.onMoveRight += MoveRight;
        PlayerInput.onDrop += Drop;
        PlayerInput.onCancelDrop += CancelDrop;
        PlayerInput.onRotate += Rotate;

        dropTime = GameManager.Instance.AutoDropTime;
    }
    private void OnDisable()
    {
        PlayerInput.onMoveLeft -= MoveLeft;
        PlayerInput.onMoveRight -= MoveRight;
        PlayerInput.onDrop -= Drop;
        PlayerInput.onCancelDrop -= CancelDrop;
        PlayerInput.onRotate -= Rotate;
    }
    private void FixedUpdate()
    {
        dropTimer += Time.fixedDeltaTime;
        if (dropTimer >= dropTime)
        {
            dropTimer = 0;
            MoveDown();
        }
    }
    #endregion
    #region GENERIC
    bool Movable
    {
        get
        {
            foreach (Transform child in transform)
            {
                int x = Mathf.RoundToInt(child.position.x);
                int y = Mathf.RoundToInt(child.position.y);
                if (x < 0 || x >= WIDTH || y < 0 || y >= HEIGHT || grid[x, y] != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
    void Land()
    {

        foreach (Transform child in transform)
        {
            int x = (int)child.position.x;
            int y = (int)child.position.y;
            grid[x, y] = child;
            if (y == HEIGHT - 1)
            {
                //GameOver
            }
        }

    }
    #endregion
    #region Horizintal move
    void MoveLeft()
    {
        transform.position += Vector3.left;
        if (!Movable)
        {
            transform.position += Vector3.right;

        }
    }
    void MoveRight()
    {
        transform.position += Vector3.right;
        if (!Movable)
        {
            transform.position += Vector3.left;

        }
    }
    #endregion
    #region Vertical move  
    void MoveDown()
    {
        transform.position += Vector3.down;
        if (!Movable)
        {
            transform.position += Vector3.up;
            Land();
            CleanFullRows();
            enabled = false;
            GameManager.Instance.SpawnTetromino();
        }

    }
    void Drop()
    {
        dropTime = Time.fixedDeltaTime;
    }
    void CancelDrop()
    {
        dropTime = GameManager.Instance.AutoDropTime;
    }
    #endregion
    #region Rotate
    void Rotate()
    {
        transform.Rotate(Vector3.forward, ROTATE_ANGEL);
        if (!Movable)
        {
            transform.Rotate(Vector3.forward, -ROTATE_ANGEL);

        }
    }
    #endregion
    #region Check Rows
    void CleanFullRows()
    {
        for (int i = HEIGHT - 1; i >= 0; i--)
        {
            if (IsRowFull(i))
            {
                DestroyRow(i);
                DecreaseRow(i);
            }
        }
    }
    bool IsRowFull(int y)
    {
        for (int x = 0; x < WIDTH; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }
    void DestroyRow(int y)
    {
        for (int x = 0; x < WIDTH; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    void DecreaseRow(int row)
    {
        for (int y = row; y < HEIGHT - 1; y++)
        {
            for (int x = 0;  x< WIDTH; x++)
            {
                if (grid[x,y+1]!=null)
                {
                    grid[x, y] = grid[x, y + 1];
                    grid[x, y + 1] = null;
                    grid[x, y].gameObject.transform.position += Vector3.down;
                }
            }
        }
    }
    #endregion
}
