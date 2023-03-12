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
	
	public string getActiveSide()
	{
		return activeSide;
	}

	public DiceState getState()
	{
		return state;
	}

	public void rollDice()
	{
		// Generate random number between 0 and 11
    	int randomedNumber = Random.Range(0, diceSides.Length);
    	activeSide = diceSides[randomedNumber];
	}

	// Start is called before the first frame update
	void Start()
    {
        activeSide = diceSides[0];
        state = DiceState.waitingToBeRolled;
    }

    // Update is called once per frame
    void Update()
    {
    	
    }
}
