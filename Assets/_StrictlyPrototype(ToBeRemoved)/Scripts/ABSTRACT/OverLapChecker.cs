using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OverLapChecker : MonoBehaviour
{
    #region Private Variables

    [Header("Values")]
    [Space(5)]
    [Tooltip("Time Interval Before another Overlap Check")]
    [SerializeField] private float interval = 0.2f; // Interval in seconds

    [SerializeField] private Vector3 boxSize = new Vector3(1f, 1f, 1f); // Size of the box for the overlap check
    [SerializeField] private LayerMask hitLayers; // Layers to consider for overlap checks

    [Tooltip("Position Offset that will be added to the Current Position for checking Overlap")]
    [SerializeField] private Vector3 offset;

#if UNITY_EDITOR
    [Space(5)]
    [Header("For Debugging")]
    [SerializeField] private Color overLapBoxGizmoColor;
#endif

    protected bool onHitCalled = false; // To track if the onHit callback has been called
    [SerializeField]
    protected Collider[] hitColliders = new Collider[10]; // Increased size to capture multiple colliders
    protected HashSet<Collider> previousColliders = new HashSet<Collider>(); // Track previous colliders
    Vector3 boxCenter;
    int numHits;

    #endregion

    #region LifeCycle Methods

    private void Start()
    {
        StartCoroutine(CheckOverlapBoxEveryInterval());
    }

    #endregion

    #region Private Methods

    private IEnumerator CheckOverlapBoxEveryInterval()
    {
        while (true)
        {
            // Calculate the position of the box
            boxCenter = transform.position + transform.rotation * offset;

            // Perform the overlap box check with the rotation of the GameObject
            numHits = Physics.OverlapBoxNonAlloc(boxCenter, boxSize / 2, hitColliders, transform.rotation, hitLayers);

            // Detect new hits
            for (int i = 0; i < numHits; i++)
            {
                var collider = hitColliders[i];

                if (collider != null && !previousColliders.Contains(collider))
                {
                    previousColliders.Add(collider);
                    HitCallBack(collider);
                }
            }

            // Remove colliders that are no longer overlapping
            previousColliders.RemoveWhere(collider => !IsStillOverlapping(collider));

            yield return new WaitForSeconds(interval);
        }
    }

    // Check if a collider is still overlapping
    private bool IsStillOverlapping(Collider collider)
    {
        return Physics.OverlapBoxNonAlloc(boxCenter, boxSize / 2, hitColliders, transform.rotation, hitLayers) > 0;
    }

    protected abstract void HitCallBack(Collider collider);

    #endregion

    #region Debugging

    private void OnDrawGizmos()
    {
        // Draw the overlap box in its default color
        Gizmos.color = overLapBoxGizmoColor;
        Vector3 boxCenter = transform.position + transform.rotation * offset;
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, boxSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
    #endregion
}