using Sources.Common.StateMachine;
using Zenject;

namespace Sources.Project.Factories.Zenject{
	public class StateFactory<Context> : IStateFactory{
		private readonly DiContainer _container;

		public StateFactory(DiContainer container){
			_container = container;
		}

		public T CreateState<T>() where T : IExitableState{
			if (!_container.HasBinding<T>()){
				_container.Bind<T>().AsSingle().NonLazy();	
			}
			
			return _container.Resolve<T>();
		}
	}
}