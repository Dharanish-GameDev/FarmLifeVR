using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CentreOfMass : MonoBehaviour
{
	#region Private Variables

	[SerializeField] private Vector3 centreOfMass;
	private Rigidbody rb;

	#endregion

	#region Properties



	#endregion

	#region LifeCycle Methods

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
    }
	private void Start()
	{
		rb.centerOfMass = centreOfMass;
	}
	private void Update()
	{
#if UNITY_EDITOR
		rb.centerOfMass = centreOfMass;
		rb.WakeUp();
#endif
	}

    #endregion

    #region Private Methods


    #endregion

    #region Public Methods

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position + transform.rotation * rb.centerOfMass, 0.05f);
    }

    #endregion
}
