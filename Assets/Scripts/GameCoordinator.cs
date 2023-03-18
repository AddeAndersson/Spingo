using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    
	public Dice dice = null;
    public Player[] players = null;
    private int currentPlayerIdx = 0;
    private PlayerState[] playerStates;

    public void diceClicked()
    {
        this.dice.rollDice();
        this.players[currentPlayerIdx].State = PlayerState.chooseLetter;
        print($"{this.players[currentPlayerIdx].Name} rolled {this.dice.ActiveSide}.");
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
