using Zenject;

namespace Sources.Project.StateMachine.Zenject{
	public sealed class ProjectStateMachineInstaller : MonoInstaller{
		public override void InstallBindings(){
			Container.BindInterfacesAndSelfTo<ProjectStateMachine>().AsSingle();  //(Entry point) Initialized after bindings
		}
	}   
}