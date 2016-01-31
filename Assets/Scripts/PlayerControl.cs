using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	Animator anim;

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
		}
		// verbs that play an animation
		else if (verb == "shower")
		{
			// start animation
			anim.SetTrigger(verb);
		}
	}

	public void thinking (Vector3 vector){
		GameObject obj = GameObject.FindWithTag ("bubble");
		obj.transform.localPosition = GameObject.FindWithTag ("Player").transform.position + new Vector3 (0.4f,0.5f,0);
	}





}
