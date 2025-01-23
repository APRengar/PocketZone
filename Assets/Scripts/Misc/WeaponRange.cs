using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class WeaponRange : MonoBehaviour
{
    [SerializeField] private Color lineColor = Color.green; // Color of the circle
    [SerializeField] private int segmentCount = 36;         // Number of segments for the circle
    [SerializeField] private float lineWidth = 0.05f;       // Width of the line
    [SerializeField] private string sortingLayerName = "Guns";
    [SerializeField] private int orderInLayer = 5; 

    private CircleCollider2D circleCollider;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();

        // Configure the LineRenderer
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = segmentCount;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        // Set sorting layer and order in layer to draw above other objects
        lineRenderer.sortingLayerName = sortingLayerName;
        lineRenderer.sortingOrder = orderInLayer;

        DrawCircle();
    }

    private void Update()
    {
        DrawCircle();
    }

    private void DrawCircle()
    {
        if (circleCollider == null || lineRenderer == null) return;

        float radius = circleCollider.radius;
        Vector3 offset = circleCollider.offset;

        for (int i = 0; i < segmentCount; i++)
        {
            float angle = (i * Mathf.PI * 2) / segmentCount;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // Apply the offset and set positions
            lineRenderer.SetPosition(i, new Vector3(x + offset.x, y + offset.y, 0));
        }
    }
}

