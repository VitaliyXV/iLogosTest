using System;

[Serializable]
public class Player
{
	public string FacebookID { get; set; }
	public string Name { get; set; }

	public Player()
	{
		FacebookID = "";
		Name = "Anonymous";
	}
}
