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
}
