using Cinemachine;
using Sources.Project.Gameplay.Objects;
using DG.Tweening;
using UnityEngine;

namespace Sources.Project.Gameplay.Camera{
	public class CameraController : MonoBehaviour, IFollowableCamera{
		[SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
		[SerializeField] private float _shakeStrength = 0.2f;
		private CinemachineImpulseSource _impulseSource;
		private Sequence _shakeAnimation;
		private IControlable _target;
		public Transform Transform => transform;

		private void Awake(){
			_impulseSource = _cinemachineCamera.GetComponent<CinemachineImpulseSource>();
		}

		public void SetTarget(IControlable target){
			_target = target;
			_cinemachineCamera.GetComponent<CinemachineVirtualCamera>();
			_cinemachineCamera.Follow = target.AnchorTransform;
		}

		public void ShakeCamera(){
			_impulseSource.GenerateImpulseWithForce(_shakeStrength);
		}
	}
}