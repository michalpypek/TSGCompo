using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class SightCone : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    [SerializeField]
    private float angle = 30f;
    [SerializeField]
    private float range = 10f;
    [SerializeField]
    private int rayCount = 100;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Raycast();
    }

    private void Raycast()
    {
        Vector3[] vertices = new Vector3[rayCount + 1];
        int[] indices = new int[(rayCount + 1) * 3];

        float angleDiff = angle * 2 / (rayCount - 1);

        vertices[0] = (transform.InverseTransformPoint(transform.position));

        for (int i = 0; i < rayCount; i++)
        {
            float currAngle = -angle + angleDiff * i;

            Vector3 direction = Quaternion.AngleAxis(currAngle, -transform.forward) * transform.up;

            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, direction, range);

            if (raycastHit.collider == null)
            {
                vertices[i + 1] = transform.InverseTransformPoint(transform.position + (direction * range));
            }
            else
            {
                vertices[i + 1] = transform.InverseTransformPoint(transform.position + (direction * raycastHit.distance));
            }
        }

        for (int i = 0; i < rayCount; i++)
        {
            indices[i * 3] = 0;
            indices[i * 3 + 1] = i;
            indices[i * 3 + 2] = i + 1;
        }

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        mesh.vertices = vertices;
        mesh.triangles = indices;
    }
}
