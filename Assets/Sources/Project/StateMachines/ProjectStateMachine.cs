using Sources.Project.Factories.Zenject;
using Sources.Project.Managers.UpdateManager;
using Zenject;

namespace Sources.Project.StateMachine.Zenject{
	public sealed class ProjectStateMachine : Common.StateMachine.StateMachine, IUpdatable, IInitializable{
		private IUpdateManager _updateManager;
		
		public ProjectStateMachine(StateFactory<ProjectContext> stateFactory, IUpdateManager updateManager) : base(stateFactory){
			_updateManager = updateManager;
		}

		//This is entry point. Initialize start after bindings (Zenject interface)
		public void Initialize() { 
			_updateManager.Register(this);
			
			RegisterState<BootstrapProjectState>();
			RegisterState<GameLoopProjectState>();
			
			EnterState<BootstrapProjectState>();
		}

		public void OnUpdate(float deltaTime){
			UpdateStateLogic(deltaTime);
		}
	}
}