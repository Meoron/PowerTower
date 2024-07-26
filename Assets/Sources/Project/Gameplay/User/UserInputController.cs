using Sources.Common;
using Sources.Common.Services.Input;
using Sources.Project.Gameplay.Camera;
using Sources.Project.Gameplay.Objects;
using Sources.Project.Managers.UpdateManager;
using UnityEngine;
using Zenject;

namespace Sources.Project.Gameplay{
	public sealed class UserInputController : IInitializable<SceneContext>, IUpdatable, IDisposable<SceneContext>{
		private const float _mouseSensitive = 5f;
		private bool _changedFrameRate;

		private readonly IUpdateManager _updateManager;
		private readonly IInputService _inputService;

		private IFollowableCamera _followableCamera;
		private IControlable _controlable;
		
		private IRotatable _rotatableComponent;
		private IAttacker _attackComponent;

		private bool _enableThirdPersonView = false;

		public UserInputController(IUpdateManager updateManager, IInputService inputService){
			_updateManager = updateManager;
			_inputService = inputService;
		}

		public void Initialize(){
			_updateManager.Register(this);
		}

		public void Dispose(){
			_updateManager?.Unregister(this);
			
			if (_attackComponent != null){
				_attackComponent.OnAttack -= _followableCamera.ShakeCamera;
			}
			
			SetControlableObject(null);
		}

		public void SetFollowableCamera(IFollowableCamera followableCamera){
			_followableCamera = followableCamera;
		}

		public void SetControlableObject(IControlable controlable){
			_controlable = controlable;
			BlockCursorMouse(_controlable != null);

			if (controlable != null){
				_followableCamera.SetTarget(controlable);
				GetControlableComponets(controlable);
			}
		}

		public void OnUpdate(float deltaTime){
			if (_controlable != null){
				Vector3 viewInputDirection = _mouseSensitive * deltaTime *
											_inputService.GetVector(InputConstants.GameplayMoveCameraView);

				ControlTarget(viewInputDirection, deltaTime);
			}
		}


		
		
		private void BlockCursorMouse(bool enable){
			Cursor.visible = !enable;
			Cursor.lockState = enable ? CursorLockMode.Locked : CursorLockMode.None;
		}

		private void GetControlableComponets(IControlable controlable){
			if (controlable is IRotatable rotatable){
				_rotatableComponent = rotatable;
			}

			if (controlable is IAttacker attacker){
				_attackComponent = attacker;
				_attackComponent.OnAttack += _followableCamera.ShakeCamera;
			}
		}

		private void ControlTarget(Vector3 directionInput, float deltaTime){
			if (_rotatableComponent != null){
				RotateTarget(_rotatableComponent, directionInput, deltaTime);
			}

			if (_attackComponent != null){
				Attack(_attackComponent);
				ChangeForce(_attackComponent);
			}
		}
		
		
		
		
		private void RotateTarget(IRotatable target, Vector3 eulerAngle, float deltaTime){
			target.Rotate(eulerAngle, deltaTime);
		}

		private void Attack(IAttacker attacker){
			if (_inputService.GetButtonDown(InputConstants.GameplayAttack)){
				attacker.Attack();
			}

			if (_inputService.GetButtonUp(InputConstants.GameplayAttack)){
				attacker.StopAttack();
			}
		}

		private void ChangeForce(IAttacker attacker){
			float deltaForce = _inputService.GetAxis(InputConstants.GameplayChangeForce);
			attacker.ChangeForce(deltaForce);
		}
	}
}