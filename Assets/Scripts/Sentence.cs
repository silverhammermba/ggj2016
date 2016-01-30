using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sentence : MonoBehaviour
{
	string before;
	string correct;
	string after;

	public Text beforeText;
	public Text afterText;
	public RectTransform blank;

	public float blankPad = 10f;
	public float dropThreshold = 50f;

	public void Setup(string bef, string cor, string aft)
	{
		before = bef;
		correct = cor;
		after = aft;
	}

	void Start()
	{
		beforeText.text = before;
		afterText.text = after;

		blank.localPosition = new Vector3(blankPad + blank.rect.width / 2f, blank.localPosition.y, 0f);
		afterText.transform.localPosition = new Vector3(2 * blankPad + blank.rect.width, 0f, 0f);
	}

	public void FillIn(GameObject word)
	{
		float dist = Vector3.Distance(word.transform.position, blank.position);
		if (dist < dropThreshold)
		{
			blank.gameObject.SetActive(false);
			afterText.text = " " + word.GetComponent<Noun>().english + ".";
			afterText.transform.localPosition = new Vector3(0f, 0f, 0f);
			GameObject.Destroy(word.transform.parent.gameObject);
		}
		else
		{
			Debug.Log("too far");
		}
	}
}
