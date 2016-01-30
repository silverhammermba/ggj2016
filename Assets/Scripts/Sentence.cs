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
	string animTag;

	public void Setup(string sent, string cor, string animTag)
	{
		sentence = sent;
		correct = cor;
		this.animTag = animTag;


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

	public void FillIn(GameObject word )
	{
		float dist = Vector3.Distance(word.transform.position, transform.position);
		string eng = word.GetComponent<Noun>().english;

		if (dist < dropThreshold)
		{
			string colhex = ColorUtility.ToHtmlStringRGB(eng == correct ? correctColor : wrongColor);
			SetBlank("<color=#" + colhex + ">" +  eng + "</color>");
		}

		//animation
		GameObject player = GameObject.FindWithTag ("Player");
		Debug.Log (player);
		PlayerControl pc = player.GetComponent<PlayerControl> ();
		pc.beginAnimation(GameObject.FindWithTag(eng), "eat");
	}
}
