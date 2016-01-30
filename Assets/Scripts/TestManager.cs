using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour
{
	public GameObject sentence;
	public GameObject noun;

	public RectTransform topBar;
	public RectTransform bottomBar;

	string[,] tests = new string[3, 4]
	{
		{"Tom brushes his", "teeth", ".", "牙齿"},
		{"Tom eats a big", "breakfast", ".", "早餐"},
		{"Tom takes a", "shower", ".", "洗澡"},
	};

	void Start()
	{
		CreateSentence(0);

		for (int i = 0; i < tests.GetLength(0); ++i)
		{
			CreateNoun(i);
		}
	}

	void CreateSentence(int i)
	{
		// create a sentence
		GameObject st = Instantiate(sentence, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		st.GetComponent<Sentence>().Setup(tests[i, 0], tests[i, 1], tests[i, 2]);
		st.transform.SetParent(topBar);
		st.transform.localPosition = new Vector3(0f, -50f);
	}

	void CreateNoun(int i)
	{
		// and a noun button
		// TODO add this button to a list that is scrambled up
		GameObject nn = Instantiate(noun, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		nn.GetComponentInChildren<Noun>().Setup(tests[i, 3], tests[i, 1], bottomBar);
	}
}
