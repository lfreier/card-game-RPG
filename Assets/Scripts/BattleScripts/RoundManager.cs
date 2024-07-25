using UnityEngine;
using System.Collections.Generic;

/* Script to handle rounds and turn mechanics
 * TODO:	This is basic because multiple character's aren't a thing yet. 
 *			Only works for swapping between player/enemy turns
 */

public class RoundManager : MonoBehaviour
{
	enum Turn
	{
		PLAYER = 0,
		ENEMY = 1
	};

	struct CharTurnInfo
	{
		public bool isEnded;
		public bool isActive;
		public short resource;

		public Character character;
		//pointer to hand of cards
		//pointer to character info
	};

	private List<CharTurnInfo> playerCharacters;
	private List<CharTurnInfo> enemyCharacters;

	Turn currentTurn;

	// Use this for initialization
	void Start()
	{
		playerCharacters = new List<CharTurnInfo>();
		enemyCharacters = new List<CharTurnInfo>();

		CharTurnInfo basicP = new CharTurnInfo();
		CharTurnInfo basicE = new CharTurnInfo();
		
		playerCharacters.Add(basicP);
		enemyCharacters.Add(basicE);

		currentTurn = Turn.PLAYER;

		InitTurns();
	}

	/* Switches the active turn between player characters
	 * 
	 */
	public void SwitchActiveTurn(int charIndex)
	{
		int i;
		CharTurnInfo curr;

		for (i = 0, curr = playerCharacters[i]; i < playerCharacters.Count; curr = playerCharacters[++i])
		{
			if (curr.isActive)
			{
				curr.isActive = false;
				curr = playerCharacters[charIndex];
				curr.isActive = true;
				return;
			}
		}
	}

	/* activate the character's turn and de-activates other character turns
	 */
	public void ActivateCharacterTurn(Character charToActivate)
	{
		int i;
		int maxCount = Mathf.Max(playerCharacters.Count, enemyCharacters.Count);
		string nameToCheck = "";
		CharTurnInfo curr;

		for (i = 0; i < maxCount; i ++)
		{
			if (i < playerCharacters.Count)
			{
				nameToCheck = playerCharacters[i].character.GetCharName();
				if (charToActivate.GetCharName().Equals(nameToCheck))
				{
					curr = playerCharacters[i];
					curr.isActive = true;
					break;
				}
				else
				{
					curr.isActive = false;
				}
			}

			if (i < enemyCharacters.Count)
			{
				nameToCheck = enemyCharacters[i].character.GetCharName();
				if (charToActivate.GetCharName().Equals(nameToCheck))
				{
					curr = enemyCharacters[i];
					curr.isActive = true;
					break;
				}
				else
				{
					curr.isActive = false;
				}
			}

		}
	}

	/* end the given character's turn
	 * check if round is over 
	 * then check if turn is over
	 */
	public void EndCharacterTurn()
	{
		//determine way to input which character is which
		//end turn in the playerCharacters or enemyCharacters array

		//loop should continue for each round
		while (true)
		{
			if (!CheckContinueRounds())
			{
				//restart character turns with draw and stuff
				break;
			}
		}
	}

	private bool CheckContinueRounds()
	{
		return false;
	}

	private void InitTurns()
	{
		int i;
		CharTurnInfo curr;

		for (i = 0; i < playerCharacters.Count; i ++)
		{
			curr = playerCharacters[i];
			curr.isEnded = false;
		}

		for (i = 0, curr = enemyCharacters[0]; i < enemyCharacters.Count; i++)
		{
			curr = enemyCharacters[i];
			curr.isEnded = true;
		}

		curr = playerCharacters[0];
		curr.isActive = true;
	}
}
