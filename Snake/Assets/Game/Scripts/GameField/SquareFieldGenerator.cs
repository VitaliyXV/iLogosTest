using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class for generate and control squared field
/// </summary>
public class SquareFieldGenerator : BaseField
{
	public SquareFieldGenerator():base()
	{

	}

	public override void Generate()
	{
		// TODO: optimize generator with object pool

		// create field matrix (include borders)
		field = new GameObject[Height + 2, Width + 2];
		
		// calculate center position
		int offsetX = -(Width / 2);
		int offsetY = -(int)(Height / 2.5);

		// ganerate field
		for (int y = 0; y < Height + 2; y++)
		{
			for (int x = 0; x < Width + 2; x++)
			{
				var type = (CellType)Random.Range(0, System.Enum.GetValues(typeof(CellType)).Length);

				var tile = Instantiate(TileSurfaceObject, new Vector3(offsetX++, offsetY, 0), Quaternion.identity) as GameObject;
				tile.transform.SetParent(parent.transform);

				var cell = tile.GetComponent<Cell>();
				cell.Type = type;
				cell.SetColor();

				field[y, x] = tile;

				// create walls for borders
				if (y == 0 || x == 0 || y == Height + 1 || x == Width + 1)
				{
					CreateWall(field[y, x]);
				}
			}
			offsetY++;
			offsetX = -(Width / 2);
		}
		
		SetRandomWalls();
		SetFood(null);
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
			case Direction.Up: tile = field[++y, x]; break;
			case Direction.Down: tile = field[--y, x]; break;
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
			case Direction.Up: return Quaternion.Euler(0, 0, 0);
			case Direction.Down: return Quaternion.Euler(0, 0, 180);
			case Direction.Right: return Quaternion.Euler(0, 0, -90);
			case Direction.Left: return Quaternion.Euler(0, 0, 90);
			default: return Quaternion.Euler(0, 0, 0);
		}
	}
}
