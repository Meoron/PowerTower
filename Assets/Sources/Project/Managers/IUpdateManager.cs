namespace Sources.Project.Managers.UpdateManager{
	public interface IUpdateManager{
		public void Register(IManagedObject updatableObject);
		public void Unregister(IManagedObject monoUpdatable);
		public void Clear();
	}
}