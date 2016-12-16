using UnityEngine;
using System.Collections;

public class ScrollViewBugCorrector : MonoBehaviour
{
    
	void Start ()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = new Vector3(rt.localPosition.x, 2, rt.localPosition.z);
	}
}
