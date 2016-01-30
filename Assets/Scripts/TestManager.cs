using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour
{
	public GameObject sentence;
	public GameObject noun;

	public RectTransform topBar;
	public RectTransform bottomBar;

	int sentenceIndex = 0;
	Sentence currentSentence = null;

	// hardcoded challenges
	string[,] tests = new string[7, 3]
	{
		{"Tom takes a _.", "shower", "洗澡"},
		{"Tom pees in the _.", "toilet", "卫生间"},
		{"Tom opens the _.", "refrigerator", "冰箱"},
		{"Tom eats a big _.", "breakfast", "早餐"},
		{"Tom brushes his _.", "teeth", "牙齿"},
		{"Tom wears his _.", "clothes", "衣服"},
		{"Tom puts on his _.", "shoes", "鞋子"},
	};

	void Start()
	{
		NextSentence();

		int num = tests.GetLength(0);

		// decide a random order to add nouns
		int[] indices = new int[num];
		for (int i = 0; i < num; ++i) indices[i] = i;
		for (int i = 0; i < num - 1; ++i)
		{
			int swp = Random.Range(i, num);
			int tmp = indices[swp];
			indices[swp] = indices[i];
			indices[i] = tmp;
		}

		// add nouns
		for (int i = 0; i < num; ++i)
		{
			CreateNoun(indices[i]);
		}
	}

	public void NextSentence()
	{
		if (currentSentence != null)
		{
			GameObject.Destroy(currentSentence.gameObject);
		}
		CreateSentence(sentenceIndex++);
	}

	// create a sentence from the hardcoded list
	void CreateSentence(int i)
	{
		GameObject st = Instantiate(sentence, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		currentSentence = st.GetComponent<Sentence>();
		currentSentence.Setup(tests[i, 0], tests[i, 1]);
		st.transform.SetParent(topBar);
		st.transform.localPosition = new Vector3(0f, -50f);
	}

	// create a noun button from the hardcoded list
	void CreateNoun(int i)
	{
		GameObject nn = Instantiate(noun, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		nn.GetComponentInChildren<Noun>().Setup(tests[i, 2], tests[i, 1], bottomBar);
	}
}
