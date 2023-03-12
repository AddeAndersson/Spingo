using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState {
    rollDice,
    chooseLetter,
    placeLetter,
    idle,
};

public class Player : MonoBehaviour
{

	private string name;
	private int points;
    private PlayerState state;

	public PlayerState makeAction()
	{
		if(state == PlayerState.rollDice)
        {
            // Enable dice roll for player
            print("It is " + name + "'s turn to roll the dice!");
            //state = PlayerState.chooseLetter;
        }
        else if(state == PlayerState.chooseLetter)
        {
            // Enable letter selection from active dice side
            print("It is " + name + "'s turn to choose a letter!");
            //state = PlayerState.placeLetter;
        }
        else if(state == PlayerState.placeLetter)
        {
            // Enable player to place letter  in tiles
            print("Everybody place your letter in an available tile!");
            //state = PlayerState.idle;
        }
        else if(state == PlayerState.idle)
        {
            // Do nothing
            print(name + "has completed his turn!");
        }

        return state;
	}

    private char chooseLetter(string diceSide)
    {
        return diceSide[0];
    }

    private void placeLetter(char letter)
    {
        return;
    }

    public void setName(string playerName)
    {
        name = playerName;
    }

    public string getName()
    {
        return name;
    }

    public int getPoints()
    {
        return points;
    }

    public PlayerState getState()
    {
        return state;
    }

    public void setState(PlayerState newState)
    {
        state = newState;
    }

    // Start is called before the first frame update
    void Start()
    {
    	name = "Bob";
    	points = 0;
        state = PlayerState.idle;
    }
}
