using UnityEngine;

public class TurretPreviewRange : MonoBehaviour
{
    public float range = 15f;
    public Material rangeMaterial;
    public Color rangeColor = Color.white;
    public float lineWidth = 0.05f;

    private GameObject rangeIndicator;
    private LineRenderer lineRenderer;

    private void Start()
    {
        CreateRangeIndicator();
    }

    private void CreateRangeIndicator()
    {
        if (rangeIndicator == null)
        {
            rangeIndicator = new GameObject("RangeIndicator");
            rangeIndicator.transform.SetParent(transform);

            lineRenderer = rangeIndicator.AddComponent<LineRenderer>();
            lineRenderer.material = rangeMaterial != null ? rangeMaterial : new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = rangeColor;
            lineRenderer.endColor = rangeColor;
            lineRenderer.widthMultiplier = lineWidth;

            UpdateRangeIndicator();
        }
    }

    public void ShowRange()
    {
        if (rangeIndicator != null)
            rangeIndicator.SetActive(true);
    }

    public void HideRange()
    {
        if (rangeIndicator != null)
            rangeIndicator.SetActive(false);
    }

    public void UpdateRange(float newRange)
    {
        range = newRange;
        UpdateRangeIndicator();
    }

    private void UpdateRangeIndicator()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 100;
            Vector3[] positions = new Vector3[100];
            for (int i = 0; i < 100; i++)
            {
                float angle = i * Mathf.PI * 2 / 100;
                positions[i] = new Vector3(Mathf.Sin(angle) * range, 0, Mathf.Cos(angle) * range) + transform.position;
            }
            lineRenderer.SetPositions(positions);
        }
    }
}
