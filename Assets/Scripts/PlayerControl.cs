using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public Sprite clothed;
	public AnimatorOverrideController clothedAOC;

	private float speed = 1.0f;
	private Transform target = null;
	private SpriteRenderer targetSprite = null;
	private Vector3 targetPos = Vector3.zero;
	private Animator anim;
	private SpriteRenderer sprite;
	private GameObject bubble;

	private Transform playerHand;
	private string targetNoun, targetVerb;

	private bool removeTarget = false;

	void Awake()
	{
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		playerHand = transform.GetChild(0);
		bubble = GameObject.FindWithTag("bubble");
	}

	public void doThing(string noun, string verb)
	{

		Transform item = target.parent;
		targetSprite.sortingLayerName = "Front";

		if (verb == "shower" && noun != "shower")
			verb = "take";

		// verbs that move something to the hand and then animate
		if (verb == "eat" || verb == "wear" || verb == "take")
		{
			// move noun to player's hand
			item.transform.SetParent(transform.GetChild(0).transform);
			item.transform.localPosition = new Vector3(0, 0, 0);

			// start animation
			anim.SetTrigger(verb);

			// make sure that the target gets removed
			if (verb == "take" || (verb == "wear" && noun != "clothes"))
				removeTarget = false;
			else
				removeTarget = true;

			if (verb == "wear" && noun == "clothes")
				getDressed();
		}
		// verbs that play an animation
		else //if (verb == "shower" || verb == "pee")
		{
			// start animation
			anim.SetTrigger(verb);

			if (verb=="putOn" && noun == "shoes") {
				item.transform.SetParent(transform.GetChild(0).transform);
				removeTarget = true;
			}
		}
	}

	void Update()
	{
		if (target)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

			if (targetPos.x == transform.position.x)
			{
				sprite.flipX = Mathf.Sign(target.localScale.x) < 0 ? true : false;

				anim.SetBool("walking", false);
				doThing(targetNoun, targetVerb);
				target = null;
			}
			else
			{
				sprite.flipX = targetPos.x < transform.position.x;
			}
		}
	}

	public void setTarget(string noun, string verb)
	{
		// remove thinking bubble
		bubble.SetActive(false);

		target = GameObject.FindWithTag(noun).transform.GetChild(0);
		targetSprite = target.parent.GetComponentInChildren<SpriteRenderer>();
		targetPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
		targetNoun = noun;
		targetVerb = verb;

		anim.SetBool("walking", true);
	}

	void getDressed()
	{
		Debug.Log("dressed");
		sprite.sprite = clothed;
		anim.runtimeAnimatorController = clothedAOC;
	}

	public void onHandsAnimationExit()
	{
		if (removeTarget && playerHand.childCount > 0)
		{
			Destroy (playerHand.GetChild(0).gameObject);
			removeTarget = false;
		}
	}

	public void thinking (string itemToThink, Vector3 bubbleVector)
	{
		if (targetSprite)
			targetSprite.sortingLayerName = "Default";

		bubble.SetActive(true);

		if (bubble.transform.childCount > 0)
			Destroy (bubble.transform.GetChild(0).gameObject);

		SpriteRenderer targetRenderer = GameObject.FindWithTag(itemToThink).GetComponentInChildren<SpriteRenderer> ();
		GameObject clone = Instantiate(targetRenderer.gameObject);
		Transform target = clone.transform;
		targetRenderer = clone.GetComponent<SpriteRenderer>();
		targetRenderer.sortingLayerName = "Front";
		target = targetRenderer.transform;

		clone.transform.SetParent(bubble.transform);

		target.localPosition = new Vector3 (-0.01f, 0, 0);

		//New ration calculation
		Sprite targetSp = targetRenderer.sprite;
		Sprite bubbleSprite = bubble.GetComponent<SpriteRenderer>().sprite;

		float ratiox = (bubbleSprite.bounds.size.x / targetSp.bounds.size.x) * 0.3f;
		float ratioy = (bubbleSprite.bounds.size.y / targetSp.bounds.size.y) * 0.3f;
		float correctRatio = Mathf.Min (ratiox, ratioy);
		target.transform.localScale *= correctRatio;
	}
}
