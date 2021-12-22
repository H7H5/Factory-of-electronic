using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapHelper : MonoBehaviour
{
    public static TileMapHelper Instance;
    private Tilemap map;
    private Camera mainCamera;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        map = GetComponent<Tilemap>();
        mainCamera = Camera.main;
    }

    public Vector3 getTilePositionCamera()
    {
        Vector3Int cellPositionCamera = map.WorldToCell(mainCamera.transform.position);
        return correctTilePosition(map.CellToLocal(cellPositionCamera));
    }

    private Vector3 correctTilePosition (Vector3 cellPosition)
    {
        cellPosition.x -= 0.22f;
        cellPosition.y -= 0.08f;
        cellPosition.z = 1f;
        return cellPosition;
    }

    public Vector3 getTilePosition ()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = map.WorldToCell(worldPosition);
        return correctTilePosition(map.CellToLocal(cellPosition));
    }

    void Update()
    {

    }
}
