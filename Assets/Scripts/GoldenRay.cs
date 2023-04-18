using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class GoldenRay : MonoBehaviour
{
    public float thickness = 0.1f;

    private LineRenderer lineRenderer;
    private MeshCollider meshCollider;
    private Mesh mesh;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        // Create a mesh that follows the shape of the line renderer
        mesh = new Mesh();
        mesh.name = "LineCollider";

        Vector3[] vertices = new Vector3[lineRenderer.positionCount * 2];
        int[] triangles = new int[(lineRenderer.positionCount - 1) * 6];

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            // Get the position and rotation of the current point on the line renderer
            Vector3 point = lineRenderer.GetPosition(i);
            Quaternion rotation = Quaternion.identity;
            if (i < lineRenderer.positionCount - 1)
            {
                rotation = Quaternion.LookRotation(lineRenderer.GetPosition(i + 1) - point);
            }

            // Calculate the offset for the current point, based on the thickness of the line
            Vector3 offset = rotation * Vector3.up * thickness;

            // Add vertices for the top and bottom of the line at the current point
            vertices[i * 2] = point + offset;
            vertices[i * 2 + 1] = point - offset;

            // Add triangles for the current segment of the line
            if (i < lineRenderer.positionCount - 1)
            {
                int triangleIndex = i * 6;
                triangles[triangleIndex] = i * 2;
                triangles[triangleIndex + 1] = i * 2 + 1;
                triangles[triangleIndex + 2] = i * 2 + 2;
                triangles[triangleIndex + 3] = i * 2 + 2;
                triangles[triangleIndex + 4] = i * 2 + 1;
                triangles[triangleIndex + 5] = i * 2 + 3;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshCollider.sharedMesh = mesh;
    }

    private void Update()
    {
        // Update the mesh to follow the shape of the line renderer
        Vector3[] vertices = new Vector3[lineRenderer.positionCount * 2];

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            // Get the position and rotation of the current point on the line renderer
            Vector3 point = lineRenderer.GetPosition(i);
            Quaternion rotation = Quaternion.identity;
            if (i < lineRenderer.positionCount - 1)
            {
                rotation = Quaternion.LookRotation(lineRenderer.GetPosition(i + 1) - point);
            }

            // Calculate the offset for the current point, based on the thickness of the line
            Vector3 offset = rotation * Vector3.up * thickness;

            // Update the vertices for the top and bottom of the line at the current point
            vertices[i * 2] = point + offset;
            vertices[i * 2 + 1] = point - offset;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
}
