using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void doThing(GameObject noun, string verb)
	{
		// verbs that just move something to the hand
		if (verb == "eat" || verb == "wear")
		{
			// move noun to player's hand
			noun.transform.SetParent(transform.GetChild (0).transform);
			noun.transform.localPosition = new Vector3 (0, 0, 0);

			// start animation
			anim.SetTrigger(verb);
		}
	}
}
