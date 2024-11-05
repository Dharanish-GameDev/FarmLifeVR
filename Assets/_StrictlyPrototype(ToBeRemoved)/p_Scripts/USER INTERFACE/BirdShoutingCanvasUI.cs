using FARMLIFEVR.EVENTSYSTEM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace FARMLIFEVR.USERINTERFACE
{
	public class BirdShoutingCanvasUI : MonoBehaviour
	{
		#region Private Variables

		[SerializeField][Required] private Image fillImage;
		[SerializeField][Required] private TextMeshProUGUI shoutCountText;
		[SerializeField] private int maxShoutCount = 7;

		private int currentShoutCount = 0;

		#endregion

		#region Properties

		public int CurrentShoutCount
		{
			get { return currentShoutCount; }

			set
			{
				currentShoutCount = value;
				OnShoutCountChanged(currentShoutCount);
			}
		}

		public bool CanUpdateShoutCount => currentShoutCount < maxShoutCount;

		#endregion

		#region Private Methods

		private void OnShoutCountChanged(int shoutCount)
		{
			shoutCountText.SetText("Shout : " + shoutCount.ToString() + " / " + maxShoutCount);
			if (shoutCount == maxShoutCount)
			{
				EventManager.TriggerEvent(EventNames.MaxShoutCountReached);
			}
		}

		#endregion

		#region Public Methods

		public void SetFillAmount(float amount)
		{
			fillImage.fillAmount = amount;
		}

		public void ResetFill()
		{
			fillImage.fillAmount = 0;
		}

		public void ResetCanvas()
		{
			ResetFill();
			CurrentShoutCount = 0;
		}

		public void ShowCanvas()
		{
			gameObject.SetActive(true);
		}
		public void HideCanvas()
		{
			gameObject.SetActive(false);
		}

		#endregion
	}
}
