using UnityEngine;
using System.Collections.Generic;

public class SCS_GridMap : MonoBehaviour
{
//Размерность и переменные
public float WaterGrid = .4f;
public float Scale = .1f;
public int size = 100;

//Ячейки из SCS_CellMap
SCS_CellMap.cell[,] GridQ;

    //Старт

    private void Start() {
        
    
    //perlinnoise
//     float[,] noiseMap = new float [size, size];
// for(int y = 0; y < size; y++) {
//     for(int x = 0; x < size; x++) {
//  float noiseValue = Mathf.PerlinNoise(x * Scale, y * Scale);
//     noiseMap[x,y] = noiseValue;
    
    // }for 2
    // }for 1

//gridQ
    GridQ = new SCS_CellMap.cell[size, size];
    float[,] noiseMap = new float [size, size];
    for(int y = 0; y < size; y++) {
        for (int x = 0; x < size; x++) {
                SCS_CellMap.cell cellQ = new SCS_CellMap.cell();
                float noiseValue = Mathf.PerlinNoise(x * Scale, y * Scale);
                noiseMap[x,y] = noiseValue;
                cellQ.IsWater = noiseValue < WaterGrid;
                GridQ[x,y] = cellQ;
            }
            //for2
            } 
            //for1
            DrawTerrainMesh(GridQ);
            
}
//endstart

void DrawTerrainMesh(SCS_CellMap.cell[,] grid) {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        for(int y = 0; y < size; y++) {
            for(int x = 0; x < size; x++) {
                SCS_CellMap.cell cellQ = grid[x, y];
                if(!cellQ.IsWater) {
                    Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                    Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                    Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                    Vector3 d = new Vector3(x + .5f, 0, y - .5f);
                    Vector2 uvA = new Vector2(x / (float)size, y / (float)size);
                    Vector2 uvB = new Vector2((x + 1) / (float)size, y / (float)size);
                    Vector2 uvC = new Vector2(x / (float)size, (y + 1) / (float)size);
                    Vector2 uvD = new Vector2((x + 1) / (float)size, (y + 1) / (float)size);
                    Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                    Vector2[] uv = new Vector2[] { uvA, uvB, uvC, uvB, uvD, uvC };
                    for(int k = 0; k < 6; k++) {
                        vertices.Add(v[k]);
                        triangles.Add(triangles.Count);
                        uvs.Add(uv[k]);
                    }
                }
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
    }



//void Покраска
//  void OnDrawGizmos() {
//     if(!Application.isPlaying) return;
//     for(int y = 0; y < size; y++) {
//     for(int x = 0; x < size; x++) {
// SCS_CellMap.cell cellQ = GridQ[x,y];
// if(cellQ.IsWater)
// Gizmos.color = Color.blue;
// else
// Gizmos.color = Color.green;
// UnityEngine.Vector3 pos = new UnityEngine.Vector3(x,0,y);
// Gizmos.DrawCube(pos, UnityEngine.Vector3.one);
//     }
//     //for2
//     }
//     //for1
// }
//endl
}