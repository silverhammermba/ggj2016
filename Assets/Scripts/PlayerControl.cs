using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	private float speed = 1.0f;
	private Vector3 target = Vector3.zero;
	Animator anim;

	private Transform playerHand;
	private string targetNoun, targetVerb;

	private bool removeTarget = false;

	void Start()
	{
		playerHand = transform.GetChild (0);

	}

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void doThing(string noun, string verb)
	{
		GameObject item = GameObject.FindWithTag(noun);
		// TODO walk over to the thing

		if (verb == "shower" && noun != "shower")
			verb = "take";

		// verbs that move something to the hand and then animate
		if (verb == "eat" || verb == "wear" || verb == "take")
		{
			// move noun to player's hand
			item.transform.SetParent(transform.GetChild (0).transform);
			item.transform.localPosition = new Vector3 (0, 0, 0);

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

	void Update(){
		if (target != Vector3.zero) {
			transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

			if (target.x == transform.position.x) {
				target = Vector3.zero;
				anim.SetBool("walking", false);
				doThing(targetNoun, targetVerb);
			}
		}
	}

	public void setTarget(Vector3 dest, string noun, string verb){
		//remove thinking bubble
		GameObject bubble = GameObject.FindWithTag("bubble");
		bubble.transform.position = new Vector3 (100f, 100f, 0);
		//
		target = new Vector3 (dest.x, transform.position.y, transform.position.z);
		targetNoun = noun;
		targetVerb = verb;

		//TODO trigger walk
		anim.SetBool("walking", true);
	}

	public void onHandsAnimationExit(){
		if (playerHand.childCount > 0 && removeTarget) {
			Destroy (playerHand.GetChild(0).gameObject);
			removeTarget = false;
		}
	}

	public void thinking (string itemToThink, Vector3 bubbleVector){
		GameObject bubble = GameObject.FindWithTag ("bubble");
		bubble.transform.position = bubbleVector;

		if (bubble.transform.childCount > 0)
			Destroy (bubble.transform.GetChild (0).gameObject);
		GameObject item = GameObject.FindWithTag (itemToThink);
		GameObject clone = Instantiate (item);

		Transform target = clone.transform;
		if (target.childCount>0) {
			target = target.GetChild (0);
		}

		clone.transform.SetParent (bubble.transform);
		target.position = bubble.transform.position + new Vector3 (0.08f, 0.12f, 0f);
		float ratio = bubble.transform.localScale.x / target.transform.localScale.x;


		target.transform.localScale *= ratio;

	}
}
