using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float AutoDropTime => autoDropTime;
    public static GameManager Instance;
    [SerializeField] float autoDropTime = 1.5f;
    private void Awake()
    {
       Instance = this;
    }
}
