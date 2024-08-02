using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ControllerInputManager : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private List<ControllerButton> controllerButtonList = new List<ControllerButton>();

    #endregion

    #region Properties



    #endregion

    #region LifeCycle Methods

    private void Awake()
    {
        foreach (ControllerButton button in controllerButtonList)
        {
            button.inputAction.started += ctx => ButtonPressed(button, ctx);
            button.inputAction.canceled += ctx => ButtonReleased(button, ctx);
        }
    }
    private void OnDestroy()
    {
        foreach (ControllerButton button in controllerButtonList)
        {
            button.inputAction.started -= ctx => ButtonPressed(button, ctx);
            button.inputAction.canceled -= ctx => ButtonReleased(button, ctx);
        }
    }

    private void OnEnable()
    {
        foreach (ControllerButton button in controllerButtonList)
        {
            button.inputAction.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (ControllerButton button in controllerButtonList)
        {
            button.inputAction.Disable();
        }
    }

    #endregion

    #region Private Methods

    private void ButtonPressed(ControllerButton controllerButton, InputAction.CallbackContext context)
    {
        controllerButton.onPress.Invoke();
        controllerButton.IsHolding = true;
        InvokeRepeating(nameof(Hold), 0.0f, Time.deltaTime);
    }

    private void ButtonReleased(ControllerButton controllerButton, InputAction.CallbackContext context)
    {
        controllerButton.onRelease.Invoke();
        controllerButton.IsHolding = false;
        CancelInvoke(nameof(Hold));
    }


    private void Hold()
    {
        foreach (ControllerButton button in controllerButtonList)
        {
            if (button.IsHolding)
            {
                button.onHolding.Invoke();
            }
        }
    }
    #endregion

    #region Public Methods


    #endregion
}

[System.Serializable]
public class ControllerButton
{
    [Header("View Purpose Only")]
    [Space(5)]
    public string actionViewName = null;
    public string buttonRefName = null;
    [Space(30)]

    public InputAction inputAction = null;
    [Space(15)]
    [Header("Button Events")]
    public UnityEvent onPress = new UnityEvent();
    public UnityEvent onRelease = new UnityEvent();
    public UnityEvent onHolding = new UnityEvent();

    private bool isHolding = false;

    public bool IsHolding { get { return isHolding; } set { isHolding = value; } }
}
