using Sources.Project.Scene.StateMachine;
using Zenject;

namespace SSources.Project.ZenjectInstallers.SceneContext{
	public sealed class SceneStateMachineInstaller : MonoInstaller{
		public override void InstallBindings(){
			//Entry point of scene state machine Zenject.Initialize()
			Container.BindInterfacesAndSelfTo<SceneStateMachine>().AsSingle();  
		}
	}
}