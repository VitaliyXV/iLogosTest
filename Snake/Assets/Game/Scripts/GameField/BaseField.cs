using UnityEngine;
using System.Collections;

public abstract class BaseField : MonoBehaviour, IFieldGenerator<GameObject>
{
	public GameObject TileSurfaceObject { get; private set; }
	public GameObject Wall { get; private set; }
	public GameObject Food { get; private set; }

	protected GameObject parent;

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

	public GameObject PlayerStartPosition
	{
		get { return field[PlayerStartPositionY, PlayerStartPositionX]; }
		set { }
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

	public BaseField()
	{
		parent = GameObject.Find("GameContainer");

		LocalDataProvider.Instance.GetPrefab("Rabbit", food => Food = food);

		switch (GameData.CurrentFieldType)
		{
			case FieldType.Square:
				LocalDataProvider.Instance.GetPrefab("SquareWall", wall => Wall = wall);
				LocalDataProvider.Instance.GetPrefab("SquareTile", tile => TileSurfaceObject = tile);
				break;
			case FieldType.Hexahonal:
				LocalDataProvider.Instance.GetPrefab("HexagonWall", wall => Wall = wall);
				LocalDataProvider.Instance.GetPrefab("HexTile", tile => TileSurfaceObject = tile);
				break;
		}
		
		Width = GameData.FieldWeight;
		Height = GameData.FieldHeight;

		WallCount = Width * Height / 20;

		PlayerStartPositionX = Width / 2 + 1;
		PlayerStartPositionY = Height / 2 + 1;
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

			if (cell.ObjectOnTileType != TileObject.Empty)
			{
				i--;
				continue;
			}

			CreateWall(tile);
		}
	}

	public GameObject SetFood(GameObject existingFood)
	{
		GameObject tile;
		Cell cell;
		
		while (true)
		{
			int x = Random.Range(1, Width);
			int y = Random.Range(1, Height);

			if (x == PlayerStartPositionX && y == PlayerStartPositionY) continue;

			tile = field[y, x];
			cell = tile.GetComponent<Cell>();

			if (cell.ObjectOnTileType == TileObject.Empty) break;			
		}		

		if (existingFood == null)
		{
			existingFood = Instantiate(Food, tile.transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
			existingFood.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 1);
		}
		else
		{
			existingFood.transform.position = tile.transform.position;
		}

		existingFood.transform.SetParent(tile.transform);

		cell.ObjectOnTileType = TileObject.Food;
		cell.ObjectOnTile = existingFood;

		return existingFood;
	}

	protected GameObject CreateWall(GameObject tile)
	{
		var wall = Instantiate(Wall, tile.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
		wall.transform.SetParent(tile.transform);
		tile.GetComponent<Cell>().ObjectOnTileType = TileObject.Wall;

		return wall;
	}

	public void Clear()
	{
		field = null;
	}

	public abstract GameObject NextTile(Direction direction, ref int y, ref int x, ref Quaternion rotation);
}
