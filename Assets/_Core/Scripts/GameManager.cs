using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using FARMLIFEVR.EVENTSYSTEM;
using FARMLIFEVR.LAND;
using FARMLIFEVR.CROPS.MAIZE;


[DefaultExecutionOrder(-1)] // To Execute this class's OnEnable before Anything else
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	#region Private Variables

	[Header("Player Related")]
	[Space(3)]

	[Tooltip("Player's Transform Component")]
	[SerializeField][Required] private Transform playerTransform;

	[Tooltip("Its the Point attached to the player where the dog will when its called")]
	[SerializeField][Required] private Transform petDestinationPoint;

	[Space(5)]

	[Header("Script Refs")]
	[Space(3)]

	[Tooltip("Its the Script that controls watering in the Games")]
	[SerializeField] [Required] private IrrigationManager irrigationManager;

	[Tooltip("Its the Script that handles all the Maize behaviour in the Game")]
	[SerializeField] [Required] private MaizeFeildStateMachine maizeFeildStateMachine;


	#endregion

	#region Properties

	public Transform PlayerTransform => playerTransform;
	public Transform PetDestinationPoint => petDestinationPoint;
	public IrrigationManager IrrigationManager => irrigationManager;
	public MaizeFeildStateMachine MaizeFeildStateMachine => maizeFeildStateMachine;

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

	private void ValidateConstraints()
	{
		Assert.IsNotNull(playerTransform, "The Player Transform is Null!");
		Assert.IsNotNull(PetDestinationPoint, "Pet Destination Point is Null");
	}

	#endregion

	#region Public Methods


	#endregion
}
