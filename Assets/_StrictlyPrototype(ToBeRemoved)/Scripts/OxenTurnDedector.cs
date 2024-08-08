using System;
using System.Collections;
using UnityEngine;

public class OxenTurnDetector : MonoBehaviour
{
    public event Action<OxenBoundary> OnHit;

    #region Private Variables

    public float interval = 0.2f; // Interval in seconds
    public Vector3 boxSize = new Vector3(1f, 1f, 1f); // Size of the box for the overlap check
    public LayerMask hitLayers; // Layers to consider for overlap checks
    public Vector3 offset;

    private bool onHitCalled = false; // To track if the onHit callback has been called
    private Collider[] hitColliders = new Collider[10]; // Array to store hit colliders

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
            Vector3 boxCenter = transform.position + transform.rotation * offset;

            // Perform the overlap box check with the rotation of the GameObject
            int numHits = Physics.OverlapBoxNonAlloc(boxCenter, boxSize / 2, hitColliders, transform.rotation, hitLayers);

            if (numHits > 0)
            {
                if (!onHitCalled)
                {
                    OnHit?.Invoke(hitColliders[0].TryGetComponent(out OxenBoundary boundary) ? boundary : null); // Invoke the OnHit event with the first hit collider
                    onHitCalled = true;
                }
            }
            else
            {
                onHitCalled = false;
            }

            // Wait for the specified interval before checking again
            yield return new WaitForSeconds(interval);
        }
    }

    #endregion

    #region Debugging

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw the overlap box
        Vector3 boxCenter = transform.position + transform.rotation * offset;
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, boxSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        if (onHitCalled)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }

    #endregion
}
