using FARMLIFEVR.STATEMACHINE;

namespace FARMLIFEVR.CATTLES.DOG
{
    public abstract class DogBaseState : BaseState<DogStateMachine.EDogState>
    {
        protected DogStateContext dogStateContext;

        // Construtor
        public DogBaseState(DogStateContext context, DogStateMachine.EDogState stateKey) : base(stateKey)
        {
            this.dogStateContext = context;
        }
    }
}

