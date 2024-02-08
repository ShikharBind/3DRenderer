using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColliderController : MonoBehaviour
{
    public Tilemap solidWallTilemap; // Reference to the Tilemap containing solid walls

    void Start()
    {
        AddCollidersToSolidWalls();
    }

    void AddCollidersToSolidWalls()
    {
        BoundsInt bounds = solidWallTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = solidWallTilemap.GetTile(pos);
            if (tile != null) // Check if the current position has a tile
            {
                // Add collider if the tile represents a solid wall
                // You can customize this condition based on your tile setup
                if (tile.name == "SolidWallTile") // Change "SolidWallTile" to the name of your solid wall tile
                {
                    AddColliderAt(pos);
                }
            }
        }
    }

    void AddColliderAt(Vector3Int tilePosition)
    {
        // Calculate the world position of the tile
        Vector3 tileWorldPos = solidWallTilemap.GetCellCenterWorld(tilePosition);

        // Add a BoxCollider2D component to the tile
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();

        // Set collider size to match the tile size
        collider.size = solidWallTilemap.cellSize;

        // Set collider position to match the tile position
        collider.offset = tileWorldPos - transform.position;
    }
}
