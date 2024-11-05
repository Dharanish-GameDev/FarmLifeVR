using FARMLIFEVR.EVENTSYSTEM;
using System;
using UnityEngine.XR.Interaction.Toolkit;

namespace FARMLIFEVR.SIMPLEINTERACTABLES
{
	public class PesticideSprayerInteractable : BaseSimpleInteractable
	{
        private bool hasSprayerProvidedToPlayer = false;

        public bool HasSprayerProvidedToPlayer
        {
            get
            {
                return hasSprayerProvidedToPlayer;
            }
            set
            {
                hasSprayerProvidedToPlayer = value;
                OnHasSprayerProvidedToPlayerChanged(value);
            }
        }

        private void OnHasSprayerProvidedToPlayerChanged(bool value)
        {
            if(value)
            {
                gameObject.SetActive(false);
            }
        }

        #region Overriden Methods

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            EventManager.TriggerEvent(EventNames.PesticideSprayInteractableSelected, this);
        }

        public void ShowPesticideSprayInteractable()
        {
            gameObject.SetActive(true);
        }
        
        public void HidePesticideSprayInteractable()
        {
            gameObject.SetActive(false);
            XR_Player.Instance.EnableAndDisablePesticideSprayer(false); // Disabling the Visual which is attached to the Player as well
        }


        #endregion
    }
}
