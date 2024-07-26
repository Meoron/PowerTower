using System.Collections.Generic;
using Sources.Common.AssetManager;
using Sources.Common.Services.Input;
using Sources.Common.StateMachine;
using Sources.Project.Factories;
using Sources.Project.Managers;
using UnityEngine;
using Zenject;

namespace Sources.Project.Scene.StateMachine{
	public sealed class GameLoopState : IState, IUpdatableState{
		private readonly IInputService _inputService;
		private readonly IStateMachine _sceneStateMachine;
		private readonly DiContainer _diContainer;
		
		
		public GameLoopState(IInputService inputService, SceneStateMachine sceneStateMachine, DiContainer container){
			_inputService = inputService;
			_sceneStateMachine = sceneStateMachine;
			_diContainer = container;
		}
		
		public void OnExit(){
			
		}

		public void OnEnter(){
			InitializeGameManager();
		}

		public void OnUpdate(float deltaTime){
			if (_inputService.GetButtonDown(InputConstants.DebugBindingExit)){
				Application.Quit();
			}
		}

		private void InitializeGameManager(){
			/*var enemyPath = "Prefabs/Enemies/DefaultEnemy";
			var enemyPrefab = AssetManager.Load<DefaultEnemy>(enemyPath);
			var enemyFactory = new ZenjectPrefabFactory<DefaultEnemy>(_diContainer);
			var enemyPool = new PoolWithFactory<DefaultEnemy>(enemyFactory,enemyPrefab, 5);
			var respawnPositions = new List<Vector3>{ Vector3.one };
			_gameManager = new GameManager(enemyPool,respawnPositions);
			
			enemyPool.Initialize();
			_gameManager.Initialize();*/
		}
	}
}