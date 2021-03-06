using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class KeyTextScript : MonoBehaviour
{

	private static int keyCount = 0; 
	Text keyText; 
	

    // Start is called before the first frame update
    void Start()
    {
        keyText = GetComponent<Text>();
		keyText.text = "0 of 5 keys found";

    }

    // Update is called once per frame
    void Update()
    {

		
		if (DoraMouse.keys == 1) {
			keyText.text = "1 of 5 keys found";
		}

		if (DoraMouse.keys == 2) {
			keyText.text = "2 of 5 keys found";
		}

		if (DoraMouse.keys == 3) {
			keyText.text = "3 of 5 keys found";
		}

		if (DoraMouse.keys == 4) {
			keyText.text = "4 of 5 keys found";
		}

		if (DoraMouse.keys == 5) {
			keyText.text = "ALL KEYS FOUND!";
		}

    }
}
