using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class TextureGenerator
    {
        public Texture2D TextureFromColourMap(Color[] colourMap, int width, int length)
        {
            Texture2D texture = new Texture2D(width, length);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels(colourMap);
            texture.Apply();
            return texture;
        }
    }

