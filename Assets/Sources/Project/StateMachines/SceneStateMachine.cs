using Sources.Common;
using Sources.Project.Factories.Zenject;
using Sources.Project.Managers.UpdateManager;
using Zenject;

namespace Sources.Project.Scene.StateMachine{
	public sealed class SceneStateMachine : Common.StateMachine.StateMachine, IUpdatable, IInitializable,
											IDisposable<SceneContext>{
		private readonly IUpdateManager _updateManager;
		private readonly UserCharacterFactory _userCharFactory;

		public SceneStateMachine(StateFactory<SceneContext> stateFactory, IUpdateManager updateManager) : base(
			stateFactory){
			_updateManager = updateManager;
		}

		public void Initialize(){
			_updateManager.Register(this);

			RegisterState<InitializationSate>();
			RegisterState<GameLoopState>();

			EnterState<InitializationSate>();
		}

		public void Dispose(){
			_updateManager?.Unregister(this);
		}

		public void OnUpdate(float deltaTime){
			_updatableState?.OnUpdate(deltaTime);
		}
	}
}