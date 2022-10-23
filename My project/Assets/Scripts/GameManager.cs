using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float AutoDropTime => autoDropTime;
    public static GameManager Instance;
    [SerializeField] GameObject[] tetrominoPrefabs;
    [SerializeField] GameObject[] ghostPrefabs;
    public GameObject[] tetrominoPrefabsDemo;
    [SerializeField] Transform tetrominoSpawnerTransform;
    [SerializeField] Transform tetrominoPreviewTransformUp;
    [SerializeField] Transform tetrominoPreviewTransformDown;
    [SerializeField] Transform holdTrans;
    [SerializeField] float autoDropTime = 0.7f;
    [SerializeField, Header("遊戲結束")] GameObject imageWin;
    public AudioSource audioSourceFall;
    [Header("到地面聲音")] public AudioClip[] audioClipsFull;
    GameObject previewTetrominoPrefabDown;
    GameObject previewTetrominoPrefabUp;
    GameObject previewTetrominoPrefabSpawn;
    GameObject holdTetromino;
    public List<int> randList = new();
    public List<GameObject> tetrominoIndexList = new();
    public List<int> holdList = new();
    GameObject tetromino;
    public int currentTetromino;
    public int holdCount;
    public bool isCount;
    bool isHold;
    int holdIndex;
    Background background;
    [SerializeField, Header("Image_移動遮擋")]
    GameObject moveCover;
    private void Awake()
    {
        Instance = this;
        SpawnTetromino();
        SpawnTetromino();
        SpawnTetromino();
        audioSourceFall = GetComponent<AudioSource>();
        background = GameObject.Find("BackgroundControl").GetComponent<Background>();
        background.enabled = false;
        //previewTetrominoPrefabDown.transform.position = tetrominoPreviewTransformDown.position;
    }
    private void Update()
    {

    }
    /// <summary>
    /// 生成方塊
    /// </summary>
    public void SpawnTetromino()
    {

        int r = Random.Range(0, tetrominoPrefabs.Length);
        randList.Add(r);
        //移動到生成區
        PreviewMoveSpawn();
        //從預覽下移動到預覽上
        PreviewMoveUP();
        //生成Demo預覽下
        PreviewInstantiateTetromino(r);
    }
    /// <summary>
    /// 生成:移動到生成點，移動到上方，生成在下方
    /// </summary>
    #region Spawn
    void PreviewMoveSpawn()
    {
        if (previewTetrominoPrefabUp != null)
        {
            //刪除up位置多的物件
            previewTetrominoPrefabSpawn = previewTetrominoPrefabUp;
            Destroy(previewTetrominoPrefabSpawn);


            //生成下墜預覽方塊
            Instantiate(ghostPrefabs[randList[0]], new Vector3(tetrominoSpawnerTransform.position.x, tetrominoSpawnerTransform.position.y, 0.2f), Quaternion.identity);
            //生成方塊
            tetromino = Instantiate(tetrominoPrefabs[randList[0]], tetrominoSpawnerTransform.position, Quaternion.identity);


            currentTetromino = randList[0];



            //加入list，讓ghost抓到該物件
            tetrominoIndexList.Add(tetromino);

            randList.RemoveAt(0);


        }


    }
    void PreviewMoveUP()
    {

        if (previewTetrominoPrefabDown != null)
        {

            previewTetrominoPrefabUp = previewTetrominoPrefabDown;

            previewTetrominoPrefabUp.transform.position = tetrominoPreviewTransformUp.position;

        }

    }
    void PreviewInstantiateTetromino(int rand)
    {
        previewTetrominoPrefabDown = Instantiate(tetrominoPrefabsDemo[rand], tetrominoPreviewTransformDown.position, Quaternion.identity);

    }
    #endregion
    #region Hold
    public void HoldInstantiateTetromino()
    {

        //生成在hold
        holdTetromino = Instantiate(tetrominoPrefabsDemo[currentTetromino], holdTrans.position, Quaternion.identity);

        holdIndex = currentTetromino;


        isHold = true;
        Destroy(tetromino);
        holdCount++;
        SpawnTetromino();

    }
    public void Hold()
    {
        isCount = (holdCount == 0);


        if (isCount)
        {
            if (isHold)
            {

                Destroy(holdTetromino);

                holdTetromino = Instantiate(tetrominoPrefabsDemo[currentTetromino], holdTrans.position, Quaternion.identity);

                Destroy(tetromino);
                holdCount++;
                //生成下墜預覽方塊
                Instantiate(ghostPrefabs[holdIndex], new Vector3(tetrominoSpawnerTransform.position.x, tetrominoSpawnerTransform.position.y, 0.2f), Quaternion.identity);
                //生成方塊
                tetromino = Instantiate(tetrominoPrefabs[holdIndex], tetrominoSpawnerTransform.position, Quaternion.identity);
                tetrominoIndexList.Add(tetromino);
                holdIndex = currentTetromino;
            }
            else
            {
                HoldInstantiateTetromino();
            }

        }
        else
        {
            audioSourceFall.PlayOneShot(audioClipsFull[0]);
        }


    }
    #endregion
    /// <summary>
    /// 遊戲結束
    /// </summary>
    public void FinishGame()
    {
        imageWin.SetActive(true);

    }

    public void FinishWave()
    {
       
        tetromino.GetComponent<Tetromino>().enabled = false;
        moveCover.SetActive(true);
        StartCoroutine(nameof(MoveGround));
        background.enabled = false;
        moveCover.SetActive(false);
        tetromino.GetComponent<Tetromino>().enabled = true;
    }
    IEnumerator MoveGround()
    {
        background.enabled = true;
        yield return new WaitForSeconds(1.5f);
        StopCoroutine(nameof(MoveGround));

    }




}
