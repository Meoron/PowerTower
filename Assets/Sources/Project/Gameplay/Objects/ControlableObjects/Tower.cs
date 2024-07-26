using System;
using Sources.Common.PoolManager;
using Sources.Project.Gameplay.Objects.Components;
using UnityEngine;
using Zenject;

namespace Sources.Project.Gameplay.Objects{
	[RequireComponent(typeof(RotateComponent))]
	[RequireComponent(typeof(GunComponent))]
	public sealed class Tower : MonoBehaviour, IControlable, IRotatable, IAttacker{
		[SerializeField] private Transform _upperPart;
		[SerializeField] private Transform _visorTransform;
		
		private RotateComponent _rotateComponent;
		private GunComponent _gunComponent;

		public event Action OnAttack;

		public Transform AnchorTransform => _visorTransform;

		[Inject]
		private void Construct(IPool<Projectile> poolProjectile){
			_rotateComponent = GetComponent<RotateComponent>();
			_gunComponent = GetComponent<GunComponent>();

			_gunComponent.Construct(poolProjectile);
		}
		
		private void Awake(){
			_rotateComponent.SetRotatableTransform(_upperPart);
			_rotateComponent.SetClamp(Vector3.right, -65,50);
			_gunComponent.Initialize();
			
			_gunComponent.OnSpawnProjectile += OnSpawnProjectile;
		}

		private void OnDestroy(){
			_gunComponent.OnSpawnProjectile -= OnSpawnProjectile;
		}

		public void Rotate(Vector3 directionInput, float deltaTime){
			directionInput = Vector3.up * directionInput.x + Vector3.right * directionInput.y;
			_rotateComponent.Rotate(directionInput);
		}

		public void Attack(){
			_gunComponent.Attack();
		}

		public void StopAttack(){
			_gunComponent.StopAttack();
		}

		public void ChangeForce(float deltaForce){
			_gunComponent.ChangeForce(deltaForce);
		}

		private void OnSpawnProjectile(){
			OnAttack?.Invoke();
		}
	}
}