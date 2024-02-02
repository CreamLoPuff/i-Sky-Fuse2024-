using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Refrences:")]
    [SerializeField] Transform platformTransform;
    
    [Header("Platform Variables:")]
    [SerializeField] float platformMoveSpeed;
    [SerializeField] float platformRotateSpeed;


    [Header("Gizmo Variables:")]
    [SerializeField] Mesh gizmoMesh;
    [SerializeField] float gizmoScale;
    
    private Queue<Transform> movementNodes = new Queue<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // Get Movement Nodes
        for (int i = 0; i < transform.childCount; i++)
        {
            movementNodes.Enqueue(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Handle platform movement
        if (movementNodes.Count > 0)
        {
            platformTransform.position = Vector3.MoveTowards(platformTransform.position, movementNodes.Peek().position, platformMoveSpeed * Time.deltaTime);
            platformTransform.rotation = Quaternion.RotateTowards(platformTransform.rotation, movementNodes.Peek().rotation, platformRotateSpeed * Time.deltaTime);

            if (platformTransform.position == movementNodes.Peek().position)
            {
                movementNodes.Dequeue();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(transform.childCount > 1)
        {
            // Lines Between Nodes
            Gizmos.color = Color.green;
            for (int i = 1; i < transform.childCount; i++)
            {
                Gizmos.DrawLine(transform.GetChild(i-1).position, transform.GetChild(i).position);
            }

            // Directional Mesh Nodes
            Gizmos.color = Color.yellow;
            for (int i = 0;i < transform.childCount; i++)
            {
                if (gizmoMesh != null)
                {
                    Gizmos.DrawWireMesh(gizmoMesh, transform.GetChild(i).position + Vector3.up * -0.25f, transform.GetChild(i).rotation, Vector3.one * gizmoScale);
                }
            }
        }
    }
}
