using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskFog;
    [SerializeField] private LayerMask layerMask;

    public Tilemap fogOfWarTilemap;
    public Tile fogTile;

    public GameObject objOrigin;
    private Vector3 origin;

    private HashSet<Vector3Int> revealedTiles = new HashSet<Vector3Int>();


    private Mesh mesh;
    private float fov;
    private float viewDistance;
    private float startingAngle;


    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 50f;
        viewDistance = 12f;
        origin = Vector3.zero;

        // Initialize fog of war
        InitializeFogOfWar();

    }

    // Reveal Fog
    private void Update()
    {
        // update ray cast origin
        origin = objOrigin.transform.position;



        // Update fog of war based on player's position
        UpdateFogOfWar();
    }
    void InitializeFogOfWar()
    {                   // We can adjust these values, how big fog area we want.
        for (int i = -180; i < 20; ++i)
        {
            for (int j = -180; j < 20; ++j)
            {
                fogOfWarTilemap.SetTile(new Vector3Int(i, j, 0), fogTile);
            }
        }
    }

    public void UpdateFogOfWar()
    {

        float fov = 360.0f;
        int rayCount = 32;
        float angle = objOrigin.transform.rotation.eulerAngles.z;
        float angleIncrease = fov / rayCount;
        float viewDistance = 10f;



        for (int i = 0; i <= rayCount-1; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Utils.GetVectorFromAngle(angle), viewDistance, layerMaskFog);


            var hitPoint = hit.point;
 
            Vector3Int rayCellHit = fogOfWarTilemap.WorldToCell(hitPoint);
            // Debug.DrawLine(origin, rayCellHit);


            if (!revealedTiles.Contains(rayCellHit))
            {
                // Debug.LogFormat("revealed {0}|{1}|{2}; total: {3}", rayCellHit.x, rayCellHit.y, rayCellHit.z, revealedTiles.Count);
                // Debug.DrawLine(origin, rayCellHit, Color.red, 0.5f);

                Vector3Int neighbour1 = new Vector3Int(rayCellHit.x + 1, rayCellHit.y, rayCellHit.z);
                Vector3Int neighbour2 = new Vector3Int(rayCellHit.x - 1, rayCellHit.y, rayCellHit.z);
                Vector3Int neighbour3 = new Vector3Int(rayCellHit.x, rayCellHit.y + 1, rayCellHit.z);
                Vector3Int neighbour4 = new Vector3Int(rayCellHit.x, rayCellHit.y - 1, rayCellHit.z);
                
                fogOfWarTilemap.SetTile(rayCellHit, null); // Set to null to reveal the tile
                revealedTiles.Add(rayCellHit);

                fogOfWarTilemap.SetTile(neighbour1, null); // Set to null to reveal the tile
                fogOfWarTilemap.SetTile(neighbour2, null); // Set to null to reveal the tile
                fogOfWarTilemap.SetTile(neighbour3, null); // Set to null to reveal the tile
                fogOfWarTilemap.SetTile(neighbour4, null); // Set to null to reveal the tile
            }

            angle += angleIncrease;
        }
    }

    // Detect enemies (update mask)
    private void LateUpdate()
    {
        int rayCount = 50;
        float angle = startingAngle + objOrigin.transform.rotation.eulerAngles.z + fov/2.0f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Utils.GetVectorFromAngle(angle), viewDistance, layerMask);

            if (raycastHit2D.collider == null)
            {
                // No hit
                vertex = origin + Utils.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit object
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = Utils.GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }
    public void SetFoV(float fov)
    {
        this.fov = fov;
    }
    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }
}
