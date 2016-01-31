using System;

using System.Collections;
using System.Collections.Generic;

public class Situation
{
	//a situation is a player level around a certain environment

	public string Name;

	//these are the words that the player can use for different actions
	//the key is the english word for convenience
	public Dictionary<string,Word> WordBank = new Dictionary<string,Word> ();

	//these are the actual phrases that the player has to answer
	public List<Challenge> Challenges = new List<Challenge>();

	public Situation ()
	{
	}
}

