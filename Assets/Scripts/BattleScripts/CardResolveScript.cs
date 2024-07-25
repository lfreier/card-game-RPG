using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script attached to each card which resolves its action.
 * Essentially a wrapper for Card class. 
 */

public class CardResolveScript : MonoBehaviour
{
	public Card cardData;

	public void resolveCardPlay(GameObject playedCard)
	{
		GameObject playerHand = GameObject.Find("PlayerArea");
		if (playerHand != null)
		{
			PlayerHandScript tempScript = playerHand.GetComponent("PlayerHandScript") as PlayerHandScript;
			if (tempScript != null)
			{
				tempScript.RemoveCard(playedCard);
			}
			else
			{
				Debug.Log("Can't find PlayerHandScript");
			}
			playedCard.transform.SetParent(gameObject.transform, true);
			playedCard.transform.rotation = Quaternion.Euler(0, 0, 0);

			playCardEffect(playedCard);
		}
	}

	void playCardEffect(GameObject playedCard)
	{
		if (cardData.cardEffect == null)
		{
			Debug.Log("cardEffect null");
		}
		else
		{
			cardData.cardEffect.Apply();
		}
		GameObject.Destroy(playedCard.gameObject);
	}
}
