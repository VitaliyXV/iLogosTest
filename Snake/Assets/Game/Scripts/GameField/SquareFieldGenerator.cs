using UnityEngine;
using System.Collections.Generic;

public class SquareFieldGenerator : MonoBehaviour, IFieldGenerator<Cell>
{
	public GameObject TileObject;

	private Cell[,] field;

	private int width;
	private int height;
	private int visibleRadius;

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
		Width = Height = 10;

		Generate();
	}

	public void Generate()
	{
		field = new Cell[Height, Width];

		int offsetX = -(Width / 2);
		int offsetY = -(int)(Height / 2.5);

		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				var type = (CellType)Random.Range(0, System.Enum.GetValues(typeof(CellType)).Length);

				var tile = Instantiate(TileObject, new Vector3(offsetX++, offsetY, 0), Quaternion.identity) as GameObject;
				tile.transform.SetParent(transform);
				var cell = tile.GetComponent<Cell>();
				cell.Type = type;
				cell.SetColor();

				field[y, x] = cell;
			}
			offsetY++;
			offsetX = -(Width / 2);
		}
	}
	
	public void Clear()
	{
		field = null;
	}
}
