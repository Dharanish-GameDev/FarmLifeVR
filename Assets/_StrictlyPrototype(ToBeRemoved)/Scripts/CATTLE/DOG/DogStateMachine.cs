using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using FARMLIFEVR.STATEMACHINE;
using FARMLIFEVR.EVENTSYSTEM;
using QFSW.QC;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogStateMachine : StateManager<DogStateMachine.EDogState>
    {
        public enum EDogState
        {
            Idle,
            RunningTowardsOwner,
            EmoteState
        }

        #region Private Variables

        // Exposed
        [Header("References")]
        [Space(5)]

        [Tooltip("Its the Dog's Animator Component in its Mesh")]
        [SerializeField][Required] private Animator dogAnimator;

        [Tooltip("Its the Overlapping Checking Component with the Player")]
        [SerializeField][Required] private DogOwnerOverLap dogOwnerOverLap;

        [Tooltip("Its the GameObject that will appear infront of the Dog while Eating")]
        [SerializeField][Required] private GameObject dogFoodGameObj;



        [Space(10)]
        [Header("Values")]
        [Space(5)]

        [Tooltip("Its the Value that controls how fast that the dog is going to move")]
        [Range(0,10)] [SerializeField] private float moveSpeed;

        [Tooltip("Its the Value that controls how fast that the dog is going to rotate towards the target")]
        [Range(0, 360)] [SerializeField] private float rotationSpeed;

        // Hidden 
        private DogStateContext dogStateContext;

        #endregion

        #region Public Variables

        #endregion

        #region Properties

        public DogIdle DogIdle => (DogIdle)States[EDogState.Idle];
        public bool isPlayerWithinRange => dogOwnerOverLap.GetIsOverlapping();

        #endregion

        #region LifeCycle Methods

        private void Awake()
        {
            ValidateConstraints();
            dogStateContext = new DogStateContext(this,dogAnimator,dogOwnerOverLap,moveSpeed,rotationSpeed,dogFoodGameObj);
            InitializeStates();
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventNames.CallPet,CallPet);
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventNames.CallPet,CallPet);
        }

        #endregion

        

        #region Private Methods

        private void InitializeStates()
        {
            States.Add(EDogState.Idle, new DogIdle(dogStateContext,EDogState.Idle));
            States.Add(EDogState.RunningTowardsOwner, new DogRunningTowardsOwner(dogStateContext, EDogState.RunningTowardsOwner));
            States.Add(EDogState.EmoteState, new DogEmoteState(dogStateContext,EDogState.EmoteState));
            CurrentState = States[EDogState.Idle];
        }

        private void ValidateConstraints()
        {
            Assert.IsNotNull(dogAnimator, "Dog's Animator is Null");
            Assert.IsNotNull(dogOwnerOverLap, "Dog Owner OverLap Component is Null");
            Assert.IsNotNull(dogFoodGameObj, "Dog Owner OverLap Component is Null");
        }


        [Command]
        public void CallPet()
        {
            Debug.Log($"<color=#83F458> {EventNames.CallPet} intent found in the response :) </color>");
            if (dogStateContext.DogOwnerOverLap.GetIsOverlapping()) return;
            if (CurrentState == States[EDogState.RunningTowardsOwner]) return;
            SwitchState(EDogState.RunningTowardsOwner);
        }
        #endregion

        #region Public Methods


        #endregion
    }
}

