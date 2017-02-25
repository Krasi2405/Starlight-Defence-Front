using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

    public float gizmoRadius = 0.25f;

	void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position , gizmoRadius);
    }

}
