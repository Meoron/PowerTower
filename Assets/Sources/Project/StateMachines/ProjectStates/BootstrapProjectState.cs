using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sources.Common;
using Sources.Common.StateMachine;
using Sources.Project.StateMachine.Zenject;
using Zenject;

namespace Sources.Project.StateMachine{
	public sealed class BootstrapProjectState : IState{
		private IStateMachine _projectStateMachine;
		private ReadOnlyCollection<IInitializable<ProjectContext>> _initializableServices;

		public BootstrapProjectState(ProjectStateMachine stateMachine,
									List<IInitializable<ProjectContext>> initializableServices){
			_projectStateMachine = stateMachine;
			_initializableServices = initializableServices.AsReadOnly();
		}

		public void OnEnter(){
			for (int i = 0; i < _initializableServices.Count; i++){
				_initializableServices[i].Initialize();
			}

			_projectStateMachine.EnterState<GameLoopProjectState>();
		}

		public void OnExit(){
		}
	}
}