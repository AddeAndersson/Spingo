using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    
	public Dice dice = null;
    public Player[] players = null;
    private Player currentPlayer = null;
    private int currentPlayerIdx;
    private PlayerState[] playerStates;

    public void diceClicked()
    {
        dice.rollDice();
        currentPlayer.setState(PlayerState.chooseLetter);
        print(currentPlayer.getName() + " rolled " + dice.getActiveSide() + ".");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(dice == null)
        {
            print("No dice was found.");
        }

        if(players.Length == 0)
        {
            print("No players were found.");
        }

        currentPlayerIdx = Random.Range(0, players.Length - 1);
        currentPlayer = players[currentPlayerIdx];
        currentPlayer.setState(PlayerState.rollDice);
        currentPlayer.makeAction();

        playerStates = new PlayerState[players.Length];
        for(int i = 0; i < players.Length; i++)
        {
            playerStates[i] = players[i].getState();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(playerStates[i] != players[i].getState())
            {
                playerStates[i] = players[i].makeAction();
            }
        }
    }
}
