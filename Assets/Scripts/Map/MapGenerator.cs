using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Map))]
 
public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject mapUnityPrefab;
    [SerializeField]
    private Transform hierarchyParent;

    [SerializeField]
    private Map map;

    [SerializeField]
    private int boardSize;

	// Use this for initialization
	void Start ()
    {
        //CreateMap();
    }

    public void CreateMap(int height, int width)
    {
        CleanMap();
        map = GetComponent<Map>();
        boardSize = map.GetBoardSize();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject newMapUnity = Instantiate(mapUnityPrefab, new Vector3(j, -0.5f, i), Quaternion.identity) as GameObject;
                newMapUnity.transform.SetParent(hierarchyParent);
            }
        }
    }

    public void CleanMap()
    {
        foreach (Transform child in transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
    }

}
