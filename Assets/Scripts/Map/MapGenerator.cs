using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Map))]
 
public class MapGenerator : MonoBehaviour
{
    public GameObject mapUnityPrefab;
    public Transform hierarchyParent;

    Map map;
    int boardSize;

	// Use this for initialization
	void Start ()
    {
        map = GetComponent<Map>();
        boardSize = map.boardSize;

        for (int i=0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject newMapUnity = Instantiate(mapUnityPrefab, new Vector3(i, -0.5f, j), Quaternion.identity) as GameObject;
                newMapUnity.transform.SetParent(hierarchyParent);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
