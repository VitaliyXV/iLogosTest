using UnityEngine;
using System.Collections;

public abstract class BaseField : MonoBehaviour, IFieldGenerator<GameObject>
{
	public GameObject TileObject;
	public GameObject Wall;
	public GameObject Food;

	protected GameObject[,] field;

	private int width;
	private int height;
	private int visibleRadius;
	private int wallCount;
	private int playerStartPositionX;
	private int playerStartPositionY;

	public int Width
	{
		get	{ return width;	}
		set	{ width = value; }
	}

	public int Height
	{
		get	{ return height; }
		set	{ height = value; }
	}

	public int PlayerStartPositionX
	{
		get { return playerStartPositionX; }
		set { playerStartPositionX = value; }
	}

	public int PlayerStartPositionY
	{
		get { return playerStartPositionY; }
		set { playerStartPositionY = value; }
	}

	public int WallCount
	{
		get	{ return wallCount; }
		set { wallCount = value; }
	}

	public int VisibleRadius
	{
		get { return visibleRadius; }
		set { visibleRadius = value; }
	}

	public GameObject this[int y, int x]
	{
		get { return field[y, x]; }
		set { field[y, x] = value; }
	}

	void Awake()
	{
		WallCount = 10;
		Width = Height = 10;

		PlayerStartPositionX = Width / 2;
		PlayerStartPositionY = Height / 2;

		Generate();
		SetRandomWalls();
		SetFood();
	}

	public abstract void Generate();

	public void SetRandomWalls()
	{
		for (int i = 0; i < wallCount; i++)
		{
			int x = Random.Range(1, Width);
			int y = Random.Range(1, Height);

			if(x == PlayerStartPositionX && y == PlayerStartPositionY)
			{
				i--;
				continue;
			}

			var tile = field[y, x];

			var cell = tile.GetComponent<Cell>();

			if (cell.IsNotEmptyTail)
			{
				i--;
				continue;
			}

			CreateWall(tile);
			cell.IsNotEmptyTail = true;
		}
	}

	public void SetFood()
	{
		GameObject tile;

		while (true)
		{
			int x = Random.Range(1, Width);
			int y = Random.Range(1, Height);

			if (x == PlayerStartPositionX && y == PlayerStartPositionY) continue;

			tile = field[y, x];

			var cell = tile.GetComponent<Cell>();

			if (!cell.IsNotEmptyTail) break;
		}

		var food = Instantiate(Food, tile.transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
		food.transform.SetParent(tile.transform);

		food.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 1); //.angularVelocity = Random.insideUnitSphere * 5;
	}

	protected GameObject CreateWall(GameObject tile)
	{
		var wall = Instantiate(Wall, tile.transform.position, Quaternion.Euler(180, 0, 0)) as GameObject;
		wall.transform.SetParent(tile.transform);	

		return wall;
	}

	public void Clear()
	{
		field = null;
	}
}
