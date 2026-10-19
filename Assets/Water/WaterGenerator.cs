using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaterGenerator : MonoBehaviour
{
    public Vector2 Size = new Vector2(10f, 10f);
    public float CellSize = 0.5f;

    public void GeneratePlane()
    {
        if (Size.x <= 0f || Size.y <= 0f || CellSize <= 0f)
        {
            Debug.LogError("WaterGenerator: size and cellSize must be positive.");
            return;
        }

        int cellsX = Mathf.Max(1, Mathf.RoundToInt(Size.x / CellSize));
        int cellsZ = Mathf.Max(1, Mathf.RoundToInt(Size.y / CellSize));

        float actualWidth  = cellsX * CellSize;
        float actualDepth  = cellsZ * CellSize;

        int vertsX = cellsX + 1;
        int vertsZ = cellsZ + 1;
        Vector3[] vertices = new Vector3[vertsX * vertsZ];
        Vector2[] uvs = new Vector2[vertices.Length];

        float halfW = actualWidth * 0.5f;
        float halfD = actualDepth * 0.5f;

        for (int z = 0; z < vertsZ; z++)
        {
            for (int x = 0; x < vertsX; x++)
            {
                int i = z * vertsX + x;
                vertices[i] = new Vector3(
                    x * CellSize - halfW,
                    0f,
                    z * CellSize - halfD
                );

                uvs[i] = new Vector2(
                    (float)x / cellsX,
                    (float)z / cellsZ
                );
            }
        }

        int[] triangles = new int[cellsX * cellsZ * 6];
        int t = 0;
        for (int z = 0; z < cellsZ; z++)
        {
            for (int x = 0; x < cellsX; x++)
            {
                int bl = z * vertsX + x;    // bottom-left
                int br = bl + 1;    // bottom-right
                int tl = bl + vertsX;   // top-left
                int tr = tl + 1;    // top-right

                // First triangle (bl, tl, tr)
                triangles[t++] = bl;
                triangles[t++] = tl;
                triangles[t++] = tr;

                // Second triangle (bl, tr, br)
                triangles[t++] = bl;
                triangles[t++] = tr;
                triangles[t++] = br;
            }
        }

        Vector3[] normals = new Vector3[vertices.Length];
        for (int i = 0; i < normals.Length; i++)
            normals[i] = Vector3.up;

        Mesh mesh = new Mesh();
        mesh.name = "Water";
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        mesh.vertices  = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}