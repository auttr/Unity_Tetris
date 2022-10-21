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
    [SerializeField] GameObject[] tetrominoPrefabsDemo;
    [SerializeField] Transform tetrominoSpawnerTransform;
    [SerializeField] Transform tetrominoPreviewTransformUp;
    [SerializeField] Transform tetrominoPreviewTransformDown;
    [SerializeField] float autoDropTime = 0.7f;
    [SerializeField, Header("遊戲結束")] GameObject imageWin;
    public AudioSource audioSourceFall;
    [Header("到地面聲音")] public AudioClip[] audioClipsFull;
    GameObject previewTetrominoPrefabDown;
    GameObject previewTetrominoPrefabUp;
    GameObject previewTetrominoPrefabSpawn;
    [SerializeField]
    List<int> randList = new();
    private void Awake()
    {
        Instance = this;
        SpawnTetromino();
        SpawnTetromino();
        SpawnTetromino();

        audioSourceFall = GetComponent<AudioSource>();
        //previewTetrominoPrefabDown.transform.position = tetrominoPreviewTransformDown.position;
    }
   
    public void SpawnTetromino()
    {
        int r = Random.Range(0, tetrominoPrefabs.Length);
        randList.Add(r);
        PreviewMoveSpawn(r);
        PreviewMoveUP(r);
        InstantiateTetromino(r);


    }
    public void FinishGame()
    {
        imageWin.SetActive(true);

    }
    void PreviewMoveSpawn(int rand)
    {
        if (previewTetrominoPrefabUp != null)
        {
            Instantiate(tetrominoPrefabs[randList[0]], tetrominoSpawnerTransform.position, Quaternion.identity);
            Instantiate(ghostPrefabs[randList[0]], new Vector3(tetrominoSpawnerTransform.position.x, tetrominoSpawnerTransform.position.y, 0.2f), Quaternion.identity);
            randList.RemoveAt(0);
        }

    }
    void PreviewMoveUP(int rand)
    {

        if (previewTetrominoPrefabDown != null)
        {
            previewTetrominoPrefabUp = previewTetrominoPrefabDown;

            previewTetrominoPrefabUp.transform.position = tetrominoPreviewTransformUp.position;

        }

    }
    void InstantiateTetromino(int rand)
    {
        previewTetrominoPrefabDown = Instantiate(tetrominoPrefabsDemo[rand], tetrominoPreviewTransformDown.position, Quaternion.identity);
    }


}
