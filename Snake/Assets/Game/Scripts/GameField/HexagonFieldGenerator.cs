using UnityEngine;
using System.Collections;

public class HexagonFieldGenerator : BaseField
{
	//Hexagon tile width and height in game world
	private float hexWidth;
	private float hexHeight;
		
	public override void Generate()
	{
		field = new GameObject[Height + 2, Width + 2];

		int offsetX = -(Width / 2);
		int offsetY = -(int)(Height / 2.5);

		setSizes();

		//Game object which is the parent of all the hex tiles
		//GameObject hexGridGO = new GameObject("HexGrid");
		
		for (int y = 0; y < Height + 2; y++)
		{
			for (int x = 0; x < Width + 2; x++)
			{
				//GameObject assigned to Hex public variable is cloned
				GameObject hex = (GameObject)Instantiate(TileSurfaceObject);
				//Current position in grid
				Vector2 gridPos = new Vector2(x, y);
				hex.transform.position = calcWorldCoord(gridPos);
				hex.transform.SetParent(parent.transform);

				var type = (CellType)Random.Range(0, System.Enum.GetValues(typeof(CellType)).Length);

				var cell = hex.GetComponent<Cell>();
				cell.Type = type;
				//cell.SetColor();
				
				field[y, x] = hex;

				if (y == 0 || x == 0 || y == Height + 1 || x == Width + 1)
				{
					CreateWall(field[y, x]);
				}
			}
		}
		
		SetRandomWalls();
		SetFood(null);
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
		float offsetY = -(Height / 2.2f);

		//Position of the first hex tile
		Vector3 initPos = calcInitPos();

		//Every second row is offset by half of the tile width
		float offset = 0;

		if (gridPos.y % 2 != 0)	offset = hexWidth / 2;

		float x = initPos.x + offset + gridPos.x * hexWidth;

		//Every new line is offset in z direction by 3/4 of the hexagon height
		float z = initPos.z - gridPos.y * hexHeight * 0.75f - offsetY;

		return new Vector3(x, z, 0);
	}

	public override GameObject NextTile(Direction direction, ref int y, ref int x, ref Quaternion rotation)
	{
		GameObject tile = null;

		switch (direction)
		{
			case Direction.Up: x -= y % 2 == 0 ? 1 : 0; tile = field[--y, x]; break;
			case Direction.Down: x += y % 2; tile = field[++y, x]; break;
			case Direction.Right: tile = field[y, ++x]; break;
			case Direction.Left: tile = field[y, --x]; break;
		}

		rotation = SetRotation(direction);

		return tile;
	}

	private Quaternion SetRotation(Direction direction)
	{
		switch (direction)
		{
			case Direction.Up: return Quaternion.Euler(0, 0, 33);
			case Direction.Down: return Quaternion.Euler(0, 0, 213);
			case Direction.Right: return Quaternion.Euler(0, 0, -90);
			case Direction.Left: return Quaternion.Euler(0, 0, 90);
			default: return Quaternion.Euler(0, 0, 0);
		}
	}
}
