using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Sentence : MonoBehaviour
{
	private float dropThreshold = 100f;

	public Color correctColor = Color.green;
	public Color wrongColor = Color.red;

	public string blanks = "_____";

	Text text;

	string sentence;
	string correctKey;
	int blankIndex;
	string animTag;

	public void Setup(string sent, string correctKey, string atag)
	{
		sentence = sent;
		this.correctKey = correctKey;
		animTag = atag;


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
		// TODO make drag'n'drop work based on the bounding box, not the center
		float dist = Vector3.Distance(word.transform.position, transform.position);

		string native = word.GetComponent<Noun>().native;
		string key = word.GetComponent<Noun>().key;

		if (dist < dropThreshold)
		{
			bool isCorrect = key == correctKey;
			Debug.Log ("correct: " + isCorrect);
			string colhex = ColorUtility.ToHtmlStringRGB(isCorrect ? correctColor : wrongColor);
			SetBlank("<color=#" + colhex + ">" +  native + "</color>");

			if (isCorrect) {
				//load next sentence after 5 seconds
				Invoke ("cleanupAndLoadNext", 5);
			} else {
				//TODO show angry face

				Invoke ("restart", 5);
			}

			//animation
			PlayerControl pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
			//		pc.doThing(eng, animTag);
			Transform target = GameObject.FindWithTag (key).transform;
			if (target.childCount>0) {
				target = target.GetChild (0);
			}
			pc.setTarget (target.position, key, animTag);
		}


	}

	void cleanupAndLoadNext(){
		GameObject.Find("TestManager").GetComponent<TestManager>().NextSentence();
	}

	void restart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
