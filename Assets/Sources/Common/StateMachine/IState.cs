namespace Sources.Common.StateMachine{
	public interface IUpdatableState{
		public void OnUpdate(float deltaTime);
	}

	public interface IState : IExitableState{
		public void OnEnter();
	}

	public interface IExitableState{
		public void OnExit();
	}
}