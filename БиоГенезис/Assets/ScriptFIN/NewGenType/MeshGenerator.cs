using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Bio
{
    public class FaunaGenerator : MonoBehaviour
    {
        [SerializeField]
        GameObject[] predatorPrefabs;
        [SerializeField]
        GameObject[] preyPrefabs;

        [SerializeField]
        Color[] preyColors;

        [SerializeField]
        private float predatorDensity;
        [SerializeField]
        private float preyDensity;

        private int preyCount = 0;
        private int predatorCount = 0;

        TerrainName[,] terrainMap;
        private int seed;
        private int width;
        private int length;

        public GameObject instantiatedPredators;
        public GameObject instantiatedPrey;

        private bool[,] occupiedTilesMap;

        public void GeneratePreyFauna()
        {
            instantiatedPrey = new GameObject("Заяц");
            System.Random pseudoRNG = new System.Random(seed);

            for (int y = 0; y < length - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    if ((terrainMap[x, y] == TerrainName.Grass || terrainMap[x, y] == TerrainName.ShallowGrass) && !occupiedTilesMap[x, y])
                    {
                        float chance = (float)pseudoRNG.NextDouble();
                        if (chance <= preyDensity)
                        {
                            int chosenAnimalIndex = pseudoRNG.Next(0, preyPrefabs.Length);
                            GameObject obj = Instantiate(preyPrefabs[chosenAnimalIndex], new Vector3(x + 0.5f, 0, y + 0.5f), Quaternion.identity);
                            Zaya zaya = obj.GetComponent<Zaya>();
                            obj.transform.SetParent(instantiatedPrey.transform);

                            GenderHandler gender = preyCount % 2 == 0 ? new MaleHandler(zaya) : new FemaleHandler(zaya, 10);
                            zaya.Init(obj, 50, 25, 3, 5, 1, gender, GetRandomColor(preyColors));
                            obj.name = "Rabbit" + preyCount;

                            occupiedTilesMap[x, y] = true;
                            preyCount++;
                        }
                    }
                }
            }
        }

        public void GeneratePredatorFauna()
        {
            System.Random pseudoRNG = new System.Random(seed);
            instantiatedPredators = new GameObject("Волк");

            for (int y = 0; y < length - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    if ((terrainMap[x, y] == TerrainName.Grass || terrainMap[x, y] == TerrainName.ShallowGrass) && !occupiedTilesMap[x, y])
                    {
                        float chance = (float)pseudoRNG.NextDouble();
                        if (chance <= predatorDensity)
                        {
                            int chosenAnimalIndex = pseudoRNG.Next(0, predatorPrefabs.Length);
                            GameObject obj = Instantiate(predatorPrefabs[chosenAnimalIndex], new Vector3(x + 0.5f, 0, y + 0.5f), Quaternion.identity);
                            Animal animal = obj.GetComponent<Animal>();
                            obj.transform.SetParent(instantiatedPredators.transform);

                            GenderHandler gender = predatorCount % 2 == 0 ? new MaleHandler(animal) : new FemaleHandler(animal, 10);
                            animal.Init(obj, 50, 25, 3.5f, 5, 1, gender, GetRandomColor(preyColors));
                            obj.name = "Wolf" + predatorCount;

                            occupiedTilesMap[x, y] = true;
                            predatorCount++;
                        }
                    }
                }
            }
        }

        public void ClearGeneratedFauna()
        {
            Destroy(instantiatedPrey);
            Destroy(instantiatedPredators);

            preyCount = 0;
        }

        public void Init(int seed, int width, int length, float preyDensity, float predatorDensity, TerrainName[,] terrainMap, bool[,] occupiedTilesMap)
        {
            this.seed = seed;
            this.width = width;
            this.length = length;
            this.preyDensity = preyDensity;
            this.predatorDensity = predatorDensity;
            this.terrainMap = terrainMap;
            this.occupiedTilesMap = occupiedTilesMap;
        }

        private Color GetRandomColor(Color[] colorArray)
        {
            int colorIndex = Random.Range(0, colorArray.Length);
            return colorArray[colorIndex];
        }
    }

}

