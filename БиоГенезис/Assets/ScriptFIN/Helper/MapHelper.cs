using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bio
{
    public class MapHelper
    {
        private static MapHelper instance;
        public static MapHelper Instance { get { return instance; } }

        int mapLength;
        int mapWidth;
        TerrainName[,] terrainMap;
        bool[,] occupiedFloraTiles;

        public MapHelper(int length, int width, TerrainName[,] terrain, bool[,] occupiedTilesMap)
        {
            instance = this;
            mapLength = length;
            mapWidth = width;
            terrainMap = terrain;
            occupiedFloraTiles = occupiedTilesMap;
        }

        public bool IsInaccessible(Vector3 point)
        {
            return (IsInWater(point) || IsOutOfBounds(point));
        }

        public bool IsOutOfBounds(Vector3 point)
        {
            float x = point.x;
            float z = point.z;

            if ((0 < x && x < mapWidth) && (0 < z && z < mapLength))
                return false;
            else
                return true;
        }

        public bool IsInWater(Vector3 point)
        {
            float x = point.x;
            float z = point.z;

            if (!IsOutOfBounds(point) && (terrainMap[(int)x, (int)z] == TerrainName.DeepWater || terrainMap[(int)x, (int)z] == TerrainName.ShallowWater))
                return true;
            else
                return false;
        }

        public bool IsOccupiedByFlora(Vector3 point)
        {
            int x = (int)point.x;
            int z = (int)point.z;

            if (!IsOutOfBounds(point) && occupiedFloraTiles[x, z] == true)
                return true;
            else
                return false;
        }

        public void SetTileOccupancy(int x, int y, bool value)
        {
            occupiedFloraTiles[x, y] = value;
        }
    }
}
