using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)] // To Execute this class's OnEnable before Anything else
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	#region Private Variables

	[Header("References")]
	[Space(5)]

	[Tooltip("Player's Transform Component")]
	[SerializeField][Required] private Transform playerTransform;

	[Tooltip("Its the Point attached to the player where the dog will when its called")]
	[SerializeField][Required] private Transform petDestinationPoint; 


	#endregion

	#region Properties

	public Transform PlayerTransform => playerTransform;
	public Transform PetDestinationPoint => petDestinationPoint;

    #endregion

    #region LifeCycle Methods

    private void OnEnable()
    {
        EventManager.InitializeEventDictionary();
    }
    private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
        
    }
	private void Start()
	{

	}
	private void Update()
	{

	}
	
	#endregion

	#region Private Methods


	#endregion

	#region Public Methods


	#endregion
}