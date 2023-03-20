using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCoordinator : MonoBehaviour
{
    
	public Dice dice = null;
    public Player[] players = null;
    public InputField[] letters = null;
    private int currentPlayerIdx = 0;
    private PlayerState[] playerStates;
    private string currentLetter;
    public GameLog gameLog;

    public string CurrentLetter { get => currentLetter; set => currentLetter = value; }

    public void diceClicked()
    {
        if(!this.dice.Clickable) return;
        
        this.dice.rollDice();
        this.players[currentPlayerIdx].State = PlayerState.chooseLetter;
        gameLog.write($"{this.players[currentPlayerIdx].Name} rolled {this.dice.ActiveSide}.");

        // Set letters
        if(this.dice.ActiveSide != "Spingo!")
        {
            for(int i = 0; i < 4; i++)
            {
                this.letters[i].GetComponent<Letter>().Clickable = true;
                this.letters[i].text = "" + this.dice.ActiveSide[i]; // Conversion to string
                this.letters[i].image.color = Color.white;
            }
        }
        else
        {
            // Spingo!
        }
    }

    public void letterClicked(string letter)
    {
        this.CurrentLetter = letter;

        for(int i = 0; i < 4; i++)
        {
            this.letters[i].GetComponent<Letter>().Clickable = false;
        }
        
        gameLog.write($"Choose a tile to place '{letter}'.");
        
        for(int i = 0; i < players.Length; i++)
        {
            this.players[i].State = PlayerState.placeLetter;
        }

        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                for(int n = 0; n < 5; n++)
                {
                    if(this.players[i].tiles[j, n].text == "")
                    {
                        this.players[i].tiles[j, n].GetComponent<Tile>().Clickable = true;
                        this.players[i].tiles[j, n].image.color = Color.green;
                    }
                }
            }
        }
    }

    public void tileClicked(int playerIdx, int row, int column)
    {
        this.players[playerIdx].tiles[row, column].text = CurrentLetter;
        this.players[playerIdx].State = PlayerState.idle;

        for(int j = 0; j < 5; j++)
        {
            for(int n = 0; n < 5; n++)
            {
                this.players[playerIdx].tiles[j, n].GetComponent<Tile>().Clickable = false;
                this.players[playerIdx].tiles[j, n].image.color = Color.white;
            }
        }

        // Set letters
        for(int i = 0; i < 4; i++)
        {
            this.letters[i].GetComponent<Letter>().Clickable = false;
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
        this.dice.Clickable = true;
        this.players[currentPlayerIdx].makeAction(gameLog);

        this.playerStates = new PlayerState[players.Length];
        for(int i = 0; i < players.Length; i++)
        {
            this.playerStates[i] = this.players[i].State;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int idlePlayers = 0;

        for(int i = 0; i < this.players.Length; i++)
        {
            if(this.playerStates[i] != this.players[i].State)
            {
                this.playerStates[i] = this.players[i].makeAction(gameLog);
            }

            if(this.playerStates[i] == PlayerState.idle)
            {
                idlePlayers++;
            }
        }

        if(idlePlayers == this.players.Length)
        {
            this.currentPlayerIdx = (this.currentPlayerIdx + 1) % this.players.Length;
            this.players[currentPlayerIdx].State = PlayerState.rollDice;
            this.dice.Clickable = true;

            // Set letters
            for(int i = 0; i < 4; i++)
            {
                this.letters[i].text = "";
                this.letters[i].image.color = Color.white;
            }
        }
    }
}
