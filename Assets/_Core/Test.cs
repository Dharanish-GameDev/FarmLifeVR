using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private VoiceIntentController voiceIntentController;

    #endregion

    #region Properties



    #endregion

    #region LifeCycle Methods

    private void Awake()
	{

	}
	private void Start()
	{
        voiceIntentController.AddIntentAction("call_pet",CallingPet);
    }
	private void Update()
	{

	}
	
	#endregion

	#region Private Methods

	private void CallingPet()
	{
		Debug.Log("Calling Pet");
	}

	#endregion

	#region Public Methods


	#endregion
}
