using UnityEngine;

/// <summary>
/// This class attached to tile object and contains additional data about tile
/// </summary>
public class Cell : MonoBehaviour 
{
	/// <summary>
	/// Type of cell (now, its just for color (only for square field))
	/// </summary>
	public CellType Type;

	/// <summary>
	/// Type of game object on current tile
	/// </summary>
	public TileObject ObjectOnTileType;

	/// <summary>
	/// Referense to Game Object on current tile
	/// </summary>
	public GameObject ObjectOnTile;
			
	/// <summary>
	/// Set color for current tile
	/// </summary>
	public void SetColor()
	{
		Color color = Color.gray;

		switch (Type)
		{
			case CellType.Grass: color = Color.green; break;
			case CellType.Desert: color = Color.yellow; break;
			case CellType.Ice: color = Color.white; break;
		}

		// don't work in builds :(

		//Renderer rend = GetComponent<Renderer>();
		//rend.material.shader = Shader.Find("Specular");
		//rend.material.SetColor("_Color", color);
	}
}
