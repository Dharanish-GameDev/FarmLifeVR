using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using FARMLIFEVR.STATEMACHINE;
using FARMLIFEVR.EVENTSYSTEM;

namespace FARMLIFEVR.CATTLES.DOG
{
    public class DogStateMachine : StateManager<DogStateMachine.EDogState>
    {
        public enum EDogState
        {
            Idle,
            Walking,
            Petting,
        }

        #region Private Variables

        private DogStateContext dogStateContext;

        [SerializeField][Required] private Animator dogAnimator;

        [SerializeField][Required] private DogOwnerOverLap dogOwnerOverLap;

        #endregion

        #region Public Variables

        #endregion

        #region LifeCycle Methods

        private void Awake()
        {
            ValidateConstraints();
            dogStateContext = new DogStateContext(dogAnimator,dogOwnerOverLap);
            InitializeStates();
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventNames.CallPet, (object[] parameters) => CallPet());
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventNames.CallPet, (object[] parameters) => CallPet());
        }

        #endregion

        #region Properties



        #endregion

        #region Private Methods

        private void InitializeStates()
        {
            States.Add(EDogState.Idle, new DogIdle(dogStateContext,EDogState.Idle));
            CurrentState = States[EDogState.Idle];
        }

        private void ValidateConstraints()
        {
            Assert.IsNotNull(dogAnimator, "Dog's Animator is Null");
            Assert.IsNotNull(dogOwnerOverLap, "Dog Owner OverLap Component is Null");
        }

        private void CallPet()
        {
            Debug.Log("<color=red> My Dog is comming towards Me ! </color>");
        }
        #endregion

        #region Public Methods


        #endregion
    }
}

