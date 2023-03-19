using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCoordinator : MonoBehaviour
{
    
	public Dice dice = null;
    public Player[] players = null;
    public InputField[] letters = null;
    private int currentPlayerIdx = 0;
    private PlayerState[] playerStates;

    public void diceClicked()
    {
        this.dice.rollDice();
        this.players[currentPlayerIdx].State = PlayerState.chooseLetter;
        print($"{this.players[currentPlayerIdx].Name} rolled {this.dice.ActiveSide}.");

        // Set letters
        for(int i = 0; i < 4; i++)
        {
            letters[i].GetComponent<Letter>().Clickable = true;
            letters[i].text = "" + this.dice.ActiveSide[i]; // Conversion to string
            letters[i].image.color = Color.white;
        }

        // Enable letters
    }

    public void letterClicked(string letter)
    {
        for(int i = 0; i < 4; i++)
        {
            letters[i].GetComponent<Letter>().Clickable = false;
        }

        print(this.players[currentPlayerIdx].Name + " chose the letter '" + letter + "'.");
        
        for(int i = 0; i < players.Length; i++)
        {
            this.players[i].State = PlayerState.placeLetter;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(this.dice == null)
        {
            print("No dice was found.");
        }

        if(this.players.Length == 0)
        {
            print("No players were found.");
        }
        
        this.currentPlayerIdx = Random.Range(0, this.players.Length - 1);
        this.players[currentPlayerIdx].State = PlayerState.rollDice;
        this.players[currentPlayerIdx].makeAction();

        this.playerStates = new PlayerState[players.Length];
        for(int i = 0; i < players.Length; i++)
        {
            this.playerStates[i] = this.players[i].State;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < this.players.Length; i++)
        {
            if(this.playerStates[i] != this.players[i].State)
            {
                this.playerStates[i] = this.players[i].makeAction();
            }
        }
    }
}
