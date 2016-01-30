using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sentence : MonoBehaviour
{
	public float dropThreshold = 50f;

	public Color correctColor = Color.green;
	public Color wrongColor = Color.red;

	public string blanks = "_____";

	Text text;

	string sentence;
	string correct;
	int blankIndex;

	public void Setup(string sent, string cor)
	{
		sentence = sent;
		correct = cor;

		if ((blankIndex = sentence.IndexOf("_")) < 0)
			Debug.Log("Can't find blank in sentence: " + sentence);
		else
		{
			SetBlank(blanks);
		}
	}

	void Awake()
	{
		text = GetComponent<Text>();
	}

	void SetBlank(string str)
	{
		text.text = sentence.Substring(0, blankIndex) + str + sentence.Substring(blankIndex + 1, sentence.Length - blankIndex - 1);
	}

	public void FillIn(GameObject word)
	{
		float dist = Vector3.Distance(word.transform.position, transform.position);

		if (dist < dropThreshold)
		{
			string eng = word.GetComponent<Noun>().english;
			string colhex = ColorUtility.ToHtmlStringRGB(eng == correct ? correctColor : wrongColor);
			SetBlank("<color=#" + colhex + ">" +  eng + "</color>");
		}
	}
}
