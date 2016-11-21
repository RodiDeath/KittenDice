﻿using UnityEngine;
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
        CreateMap();
    }

    public void CreateMap()
    {
        map = GetComponent<Map>();
        boardSize = map.boardSize;

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject newMapUnity = Instantiate(mapUnityPrefab, new Vector3(i, -0.5f, j), Quaternion.identity) as GameObject;
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

    // Update is called once per frame
    void Update ()
    {
	
	}
}
