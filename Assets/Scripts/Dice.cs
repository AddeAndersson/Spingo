using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	private string[] diceSides = {
			"Spingo!",
			"KAVQ",
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

	private string activeSide;
	private bool clickable;

    public string ActiveSide { get => activeSide; set => activeSide = value; }
    public bool Clickable { get => clickable; set => clickable = value; }

    public void rollDice()
	{
		if(!Clickable) return;
		// Generate random number between 0 and 11
    	int randomedNumber = Random.Range(0, diceSides.Length);
    	this.ActiveSide = this.diceSides[randomedNumber];
		Clickable = false;
	}

	void Awake()
    {
        this.ActiveSide = diceSides[0];
        this.Clickable = false;
    }

	// Start is called before the first frame update
	void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
    	
    }
}
