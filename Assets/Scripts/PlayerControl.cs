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
		//Move away the thinking
		PlayerControl pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		pc.thinking (new Vector3 (100f, 100f, 100f));


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

	public void thinking (Vector3 vector){
		GameObject obj = GameObject.FindWithTag ("bubble");
		obj.transform.localPosition = GameObject.FindWithTag ("Player").transform.position + new Vector3 (0.4f,0.5f,0);
	}


}
