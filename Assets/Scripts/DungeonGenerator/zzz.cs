using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zzz : MonoBehaviour
{
    public Material terrainMaterial;
    public Material edgeMaterial;
    public int size = 100;
    public float scale = 0.1f;
    public float mountainLevel = .4f;
    Cell[,] grid;
    List<Vector3> edgeVertices = new List<Vector3>();
    List<int> edgeTriangles = new List<int>();

    private void Start()
    {
        float[,] noiseMap = new float[size, size];
        float xOffset = Random.Range(-1000, 1000);
        float yOffset = Random.Range(-1000, 1000);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        float[,] fallOffMap = new float[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float xv = x / (float)size * 2 - 1;
                float yv = y / (float)size * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                fallOffMap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }

        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = new Cell();
                float noiseValue = noiseMap[x, y];
                noiseValue -= fallOffMap[x, y];
                cell.isMountain = noiseValue < mountainLevel;
                grid[y, x] = cell;
            }
        }

        // Tính toán và tạo đường nối trước khi vẽ mesh terrain
        CalculateEdges(grid);

        DrawTerrainMesh(grid);
        DrawTexture(grid);
        DrawEdgeMesh();
    }

    void CalculateEdges(Cell[,] grid)
    {
        // Lặp qua từng ô trong grid
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (!cell.isMountain)
                {
                    // Kiểm tra ô bên trái
                    if (x > 0 && grid[x - 1, y].isMountain)
                    {
                        AddEdge(new Vector3(x - 0.5f, 0, y + 0.5f), new Vector3(x - 0.5f, -1, y + 0.5f));
                    }

                    // Kiểm tra ô bên phải
                    if (x < size - 1 && grid[x + 1, y].isMountain)
                    {
                        AddEdge(new Vector3(x + 0.5f, 0, y + 0.5f), new Vector3(x + 0.5f, -1, y + 0.5f));
                    }

                    // Kiểm tra ô bên trên
                    if (y < size - 1 && grid[x, y + 1].isMountain)
                    {
                        AddEdge(new Vector3(x + 0.5f, 0, y + 0.5f), new Vector3(x + 0.5f, -1, y + 0.5f));
                    }

                    // Kiểm tra ô bên dưới
                    if (y > 0 && grid[x, y - 1].isMountain)
                    {
                        AddEdge(new Vector3(x - 0.5f, 0, y - 0.5f), new Vector3(x - 0.5f, -1, y - 0.5f));
                    }
                }
            }
        }
    }

    void AddEdge(Vector3 start, Vector3 end)
    {
        int vertexIndex = edgeVertices.Count;
        edgeVertices.Add(start);
        edgeVertices.Add(end);
        edgeTriangles.Add(vertexIndex);
        edgeTriangles.Add(vertexIndex + 1);
    }

    void DrawTerrainMesh(Cell[,] grid)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (!cell.isMountain)
                {
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
                    for (int k = 0; k < 6; k++)
                    {
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

    void DrawTexture(Cell[,] grid)
    {
        Texture2D texture = new Texture2D(size, size);
        Color[] colorMap = new Color[size * size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (cell.isMountain)
                {
                    colorMap[y * size + x] = Color.black;
                }
                else
                {
                    colorMap[y * size + x] = Color.yellow;
                }
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.SetPixels(colorMap);
        texture.Apply();

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = terrainMaterial;
        meshRenderer.material.mainTexture = texture;
    }

    void DrawEdgeMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = edgeVertices.ToArray();
        mesh.triangles = edgeTriangles.ToArray();
        mesh.RecalculateNormals();

        GameObject edgeObj = new GameObject("Edge");
        edgeObj.transform.SetParent(transform);

        MeshFilter meshFilter = edgeObj.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = edgeObj.AddComponent<MeshRenderer>();
        meshRenderer.material = edgeMaterial;
    }
}

