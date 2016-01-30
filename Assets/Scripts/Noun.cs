using UnityEngine;
using System.Collections;

public class Noun : MonoBehaviour
{
	bool dragging = false;

	void Update ()
	{
		if (dragging)
		{
			transform.position = Input.mousePosition;
		}
		else
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0f, 0f, 0f), 0.25f);
		}
	}

	public void StartDrag()
	{
		dragging = true;
	}

	public void StopDrag()
	{
		//Debug.Log(transform.localPosition.sqrMagnitude);
		dragging = false;
		if (transform.localPosition.sqrMagnitude < 1000f)
		{
			Debug.Log("noun!");
		}
		else
		{
			Sentence sent = GameObject.FindWithTag("sentence").GetComponent<Sentence>();
			sent.FillIn(gameObject);
		}
	}
}
