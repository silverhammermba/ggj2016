using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestManager : MonoBehaviour
{

	public const string NativeLang = "en";
	public const string LearningLang = "zh"; //zh, ja

	public GameObject sentence;
	public GameObject noun;

	/// player
	public GameObject player;

	public RectTransform topBar;
	public RectTransform bottomBar;

	int sentenceIndex = 0;

	Sentence currentSentence = null;

	// hardcoded challenges
	string[,] tests;

	public TestManager(){
		if (LearningLang == "zh") {
			tests = new string[,] {
				//0:challenge		1:native word	2:foreign word	3:word key	4:animation	key
				{ "Tom pees in the _.", "toilet", "卫生间", "toilet", "pee" },
				{ "Tom takes a _.", "shower", "洗澡", "shower", "shower" },
				{ "Tom wears his _.", "clothes", "衣服", "clothes", "wear" },
				{ "Tom opens the _.", "refrigerator", "冰箱", "refrigerator", "open" },
				{ "Tom eats a big _.", "breakfast", "早餐", "breakfast", "eat" },
				{ "Tom reads the _.", "newspaper", "报纸", "newspaper", "read" },
				{ "Tom puts on his _.", "shoes", "鞋子", "shoes", "putOn" },
			};
		} else if (LearningLang == "ja") {
			tests = new string[,]
			{
				//0:challenge		1:native word	2:foreign word	3:word key	4:animation	key
				{"Tom pees in the _.", "toilet", "トイレ", "toilet", "pee"},
				{"Tom takes a _.", "shower", "シャワー", "shower", "shower"},
				{"Tom wears his _.", "clothes", "服", "clothes", "wear"},
				{"Tom opens the _.", "refrigerator", "冷蔵庫", "refrigerator", "open"},
				{"Tom eats a big _.", "breakfast", "朝ごはん", "breakfast", "eat"},
				{"Tom reads the _.", "newspaper", "新聞", "newspaper", "read"},
				{"Tom puts on his _.", "shoes", "靴", "shoes", "putOn"},
			};
		}
	}

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
		if (sentenceIndex == tests.Length) {
			//the player wins!
			Debug.Log("you win!");
		}
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
		currentSentence.Setup(tests[i, 0], tests[i, 3], tests[i, 4]);
		st.transform.SetParent(topBar);
		st.transform.localPosition = new Vector3(0f, -20f);


		//Starting the thinking bubble
		PlayerControl pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		Vector3 vector = (Vector3)(GameObject.FindWithTag ("Player").transform.position + new Vector3 (0.25f,0.3f, 0));
		pc.thinking(tests [i, 1], vector);
	}

	// create a noun button from the hardcoded list
	void CreateNoun(int i)
	{
		GameObject nn = Instantiate(noun, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		nn.GetComponentInChildren<Noun>().Setup(tests[i, 3], tests[i, 2], tests[i, 1], bottomBar);
	}
		 
}
