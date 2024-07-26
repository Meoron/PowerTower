namespace Sources.Common.StateMachine{
	public interface IStateFactory{
		public T CreateState<T>() where T : IExitableState;
	}
}