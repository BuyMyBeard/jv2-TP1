using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshObjective : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.7f);
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
