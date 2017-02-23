using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Map map;
    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private LifesManager lifesManager;

    [SerializeField]
    private GameObject dicePrefab;

    [SerializeField]
    private Transform DicesFolder;

    [SerializeField]
    Button pauseButton;

    [SerializeField]
    Button restartButton;
    [SerializeField]
    Button restartGameOverButton;

    //[SerializeField]
    //int level;
    //[SerializeField]
    //int world;

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject completedPanel;


    public static string world = "Air";
    public static int level = 1;


    void Awake()
    {
        if (level == 0) level = 1;
        // Get the starting level and then:
        levelManager.CalculateMapSize(world, level);
        map.InitializeMap(levelManager.GetBoardHeight(), levelManager.GetBoardWidth());

        levelManager.LoadLevel(world,level);

    }

    void Start()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        completedPanel.SetActive(false);
    }

   // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CreateRandomDice();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel("Air", 1);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadLevel("Air", 2);
        }

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    FindObjectOfType<MapGenerator>().CleanMap();
        //}

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    FindObjectOfType<MapGenerator>().CreateMap();
        //}

    }

    private void LoadLevel(string world, int lvl)
    {
        levelManager.CalculateMapSize(world, lvl);
        map.InitializeMap(levelManager.GetBoardHeight(), levelManager.GetBoardWidth());

        levelManager.LoadLevel(world, lvl);
    }

    private void CreateRandomDice()
    {
        int y, x;

        x = Random.Range(0, levelManager.GetBoardWidth());
        y = Random.Range(0, levelManager.GetBoardHeight());

        while (!map.IsEmpty(x, y) && !map.IsFull())
        {
            x = Random.Range(0, levelManager.GetBoardWidth());
            y = Random.Range(0, levelManager.GetBoardHeight());
        }

        if (!map.IsFull())
        {
            GameObject newDice = Instantiate(dicePrefab, new Vector3(x, 0.5f, y), Quaternion.identity) as GameObject;
            Dice newDiceScript = newDice.GetComponent<Dice>();
            newDiceScript.SetDiceCoorX(x);
            newDiceScript.SetDiceCoorY(y);

            int newUpperFace = Random.Range(1, 7);
            newDiceScript.SetUpperFace(newUpperFace);

            map.AddDice(newDiceScript);


            newDice.transform.GetChild(1).transform.GetChild(0).GetComponent<FaceDetector>().TurnDiceTo(newUpperFace, Random.Range(1, 7));
            newDice.transform.SetParent(DicesFolder);
        }
    }
    public void Pause()
    {
        if (!pausePanel.activeSelf)
        {
            pausePanel.SetActive(true);

            ScoreManager.UpdateDataLevelInPanel(pausePanel.transform.GetChild(0), pausePanel.transform.GetChild(1).GetComponent<Text>());

            Time.timeScale = 0;
            if (LifesManager.lifes < 1)
            {
                restartButton.interactable = false;
            }
            else
            {
                restartButton.interactable = true;
            }
        }
        else if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            
            Time.timeScale = 1;
        }
    }
    public void GameOver()
    {
        if (LifesManager.lifes < 1)
        {
            restartGameOverButton.interactable = false;
        }
        else
        {
            restartGameOverButton.interactable = true;
        }
        gameOverPanel.SetActive(true);
        ScoreManager.UpdateDataLevelInPanel(gameOverPanel.transform.GetChild(0), gameOverPanel.transform.GetChild(1).GetComponent<Text>());
        Time.timeScale = 0;
        pauseButton.interactable = false;

        lifesManager.LoseLife();
    }

    public void Completed()
    {
        completedPanel.SetActive(true);
        ScoreManager.UpdateDataLevelInPanel(completedPanel.transform.GetChild(0), completedPanel.transform.GetChild(1).GetComponent<Text>());
        Time.timeScale = 0;
        pauseButton.interactable = false;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
        //lifesManager.LoseLife();
    }

    public void RestartLosingLife()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
        lifesManager.LoseLife();
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        lifesManager.LoseLife();
    }

    public void NextLevel()
    {
        world = "Air"; //calcular si es el ultimo nivel del mundo
        level = level+1;
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
