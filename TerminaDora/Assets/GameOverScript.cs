using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{

	public static Text displayText; 

    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void endGame(){
    	displayText.text = "GAME OVER";
    }
}
