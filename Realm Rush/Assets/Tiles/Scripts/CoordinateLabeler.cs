using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabeler : MonoBehaviour
{
    Color defaultColor = Color.white;
    Color blockedColor = Color.red;
    Color exploredColor = Color.yellow;
    Color pathColor = new Color(1f, 0f, 1f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        DisplayCoordinates();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled=true;
        }

        SetLabelColor();
        ToggleLabels();
    }



    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }
        
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C)) { label.enabled = !label.IsActive(); }
    }

    void SetLabelColor()
    {
        if (gridManager ==  null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node ==  null) { return; }
        if (!node.isWalkable) { label.color = blockedColor; }
        else if (node.isPath) { label.color = pathColor; }
        else if (node.isExplored) { label.color = exploredColor; }
        else { label.color = defaultColor; }
    }
}
