using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    #region Feilds
    public static Transform[,] grid = new Transform[WIDTH, HEIGHT];

    public const int WIDTH = 10;
    public const int HEIGHT = 23;
    const float ROTATE_ANGEL = 90f;

    float dropTime;
    float dropTimer;

    [SerializeField, Header("基本傷害")]
    float baseDamage;
    [SerializeField, Header("基本連擊傷害")]
    float baseCombo;
    int blockCount;


    bool isClear;

    TextMeshProUGUI textCombo;
    CanvasGroup endCanvas;
    GameObject ghostTetromino;
    PlayerInput playerInput;
    EnemySystem enemySystem;


    #endregion
    #region Unity Event Func

    private void Start()
    {

        ghostTetromino = GameObject.FindGameObjectWithTag("Ghost");
        playerInput = FindObjectOfType<PlayerInput>();
        textCombo = GameObject.Find("Text_Combo").GetComponent<TextMeshProUGUI>();
        enemySystem = GameObject.Find("Mutant").GetComponent<EnemySystem>();
        endCanvas = GameObject.Find("Canvas_遊戲結束").GetComponent<CanvasGroup>();
        isClear = false;
        blockCount = 0;
    }

    private void OnEnable()
    {
        PlayerInput.onMoveLeft += MoveLeft;
        PlayerInput.onMoveRight += MoveRight;
        PlayerInput.onDrop += Drop;
        PlayerInput.onCancelDrop += CancelDrop;
        PlayerInput.onRotate += Rotate;
        PlayerInput.onInstantDrop += InstantDrop;

        dropTime = GameManager.Instance.AutoDropTime;
    }

    void OnDisable()
    {
        PlayerInput.onMoveLeft -= MoveLeft;
        PlayerInput.onMoveLeft -= MoveLeft;
        PlayerInput.onMoveRight -= MoveRight;
        PlayerInput.onDrop -= Drop;
        PlayerInput.onCancelDrop -= CancelDrop;
        PlayerInput.onRotate -= Rotate;
        PlayerInput.onInstantDrop -= InstantDrop;
        ghostTetromino = GameObject.FindGameObjectWithTag("Ghost");
        DestroyGhost();
        HadGetHurt();

    }
    public virtual void FixedUpdate()
    {
        dropTimer += Time.fixedDeltaTime;
        if (dropTimer >= dropTime)
        {
            dropTimer = 0;
            MoveDown();
        }
        if (PlayerInput.keepMoveLeft)
        {
            MoveLeft();
        }
        if (PlayerInput.keepMoveRight)
        {
            MoveRight();
        }
    }
    #endregion
    #region GENERIC
    public virtual bool Movable
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
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);
            grid[x, y] = child;
            if (y == HEIGHT - 2)
            {
                Time.timeScale = 0;
                GameManager.Instance.FinishGame();
                playerInput.enabled = false;
                enabled = false;
            }
        }

    }
    #endregion
    #region Horizintal move
    public virtual void MoveLeft()
    {
        transform.position += Vector3.left;
        if (!Movable)
        {
            transform.position += Vector3.right;
        }
    }

    public virtual void MoveRight()
    {
        transform.position += Vector3.right;
        if (!Movable)
        {
            transform.position += Vector3.left;

        }
    }


    #endregion
    #region Vertical move  
    public virtual void MoveDown()
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
    public virtual void Rotate()
    {
        transform.Rotate(Vector3.forward, -ROTATE_ANGEL);
        if (!Movable)
        {
            transform.Rotate(Vector3.forward, +ROTATE_ANGEL);

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
                isClear = true;
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
            blockCount++;
        }
    }
    void DecreaseRow(int row)
    {
        for (int y = row; y < HEIGHT - 1; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                if (grid[x, y + 1] != null)
                {
                    grid[x, y] = grid[x, y + 1];
                    grid[x, y + 1] = null;
                    grid[x, y].gameObject.transform.position += Vector3.down;

                }
            }
        }
    }
    #endregion
    void InstantDrop()
    {
        dropTime = 0;
        for (int i = HEIGHT; i >= 0; i--)
        {
            if (Movable)
            {
                transform.position += Vector3.down;

            }

        }

        transform.position += Vector3.up;


    }

    public virtual void DestroyGhost()
    {
        Destroy(ghostTetromino);

    }
    public virtual void HadGetHurt()
    {
        if (isClear)
        {
            enemySystem.GetHurt(blockCount * baseDamage);
           
            if (enemySystem.hurtCount > 1)
            {
                enemySystem.comboCount++;
                textCombo.color = new Color(255, 255, 255, 255);
                textCombo.text = enemySystem.comboCount.ToString() + " Combo" + "\n+ " + enemySystem.comboCount * baseCombo;
                enemySystem.ComboDamage(enemySystem.comboCount * baseCombo);
            }
            if (enemySystem.comboCount > 5)
            {
                GameManager.Instance.audioSourceFall.PlayOneShot(GameManager.Instance.audioClipsFull[5]);
            }
            else
            {
                GameManager.Instance.audioSourceFall.PlayOneShot(GameManager.Instance.audioClipsFull[enemySystem.comboCount]);
            }
            
        }
        else
        {
            enemySystem.hurtCount = 0;
            enemySystem.comboCount = 0;
            textCombo.color = new Color(0, 0, 0, 0);
            GameManager.Instance.audioSourceFall.PlayOneShot(GameManager.Instance.audioClipsFull[0]);
        }

    }



}
