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
	[RequireComponent(typeof(Camera))]
	public class CameraManager : MonoBehaviour
	{
		private Camera _camera;
		public Transform Follow;
		public Vector3 Offset = new Vector3(0, 0, -10);
		public float LerpFactor = 0.1f;

		private void Awake()
		{
			_camera = GetComponent<Camera>();
		}

		private void Update()
		{
			var target = Follow.position + Offset;
			_camera.transform.position = Vector3.Lerp(_camera.transform.position, target, LerpFactor);
		}
	}
}