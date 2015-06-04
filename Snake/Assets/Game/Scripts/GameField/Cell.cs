using UnityEngine;

public class Cell : MonoBehaviour 
{
	public CellType Type;
	public TileObject ObjectOnTileType;
	public GameObject ObjectOnTile;
		
	void Awake()
	{
		
	}

	public void SetColor()
	{
		Color color = Color.gray;

		switch (Type)
		{
			case CellType.Grass: color = Color.green; break;
			case CellType.Desert: color = Color.yellow; break;
			case CellType.Ice: color = Color.white; break;
		}

		Renderer rend = GetComponent<Renderer>();
		rend.material.shader = Shader.Find("Specular");
		rend.material.SetColor("_Color", color);
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{
	
	}
}
