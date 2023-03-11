using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
	private string[] diceSides = {
			"KAVQ",
			"Spingo",
			"GEJÖ",
			"DESO",
			"KIER",
			"CAHI",
			"NÄFI",
			"TXUP",
			"LYOS",
			"ZABM",
			"REDN",
			"LATÅ"
		};

	private bool isClicked;

	public string activeSide;

	// Start is called before the first frame update
	void Start()
    {
        isClicked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isClicked)
        {
        	// Generate random number between 0 and 11
        	int randomedNumber = Random.Range(0, 11);
        	activeSide = diceSides[randomedNumber];
        	print("Dice rolled: " + activeSide);
        	isClicked = false;
        }
    }
}
