using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sources.Common;
using Sources.Common.StateMachine;
using Sources.Project.Factories.Zenject;
using Sources.Project.Gameplay;
using Sources.Project.Gameplay.Camera;
using Sources.Project.Gameplay.Objects;
using UnityEngine;
using Zenject;

namespace Sources.Project.Scene.StateMachine{
	public sealed class InitializationSate : IState{
		private readonly SceneStateMachine _sceneStateMachine;
		private ReadOnlyCollection<IInitializable<SceneContext>> _initializableObjects;
		private readonly UserInputController _userInputController;
		private readonly UserCharacterFactory _userCharacterFactory;

		public InitializationSate(SceneStateMachine stateMachine,
								List<IInitializable<SceneContext>> initializableServices,
								UserInputController userInputController,
								UserCharacterFactory userCharacterFactory){
			_sceneStateMachine = stateMachine;
			_userInputController = userInputController;
			_userCharacterFactory = userCharacterFactory;
			_initializableObjects = initializableServices.AsReadOnly();
		}

		public void OnExit(){
		}

		public void OnEnter(){
			InitializeObjects();
			InitializeCharacter();

			_sceneStateMachine.EnterState<GameLoopState>();
		}

		private void InitializeObjects(){
			for (int i = 0; i < _initializableObjects.Count; i++){
				_initializableObjects[i].Initialize();
			}
		}

		private void InitializeCharacter(){
			//TODO: Loading position from scene config and replace this hard code
			var distanceFromCenter = 14f;
			var characterPosition = Vector3.forward * distanceFromCenter;
			var characterRotation = Quaternion.Euler(Vector3.up*180f);

			var characterObject = _userCharacterFactory.CreatePrefab<Tower>("Prefabs/Objects/Tower",
				characterPosition,
				characterRotation, null);
			var camera = CreateInstance<CameraController>("Prefabs/Cameras/FollowableCamera");

			_userInputController.SetFollowableCamera(camera);
			_userInputController.SetControlableObject(characterObject);
		}

		private T CreateInstance<T>(string path) where T : MonoBehaviour{
			var charPrefab = Resources.Load<T>(path);
			var charObject = MonoBehaviour.Instantiate(charPrefab);
			return charObject;
		}
	}
}