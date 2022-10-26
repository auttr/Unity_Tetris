using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace auttr
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] GameObject pauseObj;
        PlayerInput playerInput;
        public bool isPause = true;
        private void Awake()
        {
            Screen.SetResolution(410, 720, false);
            playerInput = FindObjectOfType<PlayerInput>();
        }

        public void LoadNextScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
            
        }
        public void ExitGame()
        {
            Application.Quit();
        }
        public void Pause()
        {

            Time.timeScale = (!isPause) ? 0 : 1;
            pauseObj.SetActive((!isPause));
            playerInput.enabled = isPause;

            isPause = !isPause;

        }
        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }

    }

}
