using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Map map;
    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private GameObject dicePrefab;

    [SerializeField]
    private Transform DicesFolder;

    [SerializeField]
    int level;


    void Awake()
    {
        if (level == 0) level = 1;
        // Get the starting level and then:
        levelManager.CalculateMapSize(level);
        map.InitializeMap(levelManager.GetBoardHeight(), levelManager.GetBoardWidth());

        levelManager.LoadLevel(level);

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
            LoadLevel(1);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadLevel(2);
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

    private void LoadLevel(int lvl)
    {
        levelManager.CalculateMapSize(lvl);
        map.InitializeMap(levelManager.GetBoardHeight(), levelManager.GetBoardWidth());

        levelManager.LoadLevel(lvl);
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
}
