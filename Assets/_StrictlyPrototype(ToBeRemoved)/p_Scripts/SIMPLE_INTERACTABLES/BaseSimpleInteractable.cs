using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace FARMLIFEVR.SIMPLEINTERACTABLES
{
    public class BaseSimpleInteractable : XRSimpleInteractable
    {
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            Debug.Log($"{this.gameObject.name} is Select Entered!");
        }
    }
}

