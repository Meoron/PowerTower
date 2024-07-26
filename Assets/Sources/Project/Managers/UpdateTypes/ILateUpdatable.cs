namespace Sources.Project.Managers.UpdateManager{
	public interface ILateUpdate : IManagedObject{
		public void OnLateUpdate(float deltaTime);
	}
}