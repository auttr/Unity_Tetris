using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject enemyCountCursor;
    [SerializeField] GameObject comboCursor;
    [SerializeField] GameObject OPCursor;

    [SerializeField] GameObject enemyCountImage;
    [SerializeField] GameObject attackImage;
    [SerializeField] GameObject comboImage;
    [SerializeField] GameObject OPImage;
    private void Awake()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        attackImage.SetActive(true);
        yield return new WaitForSeconds(5f);
        attackImage.SetActive(false);
        StartCoroutine(EnemyCount());
    }

    IEnumerator EnemyCount()
    {
        enemyCountImage.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            enemyCountCursor.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            enemyCountCursor.SetActive(false);
        }
        enemyCountImage.SetActive(false);
        StartCoroutine(Combo());
    }
    IEnumerator Combo()
    {
        comboImage.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            comboCursor.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            comboCursor.SetActive(false);
        }
        comboImage.SetActive(false);
        StartCoroutine(OPTime());
    }
    IEnumerator OPTime()
    {
        OPImage.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            OPCursor.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            OPCursor.SetActive(false);
        }
        OPImage.SetActive(false);
        StopAllCoroutines();
        enabled = false;
    }

}
