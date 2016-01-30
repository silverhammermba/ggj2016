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

	public Color correctColor = Color.green;
	public Color wrongColor = Color.red;

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
			string eng = word.GetComponent<Noun>().english;
			string colhex = ColorUtility.ToHtmlStringRGB(eng == correct ? correctColor : wrongColor);
			afterText.text = " <color=#" + colhex + ">" +  eng + "</color>" + after;
			afterText.transform.localPosition = new Vector3(0f, 0f, 0f);
		}
	}
}
