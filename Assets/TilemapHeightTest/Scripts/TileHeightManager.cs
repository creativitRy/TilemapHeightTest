/*
MIT License

Copyright (c) 2018 Gahwon Lee

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TilemapHeightTest
{
	public class TileHeightManager : MonoBehaviour
	{
		public static List<TileHeightManager> Instances { get; set; }

		public List<TileHeightGroup> TileHeightGroups;
		private readonly Dictionary<Sprite, float> _tileHeights = new Dictionary<Sprite, float>();
		private readonly HashSet<Vector3Int> _returnBack = new HashSet<Vector3Int>();

		public Tilemap Tilemap;
		public Tilemap Overlay;

		private void Awake()
		{
			if (Instances == null)
            {
				Instances = new List<TileHeightManager>();
            }
			Instances.Add(this);
			
			if (TileHeightGroups != null)
			{
				foreach (var group in TileHeightGroups)
				{
					foreach (var tile in group.Tiles)
					{
						_tileHeights.Add(tile.Sprite, tile.Height);
					}
				}
			}
		}

		public void ReportPosition(Vector3 position, Bounds bounds)
		{
			// clear previous overlay tiles
			foreach (var pos in _returnBack)
			{
				Tilemap.SetTile(pos, Overlay.GetTile(pos));
				Overlay.SetTile(pos, null);
			}
			_returnBack.Clear();

			// project bounds to world position
			bounds.center += position;
			var playerDepth = bounds.min.y;
			
			// for all affected tiles
			var min = Vector2Int.FloorToInt(bounds.min);
			var max = Vector2Int.FloorToInt(bounds.max);
			for (var y = min.y; y <= max.y; y++)
			{
				var playerLocalHeight = y - playerDepth;
				for (var x = min.x; x <= max.x; x++)
				{
					// get the sprite
					var sprite = Tilemap.GetSprite(new Vector3Int(x, y, 0));
					if (sprite == null)
						continue;

					// compare heights
					var tileHeight = _tileHeights.ContainsKey(sprite) ? _tileHeights[sprite] : 0f;
					if (tileHeight > playerLocalHeight)
					{
						var pos = new Vector3Int(x, y, 0);
						_returnBack.Add(pos);
						Overlay.SetTile(pos, Tilemap.GetTile(pos));
						Tilemap.SetTile(pos, null);
					}
				}
			}
		}
	}
}
