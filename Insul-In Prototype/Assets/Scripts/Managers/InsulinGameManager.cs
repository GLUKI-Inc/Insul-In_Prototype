using UnityEngine;
using UnityEngine.SceneManagement;

public class InsulinGameManager : MonoBehaviour
{
    private Scene scene;
    
    public enum GAMESTATE
    {
        STARTMENU,
        PAUSED,
        PLAYING,
        TUTORIAL,
        GAMEOVER
    }

    public GAMESTATE currentState { get; private set; }

    //GameManager is a singleton and handles the state of the game
    public static InsulinGameManager Instance{ get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            // Add a listener to be called when a scene loads
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;

            Instance = this;
            DontDestroyOnLoad(gameObject);

            scene = SceneManager.GetActiveScene();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Clean up listener
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log($"Current scene: {GameSceneManager.Instance.currentScene} - {(int)GameSceneManager.Instance.currentScene}");
        
        if (scene.buildIndex == (int)GameSceneManager.Scenes.STARTMENU)     // if scene is start menu
        {
            if(Time.timeScale != 1)
                Time.timeScale = 1;     // reassure me that it is NOT paused
            currentState = GAMESTATE.STARTMENU;
        }
        else if(scene.buildIndex == (int)GameSceneManager.Scenes.INSULINBALLTEST)       // if scene is insulinballtest
        {
            currentState = GAMESTATE.PLAYING;
            if (Time.timeScale != 1)
                Time.timeScale = 1;
        }
        else if(scene.buildIndex == (int)GameSceneManager.Scenes.INSULINBALLTUTORIAL)
        {
            currentState = GAMESTATE.TUTORIAL;
            if (Time.timeScale != 1)
                Time.timeScale = 1;
        }
    }


    public void StartGame()
    {
        GameSceneManager.Instance.StartGame();
    }

    public void EnterTutorial()
    {
        GameSceneManager.Instance.EnterTutorial();
    }

    public void ReturnToStartMenu()
    {
        GameSceneManager.Instance.ReturnToStartMenu();
    }

    public void Pause()
    {
        if (currentState == GAMESTATE.PLAYING)  // If playing, pause game
        {
            currentState = GAMESTATE.PAUSED;
            Time.timeScale = 0;
        }
        else if (currentState == GAMESTATE.PAUSED) // If game is already paused, unpause game
        {
            currentState = GAMESTATE.PLAYING;
            Time.timeScale = 1;
        }
    }

    public void GameOver()
    {
        currentState = GAMESTATE.GAMEOVER;
        Time.timeScale = 0; //pause time in the game since the game is over :)
    }

    public void QuitGame()
    {
        // Shut down game:
        GameSceneManager.Instance.Quit();
        Application.Unload();

    }
}
