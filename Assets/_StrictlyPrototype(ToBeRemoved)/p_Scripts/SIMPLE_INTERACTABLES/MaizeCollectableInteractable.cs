using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine.XR.Interaction.Toolkit;

namespace FARMLIFEVR.SIMPLEINTERACTABLES
{
    public class MaizeCollectableInteractable : BaseSimpleInteractable
    {
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            EventManager.TriggerEvent(EventNames.HarvestReadyMaizeCollected);
            gameObject.SetActive(false);
        }
    }
}
