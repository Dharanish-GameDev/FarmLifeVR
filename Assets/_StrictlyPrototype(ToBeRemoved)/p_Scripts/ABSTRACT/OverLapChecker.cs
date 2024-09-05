using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OverLapChecker : MonoBehaviour
{
    public enum E_OverLapCheckingType
    {
        OnlyWhenCalled,
        PerInterval
    }

    public enum E_OverLapShape
    {
        Box,
        Sphere,
        Capsule
    }

    #region Private Variables

    [Header("Overlap Checking Properties")]
    [Space(5)]

    [Tooltip("It Determines how often we check the Overlap")]
    [SerializeField] private E_OverLapCheckingType overLapCheckingType;

    [Tooltip("Its the Shape of the Overlap checker")]
    [SerializeField] private E_OverLapShape overLapShape;

    [Header("Values")]
    [Space(5)]
    [Tooltip("Time Interval Before another Overlap Check")]
    [ConditionalField("overLapCheckingType",E_OverLapCheckingType.PerInterval)]
    [SerializeField] private float interval = 0.2f; // Interval in seconds

    [ConditionalField("overLapShape", E_OverLapShape.Box)]
    [SerializeField] private Vector3 boxSize = new Vector3(1f, 1f, 1f); // Size of the box for the overlap check

    [ConditionalField("overLapShape", E_OverLapShape.Sphere)]
    [SerializeField] private float sphereRadius = 1f; // Radius of the sphere for overlap check

    [ConditionalField("overLapShape", E_OverLapShape.Capsule)]
    [SerializeField] private float capsuleRadius = 1f; // Radius of the capsule for overlap check
    [ConditionalField("overLapShape", E_OverLapShape.Capsule)]
    [SerializeField] private float capsuleHeight = 2f; // Height of the capsule for overlap check

    [SerializeField] private LayerMask hitLayers; // Layers to consider for overlap checks

    [Tooltip("Position Offset that will be added to the Current Position for checking Overlap")]
    [SerializeField] private Vector3 offset;

#if UNITY_EDITOR
    [Space(5)]
    [Header("For Debugging")]
    [SerializeField] private Color overLapGizmoColor;
#endif

    protected bool onHitCalled = false; // To track if the onHit callback has been called
    [SerializeField] // For Viewing Purpose only
    protected Collider[] hitColliders = new Collider[10]; // Increased size to capture multiple colliders
    protected HashSet<Collider> previousColliders = new HashSet<Collider>(); // Track previous colliders
    Vector3 center;
    Vector3 capsulePoint1;
    Vector3 capsulePoint2;
    int numHits;

    #endregion

    #region LifeCycle Methods

    private void Start()
    {
        if (overLapCheckingType == E_OverLapCheckingType.PerInterval)
            StartCoroutine(CheckOverlapEveryInterval());
    }

    #endregion

    #region Private Methods

    private IEnumerator CheckOverlapEveryInterval()
    {
        while (true)
        {
            PerformOverlapCheck();
            yield return new WaitForSeconds(interval);
        }
    }

    private void PerformOverlapCheck()
    {
        // Calculate the center position with the offset
        center = transform.position + transform.rotation * offset;
        numHits = PerformShapeOverlapCheck(center);

        // Detect new hits
        ProcessDetectedColliders();
    }

    private int PerformShapeOverlapCheck(Vector3 center)
    {
        switch (overLapShape)
        {
            case E_OverLapShape.Box:
                return Physics.OverlapBoxNonAlloc(center, boxSize / 2, hitColliders, transform.rotation, hitLayers);

            case E_OverLapShape.Sphere:
                return Physics.OverlapSphereNonAlloc(center, sphereRadius, hitColliders, hitLayers);

            case E_OverLapShape.Capsule:
                capsulePoint1 = center + transform.up * (capsuleHeight / 2 - capsuleRadius);
                capsulePoint2 = center - transform.up * (capsuleHeight / 2 - capsuleRadius);
                return Physics.OverlapCapsuleNonAlloc(capsulePoint1, capsulePoint2, capsuleRadius, hitColliders, hitLayers);

            default:
                Debug.LogWarning("Unsupported overlap shape.");
                return 0;
        }
    }

    // It Runs the HitCallback for every collider in hitColliders
    private void ProcessDetectedColliders()
    {
        for (int i = 0; i < numHits; i++)
        {
            var collider = hitColliders[i];
            if (collider != null)
            {
                HitCallBack(collider);
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Perform an overlap check for a single frame.
    /// </summary>
    public void CheckOverlapOnce()
    {
        if (overLapCheckingType == E_OverLapCheckingType.OnlyWhenCalled)
        {
            PerformOverlapCheck();
        }
    }
    /// <summary>
    /// This method check if its overlapping anything and return the bool
    /// </summary>
    /// <returns></returns>
    public bool GetIsOverlapping()
    {
        center = transform.position + transform.rotation * offset;
        return PerformShapeOverlapCheck(center) > 0 ? true : false;
    }

    #endregion

    #region Abstract Methods

    /// <summary>
    /// This method must be implemented in derived classes to define the behavior when a hit is detected.
    /// </summary>
    protected abstract void HitCallBack(Collider collider);

    #endregion

    #region Debugging

    private void OnDrawGizmos()
    {
        Gizmos.color = overLapGizmoColor;
        Vector3 center = transform.position + transform.rotation * offset;

        switch (overLapShape)
        {
            case E_OverLapShape.Box:
                Gizmos.matrix = Matrix4x4.TRS(center, transform.rotation, boxSize);
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                break;

            case E_OverLapShape.Sphere:
                Gizmos.DrawWireSphere(center, sphereRadius);
                break;

            case E_OverLapShape.Capsule:
                Vector3 point1 = center + transform.up * (capsuleHeight / 2 - capsuleRadius);
                Vector3 point2 = center - transform.up * (capsuleHeight / 2 - capsuleRadius);
                Gizmos.DrawWireSphere(point1, capsuleRadius);
                Gizmos.DrawWireSphere(point2, capsuleRadius);
                Gizmos.DrawLine(point1 + transform.forward * capsuleRadius, point2 + transform.forward * capsuleRadius);
                Gizmos.DrawLine(point1 - transform.forward * capsuleRadius, point2 - transform.forward * capsuleRadius);
                Gizmos.DrawLine(point1 + transform.right * capsuleRadius, point2 + transform.right * capsuleRadius);
                Gizmos.DrawLine(point1 - transform.right * capsuleRadius, point2 - transform.right * capsuleRadius);
                break;
        }
    }

    #endregion
}
