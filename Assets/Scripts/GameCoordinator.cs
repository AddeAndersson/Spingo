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
    private InputField[] spingoLetters = null;
    private int currentPlayerIdx = 0;
    private PlayerState[] playerStates;
    private string currentLetter;
    public GameLog gameLog;
    private bool coroutineAllowed = true;
    private string[] alphabet = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
        "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Å", "Ä", "Ö"};

    public string CurrentLetter { get => currentLetter; set => currentLetter = value; }

    public void diceClickedEntry()
    {
        StartCoroutine(diceClicked());
    }

    private IEnumerator diceClicked()
    {
        this.coroutineAllowed = false;
        if(!this.dice.Clickable || !this.coroutineAllowed) yield return new WaitForSeconds(0);
        
        // Reset, hide, and disable spingo letters
        for(int i = 0; i < this.spingoLetters.Length; i++)
        {
            this.spingoLetters[i].image.color = Color.white;
            this.spingoLetters[i].gameObject.SetActive(false);
        }

        // Reset, enable, original letters
        for(int i = 0; i < this.letters.Length; i++)
        {
            this.letters[i].image.color = Color.white;
            this.letters[i].gameObject.SetActive(true);
        }

        // "Animate" dice
        string previousSide;
        for(int r = 0; r < 10; r++)
        {
            previousSide = this.dice.ActiveSide;
            this.dice.rollDice();
            if(previousSide == this.dice.ActiveSide)
            {
                r--;
                continue;
            }

            for(int i = 0; i < this.letters.Length; i++)
            {
                this.letters[i].text = this.dice.ActiveSide == "SPINGO!" ? "?" : "" + this.dice.ActiveSide[i];
            }
            yield return new WaitForSeconds((r + 1) * 0.05f);
        }

        this.players[currentPlayerIdx].State = PlayerState.chooseLetter;

        if(this.dice.ActiveSide == "SPINGO!")
        {
            // Hide original letters
            for(int i = 0; i < this.letters.Length; i++)
            {
                this.letters[i].gameObject.SetActive(false);
            }

            // Enable spingo letters
            for(int i = 0; i < this.spingoLetters.Length; i++)
            {
                this.spingoLetters[i].gameObject.SetActive(true);
                this.spingoLetters[i].GetComponent<Letter>().Clickable = true;
            }
        }
        else
        {
            // Hide spingo letters
            for(int i = 0; i < this.spingoLetters.Length; i++)
            {
                this.spingoLetters[i].gameObject.SetActive(false);
            }

            // Enable default letters
            for(int i = 0; i < 4; i++)
            {
                this.letters[i].gameObject.SetActive(true);
                this.letters[i].GetComponent<Letter>().Clickable = true;
            }
        }
        

        this.dice.Clickable = false;
        string end = this.dice.ActiveSide == "SPINGO!" ? "" : ".";
        this.gameLog.write($"{this.players[currentPlayerIdx].Name} rolled {this.dice.ActiveSide}{end}");
        this.coroutineAllowed = true;
    }

    public void letterClicked(string letter)
    {
        this.CurrentLetter = letter;

        for(int i = 0; i < this.letters.Length; i++)
        {
            this.letters[i].GetComponent<Letter>().Clickable = false;
        }

        for(int i = 0; i < this.spingoLetters.Length; i++)
        {
            this.spingoLetters[i].GetComponent<Letter>().Clickable = false;
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

        // Instantiate all letters
        this.spingoLetters = new InputField[alphabet.Length];
        for(int i = 0; i < alphabet.Length; i++)
        {
            this.spingoLetters[i] = Instantiate(this.letters[0], GameObject.Find("Letters").transform);
            this.spingoLetters[i].text = alphabet[i];
            this.spingoLetters[i].gameObject.name = "Spingo" + alphabet[i];
            float y = i < 15 ? 50.0f : -50.0f;
            float x = i < 15 ? -700.0f + i * 100.0f : -650.0f + (i - 15.0f) * 100.0f;
            this.spingoLetters[i].gameObject.transform.localPosition = new Vector3(x, y, 0.0f);
            this.spingoLetters[i].gameObject.SetActive(false);
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
            for(int i = 0; i < this.letters.Length; i++)
            {
                this.letters[i].text = "";
                this.letters[i].image.color = Color.white;
                this.letters[i].gameObject.SetActive(false);
            }

            for(int i = 0; i < this.spingoLetters.Length; i++)
            {
                this.spingoLetters[i].image.color = Color.white;
                this.spingoLetters[i].gameObject.SetActive(false);
            }
        }
    }
}
