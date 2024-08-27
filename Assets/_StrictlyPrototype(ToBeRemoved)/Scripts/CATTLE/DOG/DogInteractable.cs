using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class DogInteractable : XRSimpleInteractable
{
    [Space(10)]
    [Header("References")]
    [Space(5)]
    [SerializeField][Required] private DogEmoteManager dogEmoteManager;



    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        dogEmoteManager.ToggleEmoteWheelUI(); // Toggle Activity of Emote Wheel
    }
}
