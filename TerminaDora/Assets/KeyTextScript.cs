using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class KeyTextScript : MonoBehaviour
{

	public static int keyCount = 0; 
	Text kText; 
	Vector3 doraPosn; 
    // Start is called before the first frame update
    void Start()
    {
        kText = GetComponent<Text>();
        doraPosn = GameObject.FindGameObjectsWithTag("Player")[0].transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
    	//TRY #1
        // Vector3 temp = transform.position; 
        // temp.x = doraPosn.x + 7.4f; 
        // temp.y = doraPosn.y + 5.5f;
        // transform.position = temp; 
        //Debug.Log(transform.position);

    	//TRY #2
        // transform.position = new Vector3(doraPosn.x + 7.4f, doraPosn.y + 5.5f, 0);

        // Vector3 newPosn = new Vector3(doraPosn.x + 7.4f, doraPosn.y + 5.5f);
        // Vector3 ourPosn = Camera.main.WorldToScreenPoint(newPosn);
        // transform.position = ourPosn; 
        kText.text = "howdy";
    }
}
