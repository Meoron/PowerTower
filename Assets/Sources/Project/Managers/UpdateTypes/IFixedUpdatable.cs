namespace Sources.Project.Managers.UpdateManager{
	public interface IFixedUpdatable : IManagedObject{
		public void OnFixedUpdate(float fixedDeltaTime);
	}
}