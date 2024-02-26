using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bio
{
    public class FloraGenerator : MonoBehaviour
    {
        [SerializeField]
        GameObject[] treePrefabs;
        [SerializeField]
        GameObject[] plantPrefabs;

        [SerializeField]
        private float treeDensity;
        [SerializeField]
        private float plantDensity;

        TerrainName[,] terrainMap;
        private int seed;
        private int width;
        private int length;

        public GameObject instantiatedTrees;
        public GameObject instantiatedPlants;

        private bool[,] occupiedTilesMap;

        private int floraCount;

        public void GenerateTrees()
        {
            instantiatedTrees = new GameObject("Деревья");
            System.Random pseudoRNG = new System.Random(seed);

            for (int y = 0; y < length - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    if(terrainMap[x, y] == TerrainName.Grass && !occupiedTilesMap[x, y])
                    {
                        float chance = (float)pseudoRNG.NextDouble();
                        if(chance <= treeDensity)
                        {
                            int chosenTreeIndex = pseudoRNG.Next(0, treePrefabs.Length);
                            GameObject obj = Instantiate(treePrefabs[chosenTreeIndex], new Vector3(x + 0.5f, 0, y + 0.5f), Quaternion.identity);
                            obj.transform.SetParent(instantiatedTrees.transform);

                            occupiedTilesMap[x, y] = true;
                        }
                    }
                }
            }
        }

        public void GeneratePlants()
        {
            instantiatedPlants = new GameObject("Растение");
            System.Random pseudoRNG = new System.Random(seed);

            for (int y = 0; y < length - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    if ((terrainMap[x, y] == TerrainName.Grass || terrainMap[x, y] == TerrainName.ShallowGrass) && !occupiedTilesMap[x, y])
                    {
                        float chance = (float)pseudoRNG.NextDouble();
                        if (chance <= plantDensity)
                        {
                            int chosenPlantIndex = pseudoRNG.Next(0, plantPrefabs.Length);
                            GameObject obj = Instantiate(plantPrefabs[chosenPlantIndex], new Vector3(x + 0.5f, 0, y + 0.5f), Quaternion.identity);
                            obj.transform.SetParent(instantiatedPlants.transform);
                            obj.GetComponent<Plant>().Init(30, 1f);
                            obj.name = "Plant " + floraCount;
                            floraCount++;
                        }
                    }
                }
            }
        }

        public void ClearGeneratedFlora()
        {
            Destroy(instantiatedTrees);
            Destroy(instantiatedPlants);
        }

        public void Init(int seed, int width, int length, float plantDensity, float treeDensity, TerrainName[,] terrainMap, bool[,] occupiedTilesMap)
        {
            this.seed = seed;
            this.width = width;
            this.length = length;
            this.plantDensity = plantDensity;
            this.treeDensity = treeDensity;
            this.terrainMap = terrainMap;
            this.occupiedTilesMap = occupiedTilesMap;
        }
    }
}
