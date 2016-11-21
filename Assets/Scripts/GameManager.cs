using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Map map;
    public GameObject dicePrefab;

    public Transform DicesFolder;



	// Use this for initialization
	void Start ()
    {
       
    }

   // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CreateRandomDice();
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

    private void CreateRandomDice()
    {
        int y, x;

        x = Random.Range(0, map.boardSize);
        y = Random.Range(0, map.boardSize);

        while (!map.IsEmpty(x, y) && !map.IsFull())
        {
            x = Random.Range(0, map.boardSize);
            y = Random.Range(0, map.boardSize);
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
