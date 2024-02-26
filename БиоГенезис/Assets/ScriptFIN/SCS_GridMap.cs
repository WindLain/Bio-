using UnityEngine;
using System.Collections.Generic;
using UnityEngine.TerrainTools;
using UnityEngine.UIElements;
using System.Security.Cryptography.X509Certificates;

public class SCS_GridMap : MonoBehaviour
{
//Размерность и переменные
public Material uglMat;
public Material Zemlya;
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
            //DrawTexture(GridQ);
            DrawEdgeMesh(GridQ);     
}
//endstart

void DrawTerrainMesh(SCS_CellMap.cell[,] gridQ) {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        for(int y = 0; y < size; y++) {
            for(int x = 0; x < size; x++) {
                SCS_CellMap.cell cellQ = GridQ[x, y];
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
        meshRenderer.material = Zemlya;

    }


 void DrawEdgeMesh(SCS_CellMap.cell[,] grid) {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        for(int y = 0; y < size; y++) {
            for(int x = 0; x < size; x++) {
                SCS_CellMap.cell cellQ = grid[x, y];
                if(!cellQ.IsWater) {
                    if(x > 0) {
                        SCS_CellMap.cell left = grid[x - 1, y];
                        if(left.IsWater) {
                            Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                            Vector3 b = new Vector3(x - .5f, 0, y - .5f);
                            Vector3 c = new Vector3(x - .5f, -1, y + .5f);
                            Vector3 d = new Vector3(x - .5f, -1, y - .5f);
                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for(int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                    if(x < size - 1) {
                        SCS_CellMap.cell right = grid[x + 1, y];
                        if(right.IsWater) {
                            Vector3 a = new Vector3(x + .5f, 0, y - .5f);
                            Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                            Vector3 c = new Vector3(x + .5f, -1, y - .5f);
                            Vector3 d = new Vector3(x + .5f, -1, y + .5f);
                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for(int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                    if(y > 0) {
                        SCS_CellMap.cell down = grid[x, y - 1];
                        if(down.IsWater) {
                            Vector3 a = new Vector3(x - .5f, 0, y - .5f);
                            Vector3 b = new Vector3(x + .5f, 0, y - .5f);
                            Vector3 c = new Vector3(x - .5f, -1, y - .5f);
                            Vector3 d = new Vector3(x + .5f, -1, y - .5f);
                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for(int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                    if(y < size - 1) {
                        SCS_CellMap.cell up = grid[x, y + 1];
                        if(up.IsWater) {
                            Vector3 a = new Vector3(x + .5f, 0, y + .5f);
                            Vector3 b = new Vector3(x - .5f, 0, y + .5f);
                            Vector3 c = new Vector3(x + .5f, -1, y + .5f);
                            Vector3 d = new Vector3(x - .5f, -1, y + .5f);
                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for(int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                }
            }
        }
mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        GameObject edgeObj = new GameObject("Edge");
        edgeObj.transform.SetParent(transform);

        MeshFilter meshFilter = edgeObj.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = edgeObj.AddComponent<MeshRenderer>();
        meshRenderer.material = uglMat;
    }




// void DrawUgl(SCS_CellMap.cell[,] GridQ) {
// Mesh mesh = new Mesh();
// List<Vector3> vertices = new List<Vector3>();
// List<int> triangles = new List<int>();
// for(int y = 0; y < size; y++) {
// for(int x = 0; x < size; x++) {
// SCS_CellMap.cell cellQ = GridQ[x,y];
// if (!cellQ.IsWater) {
//     if(x > 0) {
// SCS_CellMap.cell left = GridQ[x - 1, y];
// if(left.IsWater) {
// Vector3 a = new Vector3(x - .5f, 0, y + .5f);
// Vector3 b = new Vector3(x - .5f, 0, y - .5f);
// Vector3 c = new Vector3(x - .5f, -1, y + .5f);
// Vector3 d = new Vector3(x - .5f, -1, y - .5f);
// Vector3[] v = new Vector3[] {a, b, c, b, d, c};
// for(int k = 0; k < 6; k++) {
// vertices.Add(v[k]);
//     triangles.Add(triangles.Count);
     
//     }//for
//     }//if3
//     }//if2

// if(x < size - 1) {
// SCS_CellMap.cell left = GridQ[x - 1, y];
// if(left.IsWater) {
// Vector3 a = new Vector3(x - .5f, 0, y - .5f);
// Vector3 b = new Vector3(x + .5f, 0, y - .5f);
// Vector3 c = new Vector3(x - .5f, -1, y - .5f);
// Vector3 d = new Vector3(x + .5f, -1, y - .5f);
// Vector3[] v = new Vector3[] {a, b, c, b, d, c};
// for(int k = 0; k < 6; k++) {
// vertices.Add(v[k]);
//     triangles.Add(triangles.Count);
     
//     }//for
//     }//if3
//     }//if2

// if(y > 0) {
// SCS_CellMap.cell left = GridQ[x - 1, y];
// if(left.IsWater) {
// Vector3 a = new Vector3(x - .5f, 0, y - .5f);
// Vector3 b = new Vector3(x - .5f, 0, y - .5f);
// Vector3 c = new Vector3(x - .5f, -1, y - .5f);
// Vector3 d = new Vector3(x - .5f, -1, y - .5f);
// Vector3[] v = new Vector3[] {a, b, c, b, d, c};
// for(int k = 0; k < 6; k++) {
// vertices.Add(v[k]);
//     triangles.Add(triangles.Count);
     
//     }//for
//     }//if3
//     }//if2

// if(y < size - 1) {
// SCS_CellMap.cell left = GridQ[x - 1, y];
// if(left.IsWater) {
// Vector3 a = new Vector3(x + .5f, 0, y + .5f);
// Vector3 b = new Vector3(x - .5f, 0, y + .5f);
// Vector3 c = new Vector3(x + .5f, -1, y + .5f);
// Vector3 d = new Vector3(x - .5f, -1, y + .5f);
// Vector3[] v = new Vector3[] {a, b, c, b, d, c};
// for(int k = 0; k < 6; k++) {
// vertices.Add(v[k]);
//     triangles.Add(triangles.Count);
     
//     }//for
//     }//if3
//     }//if2

//           }//if1
//           }//for2
//           }//for 1
//         mesh.vertices = vertices.ToArray();
//         mesh.triangles = triangles.ToArray();
//         mesh.RecalculateNormals();

//         GameObject UglObj = new GameObject("Угол");
//         UglObj.transform.SetParent(transform);

//         MeshFilter meshFilter = UglObj.AddComponent<MeshFilter>();
//         meshFilter.mesh = mesh;

//         MeshRenderer meshRenderer = UglObj.AddComponent<MeshRenderer>();
//         meshRenderer.material = uglMat;
//       }//drawUGLend

// void DrawTexture(SCS_CellMap.cell[,] GridQ) {
//     Texture2D texture = new Texture2D(size, size);
//     Color[] colorMap = new Color[size * size];
//     for(int y = 0; y < size; y++) {
//         for(int x = 0; x < size; x++) {
// SCS_CellMap.cell cellQ = GridQ[x, y];
// if(cellQ.IsWater)
// colorMap[y * size + x] = Color.blue;
// else 
// colorMap[y * size + x] = Zemlya;

//        }//for2
//    }//for1
//     texture.filterMode = FilterMode.Point;
//     texture.SetPixels(colorMap);
//     texture.Apply();

//     MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
//     meshRenderer.material = Zemlya;
//     meshRenderer.material.mainTexture = texture;

// }

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