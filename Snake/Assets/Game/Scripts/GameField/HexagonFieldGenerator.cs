using UnityEngine;
using System.Collections;

public class HexagonFieldGenerator : BaseField
{
	// hexagon tile width and height
	private float hexWidth;
	private float hexHeight;
		
	public override void Generate()
	{
		// TODO: optimize generator with object pool

		// create field matrix (include borders)
		field = new GameObject[Height + 2, Width + 2];
		
		// calculate center position
		int offsetX = -(Width / 2);
		int offsetY = -(int)(Height / 2.5);

		SetSizes();

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
				hex.transform.position = CalcWorldCoord(gridPos);
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

	/// <summary>
	/// Set hexagon width and height
	/// </summary>
	private void SetSizes()
	{
		hexWidth = 1;
		hexHeight = 1;
	}

	/// <summary>
	/// Calculate the position of the first hexagon tile (center of the hex grid is (0,0,0))
	/// </summary>
	/// <returns></returns>
	private Vector3 CalcInitPos()
	{
		Vector3 initPos;

		// initial position will be in the left upper corner
		initPos = new Vector3(-hexWidth * Width / 2f + hexWidth / 2, Height / 2f * hexHeight - hexHeight / 2, 0);

		return initPos;
	}

	/// <summary>
	/// Convert hex grid coordinates to game world coordinates
	/// </summary>
	private Vector3 CalcWorldCoord(Vector2 gridPos)
	{
		float offsetY = -(Height / 2.2f);

		// position of the first hex tile
		Vector3 initPos = CalcInitPos();

		// every second row is offset by half of the tile width
		float offset = 0;

		if (gridPos.y % 2 != 0)	offset = hexWidth / 2;

		float x = initPos.x + offset + gridPos.x * hexWidth;

		// every new line is offset in z direction by 3/4 of the hexagon height
		float z = initPos.z - gridPos.y * hexHeight * 0.75f - offsetY;

		return new Vector3(x, z, 0);
	}

	/// <summary>
	/// Get next tile
	/// </summary>
	/// <param name="direction">Moving direction</param>
	/// <param name="y">Current Y position</param>
	/// <param name="x">Current X position</param>
	/// <param name="rotation">Current rotation</param>
	/// <returns>Next tile GameObject</returns>
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
