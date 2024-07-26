namespace Sources.Common{
	//C is context like a ProjectContext or SceneContext
	public interface IInitializable<C>{
		public void Initialize();
	}
}