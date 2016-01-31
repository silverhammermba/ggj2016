using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	private float speed = 1.0f;
	private Transform target = null;
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
			removeTarget = true;

		}
		// verbs that play an animation
		else if (verb == "shower" || verb == "pee")
		{
			// start animation
			anim.SetTrigger(verb);
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
		//remove thinking bubble
		bubble.SetActive(false);

		target = GameObject.FindWithTag(noun).transform.GetChild(0);
		targetPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
		targetNoun = noun;
		targetVerb = verb;

		//TODO trigger walk
		anim.SetBool("walking", true);
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
		bubble.SetActive(true);

		if (bubble.transform.childCount > 0)
			Destroy (bubble.transform.GetChild(0).gameObject);

		GameObject item = GameObject.FindWithTag(itemToThink);
		GameObject clone = Instantiate(item);

		Transform target = clone.transform;
		if (target.childCount>0)
		{
			target = target.GetChild(0);
		}

		clone.transform.SetParent(bubble.transform);

		target.position = bubble.transform.position + new Vector3(0.08f, 0.12f, 0f);
	}
}
