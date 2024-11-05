using FARMLIFEVR.EVENTSYSTEM;
using FARMLIFEVR.SIMPLEINTERACTABLES;
using UnityEngine;
using UnityEngine.Assertions;

public class XR_Player : MonoBehaviour
{
    #region Private Variables

    public static XR_Player Instance { get; private set; }

    //Exposed to Editor

    [Header("Object References")]
    [Space(3)]
    [SerializeField][Required] private GameObject pesticideSprayerGun;
    [SerializeField][Required] private GameObject pesticideSprayerTank;


    //Hidden From Editor

    private PesticideSprayerInteractable pesticideSprayerInteractable;

    #endregion

    #region Properties



    #endregion

    #region LifeCycle Methods

    private void OnEnable()
    {
        EventManager.StartListening(EventNames.PesticideSprayInteractableSelected, OnPesticideSprayInteractableSelected);
    }

    private void Awake()
    {
        Instance = this;
        ValidateConstraints();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventNames.PesticideSprayInteractableSelected, OnPesticideSprayInteractableSelected);
    }

    #endregion

    #region Private Methods

    private void OnPesticideSprayInteractableSelected(params object[] parameters)
    {
        if (parameters.Length > 0)
        {
            pesticideSprayerInteractable = parameters[0] as PesticideSprayerInteractable;
        }

        if (pesticideSprayerInteractable != null)
        {
            EnableAndDisablePesticideSprayer(true);
            pesticideSprayerInteractable.HasSprayerProvidedToPlayer = true;
        }
    }

    private void ValidateConstraints() // It Validates the Constraints
    {
        Assert.IsNotNull(pesticideSprayerGun, "Pesticide Sprayer Gun is Null !");
        Assert.IsNotNull(pesticideSprayerTank, "Pesticide Sprayer Tank is Null !");
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// This Method Will set the Visiblity of the Pest Sprayer Tank and Gun
    /// </summary>
    /// <param name="value"></param>
    public void EnableAndDisablePesticideSprayer(bool value)
    {
        pesticideSprayerGun.SetActive(value);
        pesticideSprayerTank.SetActive(value);
    }

    #endregion
}
