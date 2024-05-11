using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGrid : MonoBehaviour
{
    [SerializeField] public Vector2 Boundaries;
    [SerializeField] private Vector2 cellSize;

    [SerializeField] GameObject canvasBlock;

    [SerializeField] bool clearGrid;

    [HideInInspector] public List<GameObject> grid = new();

#if UNITY_EDITOR
    private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

    private void _OnValidate()
    {
        UnityEditor.EditorApplication.delayCall -= _OnValidate;
        if (this == null) return;

        // Your code
        ClearGrid();

        if (transform.childCount == 0) // When grid is empty
        {
            MakeCanvasGrid();
        }
    }
#endif

    private void Start()
    {
        MakeCanvasGrid();
    }

    private void MakeCanvasGrid()
    {
        for (int i = 0; i < Boundaries.x; i++)
        {
            for (int j = 0; j < Boundaries.y; j++)
            {
                // Fill Canvas
                GameObject block = Instantiate(canvasBlock, transform);
                block.transform.localPosition = new Vector3(i * cellSize.x, j * cellSize.y, -1);

                grid.Add(block);
            }
        }
    }

    private void ClearGrid()
    {
        if (clearGrid)
        {
            foreach (var item in grid)
            {
                DestroyImmediate(item);
            }

            grid.Clear();

            clearGrid = false;
        }
    }

}
