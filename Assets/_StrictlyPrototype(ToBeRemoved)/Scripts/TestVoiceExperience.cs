using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FARMLIFEVR.VOICEEXPERIENCE;

public class TestVoiceExperience : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private VoiceIntentController voiceIntentController;
	int refInt;

    #endregion

    #region Properties



    #endregion

    #region LifeCycle Methods

	private void ModifyNumber(ref int number)
	{
		number += 10;
	}
	private void GetValue(out int numToMod)
	{
		numToMod = 10;
	}

	private void PrintGivenParams(params object[] parameters)
	{
		for (int i = 0; i < parameters.Length; i++)
		{
			if (parameters[i] is int number)
			{
				print("Number : " + number);
			}
            else if (parameters[i] is GameObject obj)
            {
                print("GameObject : " + obj.name);
            }
        }
	}
    private void Awake()
	{

	}
	private void Start()
	{
		//GetValue(out refInt);
		//print("Before Mod : " + refInt);
		//ModifyNumber(ref refInt);
		//print("After Mod : " + refInt);
		//PrintGivenParams(1, voiceIntentController.gameObject);

        //voiceIntentController.AddIntentAction("call_pet",CallingPet);
    }

    private void OnDisable()
    {
        //voiceIntentController.RemoveIntentAction("call_pet", CallingPet);
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
