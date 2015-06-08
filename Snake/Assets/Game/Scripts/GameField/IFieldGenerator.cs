using UnityEngine;
using System.Collections;

public interface IFieldGenerator<T>
{
	int Width { get; set; }
	int Height { get; set; }
	int WallCount { get; set; }
	int VisibleRadius { get; set; }
	T this[int y, int x] { get; set; }

	T PlayerStartPosition { get; set; }
	int PlayerStartPositionX { get; set; }
	int PlayerStartPositionY { get; set; }

	/// <summary>
	/// Generate field
	/// </summary>
	void Generate();

	/// <summary>
	/// Set random walls on field
	/// </summary>
	void SetRandomWalls();

	/// <summary>
	/// Set food on field
	/// </summary>
	/// <param name="existingFood">Existing food, or null, if first food created</param>
	/// <returns></returns>
	GameObject SetFood(GameObject existingFood);

	/// <summary>
	/// Cleat the field
	/// </summary>
	void Clear();

	/// <summary>
	/// Get next tile
	/// </summary>
	/// <param name="direction">Moving direction</param>
	/// <param name="y">Current Y position</param>
	/// <param name="x">Current X position</param>
	/// <param name="rotation">Current rotation</param>
	/// <returns>Next tile GameObject</returns>
	GameObject NextTile(Direction direction, ref int y, ref int x, ref Quaternion rotation);
}
