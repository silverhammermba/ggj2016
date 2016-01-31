using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sentence : MonoBehaviour
{
	private float dropThreshold = 100f;

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
//		Debug.Log ("dist: " + dist);
		string eng = word.GetComponent<Noun>().english;

		if (dist < dropThreshold)
		{
			bool isCorrect = eng == correct;
			Debug.Log ("correct: " + isCorrect);
			string colhex = ColorUtility.ToHtmlStringRGB(isCorrect ? correctColor : wrongColor);
			SetBlank("<color=#" + colhex + ">" +  eng + "</color>");

			if (isCorrect) {
				//load next sentence after 5 seconds
				Invoke("cleanupAndLoadNext",5);
			}
		}

		//animation
		GameObject player = GameObject.FindWithTag ("Player");
//		Debug.Log (player);
		PlayerControl pc = player.GetComponent<PlayerControl> ();
		pc.beginAnimation(GameObject.FindWithTag(eng), "eat");
	}
		
	void cleanupAndLoadNext(){
		GameObject.Find("TestManager").GetComponent<TestManager>().NextSentence();
	}
}
