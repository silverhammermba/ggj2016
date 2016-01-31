using UnityEngine;
using System.Collections;
using System;

public class LevelLoader {

	public static Situation loadup(){

		//ideally this would be loaded from a file or something (json)
		Situation s = new Situation ();
		s.Name = "Home/Morning";

		Word w;

		w = new Word ();
		w.key = "toilet";
		w.langs.Add ("en", "toilet");
		w.langs.Add ("zh", "卫生间");
		w.langs.Add ("ja", "トイレ");
		s.WordBank.Add (w.key, w);

		w = new Word ();
		w.key = "shower";
		w.langs.Add ("en", "shower");
		w.langs.Add ("zh", "洗澡");
		w.langs.Add ("ja", "シャワー");
		s.WordBank.Add (w.key, w);

		w = new Word ();
		w.key = "clothes";
		w.langs.Add ("en", "clothes");
		w.langs.Add ("zh", "衣服");
		w.langs.Add ("ja", "服");
		s.WordBank.Add (w.key, w);

		w = new Word ();
		w.key = "refrigerator";
		w.langs.Add ("en", "refrigerator");
		w.langs.Add ("zh", "冰箱");
		w.langs.Add ("ja", "冷蔵庫");
		s.WordBank.Add (w.key, w);

		w = new Word ();
		w.key = "breakfast";
		w.langs.Add ("en", "breakfast");
		w.langs.Add ("zh", "早餐");
		w.langs.Add ("ja", "朝ごはん");
		s.WordBank.Add (w.key, w);

		w = new Word ();
		w.key = "newspaper";
		w.langs.Add ("en", "newspaper");
		w.langs.Add ("zh", "报纸");
		w.langs.Add ("ja", "新聞");
		s.WordBank.Add (w.key, w);

		w = new Word ();
		w.key = "shoes";
		w.langs.Add ("en", "shoes");
		w.langs.Add ("zh", "鞋子");
		w.langs.Add ("ja", "靴");
		s.WordBank.Add (w.key, w);



		Challenge c;

		c = new Challenge ();
		c.Phrases.Add("en", "Tom pees in the _.");
		c.Answer = s.WordBank["toilet"];
		c.Animation = "pee";
		s.Challenges.Add (c);

		c = new Challenge ();
		c.Phrases.Add("en", "Tom takes a _.");
		c.Answer = s.WordBank["shower"];
		c.Animation = "shower";
		s.Challenges.Add (c);

		c = new Challenge ();
		c.Phrases.Add("en", "Tom wears his _.");
		c.Answer = s.WordBank["clothes"];
		c.Animation = "wear";
		s.Challenges.Add (c);

		c = new Challenge ();
		c.Phrases.Add("en", "Tom opens the _.");
		c.Answer = s.WordBank["refrigerator"];
		c.Animation = "open";
		s.Challenges.Add (c);

		c = new Challenge ();
		c.Phrases.Add("en", "Tom eats a big _.");
		c.Answer = s.WordBank["breakfast"];
		c.Animation = "eat";
		s.Challenges.Add (c);

		c = new Challenge ();
		c.Phrases.Add("en", "Tom reads the _.");
		c.Answer = s.WordBank["newspaper"];
		c.Animation = "read";
		s.Challenges.Add (c);

		c = new Challenge ();
		c.Phrases.Add("en", "Tom puts on his _.");
		c.Answer = s.WordBank["shoes"];
		c.Animation = "putOn";
		s.Challenges.Add (c);

		return s;

	}

}
