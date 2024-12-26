using System;
using UnityEngine;
using DG.Tweening;
using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine.Serialization; // Ensure you have DoTween installed in your project

public class FadeCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float nightDuration = 1f;
    [SerializeField] private float quickFadeDuration = 0.5f;
    [SerializeField] private float teleportationFadeDuration = 0.5f;

    private void Awake()
    {
        // Initialize the CanvasGroup alpha to fully transparent or opaque based on your need
        canvasGroup.alpha = 1f; // Start with it fully transparent if you want to fade in initially

        // Call the FadeOut method in Awake
        FadeOut();
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventNames.BEGIN_NightFade,NightFadeInAndOut);
        EventManager.StartListening(EventNames.BEGIN_QuickFade,QuickFadeInAndOut);
        EventManager.StartListening(EventNames.BEGIN_TeleFade,TeleportationFadeInAndOut);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventNames.BEGIN_NightFade,NightFadeInAndOut);
        EventManager.StopListening(EventNames.BEGIN_QuickFade,QuickFadeInAndOut);
        EventManager.StopListening(EventNames.BEGIN_TeleFade,TeleportationFadeInAndOut);
    }

    private void NightFadeInAndOut()
    {
        FadeInAndOut(nightDuration);
    }

    private void QuickFadeInAndOut()
    {
        FadeInAndOut(quickFadeDuration);
    }

    private void TeleportationFadeInAndOut()
    {
        FadeInAndOut(teleportationFadeDuration);
    }

    private void FadeInAndOut(float duration)
    {
        canvasGroup.DOFade(1, duration).OnComplete(() =>
        {
            // Automatically start fading out after fade-in completes
            canvasGroup.DOFade(0, duration);
        });
    }

    private void FadeOut()
    {
        // Immediately fade out the CanvasGroup
        canvasGroup.DOFade(0, nightDuration);
    }
}