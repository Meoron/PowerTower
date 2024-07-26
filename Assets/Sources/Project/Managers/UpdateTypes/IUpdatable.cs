namespace Sources.Project.Managers.UpdateManager{
	public interface IUpdatable : IManagedObject{
		public void OnUpdate(float deltaTime);
	}
}