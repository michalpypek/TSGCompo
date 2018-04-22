using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float range = 10f;
    [SerializeField]
    float angle = 30f;

    // Change this and I'll kill you <3
    const int RAY_COUNT = 4;

    Vector3 targetPosition = Vector3.zero;

    public float steps = 2;
    float currentNumOfSteps = 0f;

    public static Vector2 Substract(Vector2 lhs, Vector3 rhs)
    {
        return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            targetPosition = MapGenerator.instance.targetPosition;
        }

        if(Vector3.Distance(transform.position, targetPosition) < 25f)
        {
            if (!MapGenerator.instance.finalTargetFound)
            {
                MapGenerator.instance.SetNewTargetPosition();
                currentNumOfSteps++;
                if (currentNumOfSteps >= steps)
                {
                    MapGenerator.instance.SetFinalTarget();
                }
            }
            else
            {
                Debug.LogError("TUMTURUM, FAJNAL SCRIN");
            }
        }

        RaycastHit2D[] raycastHits2D = new RaycastHit2D[RAY_COUNT];
        Vector3[] dirs = new Vector3[RAY_COUNT];

        float angleDiff = angle * 2 / (RAY_COUNT - 1);

        for (int i = 0; i < RAY_COUNT; i++)
        {
            float currAngle = -angle + angleDiff * i;

            Vector3 direction = Quaternion.AngleAxis(currAngle, -transform.forward) * transform.up;

            raycastHits2D[i] = Physics2D.Raycast(transform.position, direction, range);

            if (raycastHits2D[i].collider == null)
            {
                dirs[i] = direction;
            }
            else
            {
                dirs[i] = Substract(raycastHits2D[i].point, transform.position);
            }
        }

        Vector3 directionToMove;
        if (raycastHits2D[1].collider == null && raycastHits2D[2].collider == null)
        {
            directionToMove = targetPosition - transform.position;
        }
        else if (dirs[0].magnitude > dirs[3].magnitude)
        {
            directionToMove = dirs[0].normalized;
        }
        else
        {
            directionToMove = dirs[3].normalized;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.forward, directionToMove.normalized), Time.deltaTime * speed);

        Debug.DrawLine(transform.position, transform.position + directionToMove * range * 2, Color.yellow);

        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (targetPosition - transform.position).normalized * range);

        Gizmos.color = Color.green;
        for (int i = 0; i < RAY_COUNT; i++)
        {
            float angleDiff = angle * 2 / (RAY_COUNT - 1);
            float currAngle = -angle + angleDiff * i;

            Vector3 direction = Quaternion.AngleAxis(currAngle, -transform.forward) * transform.up;

            Gizmos.DrawLine(transform.position, transform.position + (direction * range));
        }
    }
}