using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sentence : MonoBehaviour
{
	public string before;
	public string after;

	public Text beforeText;
	public Text afterText;
	public RectTransform blank;

	public float blankPad = 10f;
	public float dropThreshold = 50f;

	void Start()
	{
		beforeText.text = before;
		afterText.text = after;

		blank.localPosition = new Vector3(blankPad + blank.rect.width / 2f, blank.localPosition.y, 0f);
		afterText.transform.localPosition = new Vector3(2 * blankPad + blank.rect.width, 0f, 0f);
	}

	public void FillIn(GameObject word)
	{
		float dist = Vector3.Distance(word.transform.position, transform.GetChild(1).position);
		if (dist < dropThreshold)
		{
			Debug.Log("close enough!");
		}
		else
		{
			Debug.Log("too far");
		}
	}
}
