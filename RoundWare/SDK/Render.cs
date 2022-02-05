using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoundWare.SDK
{
    internal class Render
    {
		public static Texture2D MakeTex(int width, int height, Color col)
		{
			Color[] array = new Color[width * height];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = col;
			}
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixels(array);
			texture2D.Apply();
			return texture2D;
		}
	}
}
