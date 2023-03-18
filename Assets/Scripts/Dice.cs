using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceState {
	rolled,
	waitingToBeRolled,
};

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
	private DiceState state;

    public DiceState State { get => state; set => state = value; }
    public string ActiveSide { get => activeSide; set => activeSide = value; }

    public void rollDice()
	{
		// Generate random number between 0 and 11
    	int randomedNumber = Random.Range(0, diceSides.Length);
    	this.ActiveSide = this.diceSides[randomedNumber];
	}

	void Awake()
    {
        this.ActiveSide = diceSides[0];
        this.State = DiceState.waitingToBeRolled;
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
