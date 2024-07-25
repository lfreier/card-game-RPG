using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

/* @author lfreier
 * PlayerHandScript.cs
 * 
 * Deals with non-actionable logic for the player's hand.
 * This will mainly be UI and data structure stuff like drawing or removing cards.
 */

public class PlayerHandScript : MonoBehaviour
{
	public GameObject cardPrefab;
	public GameObject playerArea;

	private List<GameObject> playerHandList;

	private static int maxHandSize = 8;

	public List<Card> discardList;
	public List<Card> deckList;
	private Card[] deckArray;

	private static string stylizeText(string text)
	{
		char[] charText = text.ToCharArray();
		string textCopy = text;
		string returnText = "";

		for (int i = 0; i < text.Length; i++)
		{
			if (charText[i] == '/')
			{
				int j = i;
				while (i < charText.Length)
				{
					i++;
					if (charText[i] == '/')
					{
						returnText = textCopy.Substring(0, j) + "<b><i>" + textCopy.Substring(j + 1, i - j - 1) + "</b></i>" + textCopy.Substring(i + 1);
						textCopy = returnText;
						charText = textCopy.ToCharArray();
						//correct length to increment past </i>
						i += 12;
						break;
					}
				}
			}
		}

		if (returnText == "")
			return text;
		else
			return returnText;
	}

	// Start is called before the first frame update
	void Start()
    {
		playerHandList = new List<GameObject>();
		discardList = new List<Card>();
		deckList = XmlCardManager.ParseCardXml("Assets/xml/allCards.xml");
		for (int i = 0; i < 6; i++)
		{
			Card dupe = new Card(deckList.ElementAt(i));
			deckList.Add(dupe);
		}
		deckArray = deckList.ToArray();
		Shuffle();
		UpdateCount();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void UpdateCount()
	{
		GameObject discardCountTextObj = this.transform.Find("DiscardCountText").gameObject;
		GameObject deckCountTextObj = this.transform.Find("DeckCountText").gameObject;
		TextMeshProUGUI discardCount = discardCountTextObj.GetComponent<TextMeshProUGUI>();
		TextMeshProUGUI deckCount = deckCountTextObj.GetComponent<TextMeshProUGUI>();

		discardCount.text = "" + discardList.Count;
		deckCount.text = "" + deckList.Count;
	}

	public void Shuffle()
	{
		int i, j;

		for (i = deckList.Count - 1; i > 0; i --)
		{
			Card buf = new Card(deckList[i]);
			j = Random.Range(0, i);
			deckList[i] = deckList[j];
			deckList[j] = buf;
		}
	}

	public void ShuffleDiscard()
	{
		int i;

		for (i = discardList.Count - 1; i >= 0; i = discardList.Count - 1)
		{
			deckList.Add(new Card(discardList[i]));
			discardList.RemoveAt(i);
		}
		Shuffle();
	}

	public void DrawCard()
	{
		RectTransform playerAreaXform = playerArea.GetComponent<RectTransform>();

		if (playerHandList.Count >= maxHandSize)
		{
			return;
		}
		else if (deckList.Count <= 0)
		{
			ShuffleDiscard();
		}

		//loop through List and change positions
		GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		playerHandList.Add(newCard);
		newCard.transform.SetParent(playerArea.transform, false);
		SortHand();

		//draw card from deck;
		CreateCard(deckList[0], newCard);
		deckList.RemoveAt(0);

		UpdateCount();
	}

	private void CreateCard(Card cardData, GameObject cardPrefab)
	{
		GameObject cardNameObj = cardPrefab.transform.Find("Card Name Text").gameObject;
		GameObject cardEffectObj = cardPrefab.transform.Find("Card Effect Text").gameObject;
		GameObject cardCostObj = cardPrefab.transform.Find("Cost Text").gameObject;
		TextMeshProUGUI cardName = cardNameObj.GetComponent<TextMeshProUGUI>();
		cardName.text = cardData.cardName;
		TextMeshProUGUI cardEffect = cardEffectObj.GetComponent<TextMeshProUGUI>();
		cardEffect.text = stylizeText(cardData.cardText);
		TextMeshProUGUI cardCost = cardCostObj.GetComponent<TextMeshProUGUI>();
		cardCost.text = cardData.cost.ToString();

		CardResolveScript tempScript = cardPrefab.GetComponent("CardResolveScript") as CardResolveScript;
		if (tempScript == null)
		{
			Debug.Log("Can't find CardResolveScript.");
			return;
			//TODO: error condition
		}

		switch (cardData.id)
		{
			case "000":
				cardData.cardEffect = new CubeEffect();
				break;
			case "001":
				cardData.cardEffect = new PrismEffect();
				break;
			case "002":
				cardData.cardEffect = new SphereEffect();
				break;
			default:
				Debug.Log("Couldn't find card effect class of Id " + cardData.id);
				break;
		}

		tempScript.cardData = new Card(cardData);
	}

	public void RemoveCard(GameObject cardToRemove)
	{
		//find card
		CardResolveScript tempScript = cardToRemove.GetComponent("CardResolveScript") as CardResolveScript;
		if (tempScript == null)
		{
			Debug.Log("Can't find CardResolveScript.");
			return;
			//TODO: error condition
		}
		discardList.Add(new Card(tempScript.cardData));
		playerHandList.Remove(cardToRemove);
		SortHand();
		UpdateCount();
	}

	private void SortHand()
	{
		GameObject currCard = null;
		int i;

		for (i = 0; i < playerHandList.Count; i++)
		{
			currCard = playerHandList[i];
			if (currCard == null || currCard.transform == null)
			{
				Debug.Log("Card in the hand is null.");
				continue;
			}

			float mod = 0 - (playerHandList.Count - 1) + (i * 2);
			Vector3 newPos = new Vector3(60 * mod, 30 - System.Math.Abs(mod * 15), 0);
			Quaternion newRotation = Quaternion.Euler(0, 0, mod * -3);
			currCard.transform.localPosition = newPos;
			currCard.transform.rotation = newRotation;
		}
	}
}
