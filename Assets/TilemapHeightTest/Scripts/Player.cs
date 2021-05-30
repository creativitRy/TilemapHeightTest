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

using UnityEngine;

namespace TilemapHeightTest
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(SpriteRenderer))]
	public class Player : MonoBehaviour
	{
		private SpriteRenderer _sprite;
		private Rigidbody2D _body;
		public float MoveSpeed;

		private void Awake()
		{
			_body = GetComponent<Rigidbody2D>();
			_sprite = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			var horiz = Input.GetAxis("Horizontal");
			var vert = Input.GetAxis("Vertical");

			_body.velocity = new Vector2(horiz, vert) * MoveSpeed;

			foreach (TileHeightManager Instance in TileHeightManager.Instances) {
				Instance.ReportPosition(transform.position, _sprite.sprite.bounds);
			}
		}
	}
}
