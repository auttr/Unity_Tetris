using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace auttr
{
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
        [SerializeField, Header("�C������")] GameObject imageWin;
        public AudioSource audioSourceFall;
        [Header("��a���n��")] public AudioClip[] audioClipsFull;
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
        [SerializeField, Header("Image_���ʾB��")]
        GameObject moveCover;
        [SerializeField, Header("�p��")]
        GameObject[] mosters;
        TextMeshProUGUI waveText;
        int waveMax = 3;
        [SerializeField, Header("Bossŧ��")]
        GameObject bossCome;
        TextMeshProUGUI scoreText;
        int wave = 1;
        public int totalScore;
        PlayerInput playerInput;
        [SerializeField] TextMeshProUGUI finishScoreText;
        [SerializeField] TextMeshProUGUI finishGameText;
        public bool isWin;
        public bool isTooHeigh;
        private void Awake()
        {
            Instance = this;
            SpawnTetromino();
            SpawnTetromino();
            SpawnTetromino();
            audioSourceFall = GetComponent<AudioSource>();
            background = GameObject.Find("BackgroundControl").GetComponent<Background>();
            background.enabled = false;
            Instantiate(mosters[0]);
            waveText = GameObject.Find("Text_�^�M��").GetComponent<TextMeshProUGUI>();
            scoreText = GameObject.Find("Text_Score").GetComponent<TextMeshProUGUI>();
            playerInput = FindObjectOfType<PlayerInput>();
            //finishScoreText = GameObject.Find("Text_����").GetComponent<TextMeshProUGUI>();
            // finishGameText = GameObject.Find("Text_�C������").GetComponent<TextMeshProUGUI>();
            //previewTetrominoPrefabDown.transform.position = tetrominoPreviewTransformDown.position;
        }
        private void Start()
        {
            waveText.text = "�^�X: " + wave + "/" + waveMax;
        }
        private void Update()
        {
            ScoreText();
        }
        /// <summary>
        /// �ͦ����
        /// </summary>
        public void SpawnTetromino()
        {

            int r = Random.Range(0, tetrominoPrefabs.Length);
            randList.Add(r);
            //���ʨ�ͦ���
            PreviewMoveSpawn();
            //�q�w���U���ʨ�w���W
            PreviewMoveUP();
            //�ͦ�Demo�w���U
            PreviewInstantiateTetromino(r);
        }
        /// <summary>
        /// �ͦ�:���ʨ�ͦ��I�A���ʨ�W��A�ͦ��b�U��
        /// </summary>
        #region Spawn
        void PreviewMoveSpawn()
        {
            if (previewTetrominoPrefabUp != null)
            {
                //�R��up��m�h������
                previewTetrominoPrefabSpawn = previewTetrominoPrefabUp;
                Destroy(previewTetrominoPrefabSpawn);


                //�ͦ��U�Y�w�����
                Instantiate(ghostPrefabs[randList[0]], new Vector3(tetrominoSpawnerTransform.position.x, tetrominoSpawnerTransform.position.y, 0.2f), Quaternion.identity);
                //�ͦ����
                tetromino = Instantiate(tetrominoPrefabs[randList[0]], tetrominoSpawnerTransform.position, Quaternion.identity);


                currentTetromino = randList[0];



                //�[�Jlist�A��ghost���Ӫ���
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

            //�ͦ��bhold
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
                    //�ͦ��U�Y�w�����
                    Instantiate(ghostPrefabs[holdIndex], new Vector3(tetrominoSpawnerTransform.position.x, tetrominoSpawnerTransform.position.y, 0.2f), Quaternion.identity);
                    //�ͦ����
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
        /// �C������
        /// </summary>
        public void FinishGame()
        {
            imageWin.SetActive(true);
            playerInput.enabled = false;

            finishScoreText.text = "Score: " + totalScore * 10;

            if (!isTooHeigh)
            {
                if (isWin)
                {
                    finishGameText.text = "���󦨥\";
                }
                else
                {
                    finishGameText.text = "�Q��ͮ���";
                }
            }
            else
            {
                finishGameText.text = "�Ӱ����������������������F";
            }
            Time.timeScale = 0;
        }

        public void FinishWave()
        {
            Destroy(tetromino);
            holdCount++;
            // tetromino.GetComponent<Tetromino>().enabled = false;
            moveCover.SetActive(true);
            StartCoroutine(MoveGround());
            wave++;
            waveText.text = "�^�X: " + wave + "/" + waveMax;
            InstantiateMonster();
        }
        IEnumerator MoveGround()
        {

            background.enabled = true;
            yield return new WaitForSeconds(2.5f);

            background.enabled = false;
            moveCover.SetActive(false);
            // tetromino.GetComponent<Tetromino>().enabled = true;
            SpawnTetromino();
            StopCoroutine(MoveGround());
        }
        void InstantiateMonster()
        {
            Instantiate(mosters[wave - 1]);
            if (wave == 3)
            {
                StartCoroutine(BossComeIE());

            }
        }
        IEnumerator BossComeIE()
        {
            for (int i = 0; i < 10; i++)
            {
                bossCome.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                bossCome.SetActive(false);
                yield return new WaitForSeconds(0.1f);
            }
            StopCoroutine(BossComeIE());

        }


        void ScoreText()
        {
            scoreText.text = "Score: " + totalScore * 10;
        }


    }

}
