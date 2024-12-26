using System;
using System.Collections;
using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;
using UnityEngine.Events;

public class SkyManager : MonoBehaviour
{
    public enum GameTimeState
    {
        Morning,
        Day,
        Evening,
        Night
    }

    public GameTimeState CurrentState { get; private set; }

    [SerializeField] private Material skyboxMaterial;

    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxMorning;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxEvening;

    [SerializeField] private float nightToMorningTime = 5f; // Lerp time for Night -> Morning
    [SerializeField] private float morningToDayTime = 10f; // Lerp time for Morning -> Day
    [SerializeField] private float dayToEveningTime = 8f; // Lerp time for Day -> Evening
    [SerializeField] private float eveningToNightTime = 6f; // Lerp time for Evening -> Night

    [SerializeField] private float nightDuration = 20f; // Duration before Night transitions to Morning
    
    private const string TEXTURE1 = "_Texture1";
    private const string TEXTURE2 = "_Texture2";
    private const string BLEND = "_Blend";

    private Coroutine autoTransitionCoroutine;
    
    private bool halfMorningEventTriggered = false;


    private void Awake()
    {
        InitializeMorningState(); // Ensure the game starts with Morning
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventNames.MakeItDay,MakeItDay);
        EventManager.StartListening(EventNames.MakeItEvening,MakeItEvening);
        EventManager.StartListening(EventNames.MakeItNight,MakeItNight);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventNames.MakeItDay,MakeItDay);
        EventManager.StopListening(EventNames.MakeItEvening,MakeItEvening);
        EventManager.StopListening(EventNames.MakeItNight,MakeItNight);
        ResetSkybox();
    }

    private void InitializeMorningState()
    {
        CurrentState = GameTimeState.Morning;
    }

    public void SetTimeState(GameTimeState newState)
    {
        StopAllCoroutines(); // Stop any ongoing transitions
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameTimeState.Morning:
                StartCoroutine(LerpSkybox(skyboxNight, skyboxMorning, nightToMorningTime));
                break;

            case GameTimeState.Day:
                StartCoroutine(LerpSkybox(skyboxMorning, skyboxDay, morningToDayTime));
                break;

            case GameTimeState.Evening:
                StartCoroutine(LerpSkybox(skyboxDay, skyboxEvening, dayToEveningTime));
                break;

            case GameTimeState.Night:
                StartCoroutine(LerpSkybox(skyboxEvening, skyboxNight, eveningToNightTime));
                autoTransitionCoroutine = StartCoroutine(AutoTransitionToMorning());
                break;
        }
    }

    private IEnumerator LerpSkybox(Texture2D from, Texture2D to, float time)
    {
        skyboxMaterial.SetTexture(TEXTURE1, from);
        skyboxMaterial.SetTexture(TEXTURE2, to);
        skyboxMaterial.SetFloat(BLEND, 0);

        for (float t = 0; t <= time; t += Time.deltaTime)
        {
            float blend = t / time;
            skyboxMaterial.SetFloat(BLEND, blend);

            // Trigger the event when Morning blend reaches 50%
            if (from == skyboxNight && to == skyboxMorning && blend >= 0.5f && !halfMorningEventTriggered)
            {
                EventManager.TriggerEvent(EventNames.AdvanceToNextMission);
                halfMorningEventTriggered = true; // Ensure it triggers only once
            }
            yield return null;
        }

        // Ensure the final state
        skyboxMaterial.SetTexture(TEXTURE1, to);
        skyboxMaterial.SetFloat(BLEND, 0);

        // Reset the bool after the lerping is complete
        halfMorningEventTriggered = false;
    }


    private IEnumerator AutoTransitionToMorning()
    {
        // Wait for the night duration before transitioning to Morning
        yield return new WaitForSeconds(nightDuration);
        SetTimeState(GameTimeState.Morning);
    }

    private void ResetSkybox()
    {
        // Reset the skybox to the default night state
        skyboxMaterial.SetTexture(TEXTURE1, skyboxMorning);
        skyboxMaterial.SetTexture(TEXTURE2, skyboxDay);
        skyboxMaterial.SetFloat(BLEND, 0);

        if (autoTransitionCoroutine != null)
        {
            StopCoroutine(autoTransitionCoroutine);
            autoTransitionCoroutine = null;
        }
    }

    public void MakeItDay()
    {
       // Debug.Log("Make It Day!");
        if (CurrentState == GameTimeState.Morning)
        {
            SetTimeState(GameTimeState.Day);
        }
    }

    public void MakeItNight()
    {
        if (CurrentState == GameTimeState.Evening)
        {
            SetTimeState(GameTimeState.Night);
            print("Switching Night");
        }
    }

    public void MakeItEvening()
    {
        if (CurrentState == GameTimeState.Day)
        {
            SetTimeState(GameTimeState.Evening);
        }
    }
}
