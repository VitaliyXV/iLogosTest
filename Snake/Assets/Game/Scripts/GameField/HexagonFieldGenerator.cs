using UnityEngine;
using System.Collections;

public class HexagonFieldGenerator : MonoBehaviour, IFieldGenerator<Cell>
{
	public GameObject TileObject;

	private Cell[,] field;

	private int width;
	private int height;
	private int visibleRadius;

	//Hexagon tile width and height in game world
	private float hexWidth;
	private float hexHeight;

	public int Width
	{
		get
		{
			return width;
		}
		set
		{
			width = value;
		}
	}

	public int Height
	{
		get
		{
			return height;
		}
		set
		{
			height = value;
		}
	}

	public int VisibleRadius
	{
		get
		{
			return visibleRadius;
		}
		set
		{
			visibleRadius = value;
		}
	}

	public Cell this[int x, int y]
	{
		get
		{
			return field[x, y];
		}
		set
		{
			field[x, y] = value;
		}
	}

	void Awake()
	{
		Width = Height = 5;
		setSizes();
		Generate();
	}

	public void Generate()
	{
		field = new Cell[Height, Width];

		//Game object which is the parent of all the hex tiles
		//GameObject hexGridGO = new GameObject("HexGrid");

		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				//GameObject assigned to Hex public variable is cloned
				GameObject hex = (GameObject)Instantiate(TileObject);
				//Current position in grid
				Vector2 gridPos = new Vector2(x, y);
				hex.transform.position = calcWorldCoord(gridPos);
				hex.transform.SetParent(transform);

				var type = (CellType)Random.Range(0, System.Enum.GetValues(typeof(CellType)).Length);

				var cell = hex.GetComponent<Cell>();
				cell.Type = type;
				//cell.SetColor();

				field[y, x] = cell;
			}
		}
	}

	public void Clear()
	{
		field = null;
	}

	//Method to initialise Hexagon width and height
	void setSizes()
	{
		//renderer component attached to the Hex prefab is used to get the current width and height
		hexWidth = 1; //TileObject.renderer.bounds.size.x;
		hexHeight = 1; //TileObject.renderer.bounds.size.z;
	}

	//Method to calculate the position of the first hexagon tile
	//The center of the hex grid is (0,0,0)
	Vector3 calcInitPos()
	{
		Vector3 initPos;
		//the initial position will be in the left upper corner
		initPos = new Vector3(-hexWidth * Width / 2f + hexWidth / 2, Height / 2f * hexHeight - hexHeight / 2, 0);

		return initPos;
	}

	//method used to convert hex grid coordinates to game world coordinates
	public Vector3 calcWorldCoord(Vector2 gridPos)
	{
		//Position of the first hex tile
		Vector3 initPos = calcInitPos();
		//Every second row is offset by half of the tile width
		float offset = 0;
		if (gridPos.y % 2 != 0)
			offset = hexWidth / 2;

		float x = initPos.x + offset + gridPos.x * hexWidth;
		//Every new line is offset in z direction by 3/4 of the hexagon height
		float z = initPos.z - gridPos.y * hexHeight * 0.75f;
		return new Vector3(x, z, 0);
	}
}
