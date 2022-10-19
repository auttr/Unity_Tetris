using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float AutoDropTime => autoDropTime;
    public static GameManager Instance;
    [SerializeField] GameObject[] tetrominoPrefabs;
    [SerializeField] GameObject[] ghostPrefabs;
    [SerializeField] Transform tetrominoSpawnerTransform;
    [SerializeField] float autoDropTime = 0.7f;
    private void Awake()
    {
        Instance = this;
        SpawnTetromino();
    }
    public void SpawnTetromino()
    {
        int r = Random.Range(0, tetrominoPrefabs.Length);
        Instantiate(tetrominoPrefabs[r], tetrominoSpawnerTransform.position, Quaternion.identity);
        Instantiate(ghostPrefabs[r], new Vector3(tetrominoSpawnerTransform.position.x, tetrominoSpawnerTransform.position.y, 0.2f), Quaternion.identity);
    }
}
