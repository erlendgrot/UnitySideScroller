
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause Menu")]
    [SerializeField] private GameObject pauseScreen;

    [Header ("Level Complete")]
    [SerializeField] private GameObject levelCompleteScreen;

    
    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        levelCompleteScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If pause screen is active -> unpause. If not active -> pause
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            } else {
                PauseGame(true);
            }
        }
    }
    #region Level Complete
    
    public void levelComplete()
    {
        //Time.timeScale = 1;
        levelCompleteScreen.SetActive(true);
    }

    public void pickNextLevel()
    {
        //Time.timeScale = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    #endregion

    #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    // Game over functions
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    // Does not work when paused
    public void MainMenu()
    {
        //Time.timeScale = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    #endregion

    #region Pause

    public void PauseGame(bool status)
    {
        // If status == true -> pause the game
        pauseScreen.SetActive(status);

        // Bugged
        // To actually pause the game. 
       // if (status)
       // {
       //     Time.timeScale = 0; // Pauses the game
        //} else {
       //     Time.timeScale = 1; // Unpauses the game
       // }
        
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }


    #endregion
}

