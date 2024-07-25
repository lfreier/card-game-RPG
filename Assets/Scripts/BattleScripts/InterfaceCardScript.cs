using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* @author lfreier
 * CardActionScript.cs
 * 
 * Deals with all interactable parts of a card.
 * It's a long list of actions, but for mainly simple and short actions.
 * 
 * e.g.
 *	Hover card in player hand
 *	Hover card in play
 *	Pick up card from hand
 *	Cancel card pickup
 *	Play card from hand
 *	Get more info on a card?
 */

public class InterfaceCardScript : MonoBehaviour
{
	private bool isDragging = false;
	private bool isHover = false;
	private bool cardHasTarget = false;

	private GameObject target;
	private GameObject mainCanvas;
	private CardResolveScript targetScript;

	private Vector3 startPosition;
	private Quaternion startRotation;
	private int startSiblingIndex;

	// Start is called before the first frame update
	void Start()
    {
	}

	void Awake()
	{
		mainCanvas = GameObject.Find("UI Canvas");
	}

	// Update is called once per frame
	void Update()
	{
		if (isDragging)
		{
			//Vector3 mousePoint = mainCamera.ScreenToViewportPoint(Input.mousePosition);
			Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
			transform.position = mousePoint;

			if (Input.GetMouseButtonDown(1) ||
				Input.GetMouseButtonDown(2) ||
				Input.GetKeyDown(KeyCode.Escape))
			{
				cardHasTarget = false;
				EndDrag();
			}
		}
	}

	public void OnHover()
	{
		//get a better view of the card
		//Debug.Log("Mouse entered card space.");
		if (isDragging)
			return;

		isHover = true;
		startPosition = transform.position;
		startRotation = transform.rotation;
		startSiblingIndex = transform.GetSiblingIndex();

		transform.rotation = Quaternion.Euler(0, 0, 0);
		transform.SetAsLastSibling();
		transform.localScale = new Vector3(1.5F, 1.5F);

		//if in hand:
		if (gameObject.transform.parent.name.Equals("PlayerArea"))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 30);
		}

		//if in play:
	}

	public void OnHoverExit()
	{
		if (isDragging)
			return;

		//if in play:
		isHover = false;
		transform.position = startPosition;
		transform.rotation = startRotation;
		transform.localScale = new Vector3(1, 1);
		transform.SetSiblingIndex(startSiblingIndex);

		//if in hand:
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.otherCollider != null && collision.gameObject.name.Equals("PlayArea"))
		{
			target = collision.gameObject;
			cardHasTarget = true;
			CardResolveScript tempScript = GetComponent("CardResolveScript") as CardResolveScript;
			if (tempScript == null)
			{
				Debug.Log("Can't find CardResolveScript in InterfaceCardScript");
				return;
			}
			targetScript = tempScript;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		cardHasTarget = false;
		target = null;
	}

	public void StartDrag()
	{
		if (isHover)
			OnHoverExit();

		//attach card to mouse
		isDragging = true;
		startPosition = transform.position;
		startRotation = transform.rotation;
		startSiblingIndex = transform.GetSiblingIndex();
		transform.rotation = Quaternion.Euler(0, 0, 0);
		transform.SetAsLastSibling();
	}

	public void EndDrag()
	{
		isDragging = false;

		//play card
		if (cardHasTarget)
		{
			//resolve card logic
			targetScript.resolveCardPlay(gameObject);
		}
		//return to hand
		else
		{
			transform.position = startPosition;
			transform.rotation = startRotation;
			transform.SetSiblingIndex(startSiblingIndex);
			cardHasTarget = false;
			target = null;
			targetScript = null;
		}
	}
}
