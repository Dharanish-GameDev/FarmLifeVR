using UnityEngine;
using UnityEngine.Assertions;

namespace FARMLIFEVR.FARMTOOLS
{
	public class Tool_PesticideSprayerGun : MonoBehaviour
	{
		#region Private Variables

		[SerializeField] [Required] private Tool_PesticideOverlapChecker pesticideOverlapChecker;
		[SerializeField] [Required] private ParticleSystem sprayingParticles;

		#endregion

		#region Properties



		#endregion

		#region LifeCycle Methods

		private void Awake()
		{
			ValidateConstraints();
		}
		#endregion

		#region Private Methods

		private void ValidateConstraints()
		{
			Assert.IsNotNull(pesticideOverlapChecker,"Pesticide Overlap Checker is Null !");
		}	

		#endregion

		#region Public Methods

		public void StartSprayPesticide()
		{
			if (gameObject.activeInHierarchy)
			{
				pesticideOverlapChecker.StartCheckingForPlants();
				sprayingParticles.Play();
			}
		}

		public void StopSprayingPest()
		{
			if (gameObject.activeInHierarchy)
			{
				pesticideOverlapChecker.StopCheckingForPlants();
				sprayingParticles.Stop();
			}
		}

		#endregion
	}
}
