using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour
{
    private static float time;
	private bool inLava;
	private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        time = 11;
		inLava = false;
    }

    // Update is called once per frame
    void Update()
    {
		// if (inLava == true) {
		// 	time = time - Time.deltaTime;
		// 	Debug.Log(time);
		// } 
		
		// if (time <= 1) {
		// 	gameOver = true;
		// 	Debug.Log(gameOver);
		// }
		// if (inLava){
  //           HeartSystem.LavaDamage();
  //       }

        
    }
/*
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<DoraMouse>())
        {
			col.GetComponent<HeartSystem>().LavaDamage();
        }
    }
*/
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<DoraMouse>())
        {
			col.GetComponent<HeartSystem>().LavaDamage();
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<DoraMouse>())
        {
			inLava = false;
        }
    }
}
