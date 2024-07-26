namespace Sources.Common{
	//C - context (Like a SceneContext or ProjectContext)
	public interface IDisposable<C>{
		public void Dispose();
	}
}