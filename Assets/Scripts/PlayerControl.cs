using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void beginAnimation (GameObject item, string animTag) {
		if (animTag == "eat") {
			Animator animator = GetComponent<Animator>();
			animator.SetTrigger("eatNow");
	
			item.transform.SetParent(transform.GetChild (0).transform);
			item.transform.localPosition = new Vector3 (0, 0, 0);


		}
	}



	void eating (GameObject food) {

	}






}
