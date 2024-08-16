using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using FARMLIFEVR.STATEMACHINE;

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

        #endregion

        #region Public Variables

        #endregion

        #region LifeCycle Methods

        private void Awake()
        {
            ValidateConstraints();
            dogStateContext = new DogStateContext(dogAnimator);
            InitializeStates();
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
        }

        #endregion

        #region Public Methods


        #endregion
    }
}

