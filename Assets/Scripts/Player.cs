using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    // 2D array representing player tiles
    private InputField[,] tiles;

    public int Points { get => points; set => points = value; }
    public string Name { get => name; set => name = value; }
    public PlayerState State { get => state; set => state = value; }

    // This function should update the UI elements based on the player state
    public PlayerState makeAction()
	{
		if(this.State == PlayerState.rollDice)
        {
            // Enable dice roll for player
            print($"It is {this.Name}'s turn to roll the dice!");
            //state = PlayerState.chooseLetter;
        }
        else if(this.State == PlayerState.chooseLetter)
        {
            // Enable letter selection from active dice side
            print($"It is {this.Name}'s turn to choose a letter!");
            //state = PlayerState.placeLetter;
        }
        else if(this.State == PlayerState.placeLetter)
        {
            // Enable player to place letter  in tiles
            print("Everybody place your letter in an available tile!");
            //state = PlayerState.idle;
        }
        else if(this.State == PlayerState.idle)
        {
            // Do nothing
            print($"{this.Name} has completed his turn!");
        }

        return this.State;
	}

    void Awake()
    {
        this.Name = "Bob";
        this.Points = 0;
        this.State = PlayerState.idle;
        
        // Set tiles
        this.tiles = new InputField[5, 5];
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                this.tiles[i, j] = GameObject.Find("Tile" + i.ToString() + j.ToString()).GetComponent<InputField>();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
