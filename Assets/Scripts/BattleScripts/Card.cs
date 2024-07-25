using UnityEngine;
using UnityEditor;

/* @author lfreier
 * 
 * Object for card data.
 * Cards are listed in a csv 'database', and this object will be created from the entries
 *   when loading the game.
 */
 
public class Card
{
	public static int MAX_TYPES = 5;

	public string cardName;
	public string cardText;
	public string flavorText;
	public string[] typesList;
	public short cost;
	public string id;

	public Sprite artwork;
	public GameObject cardPrefabObject;

	public Effect cardEffect;

	public Card() { }

	public Card(Card clone)
	{
		this.cardName = clone.cardName;
		this.cardText = clone.cardText;
		this.flavorText = clone.flavorText;
		this.typesList = clone.typesList;
		this.cost = clone.cost;
		this.id = clone.id;
		this.cardEffect = clone.cardEffect;
	}

	public void InitCard(string cardName)
	{
		this.cardName = cardName;
	}

	public void InitCard(string cardName, string cardText)
	{
		this.cardName = cardName;
		this.cardText = cardText;
	}
}