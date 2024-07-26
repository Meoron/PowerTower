using System;
using System.Text;
using Sources.Common.PoolManager;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Sources.Project.Gameplay.Objects.Components{
	public sealed class GunComponent: MonoBehaviour{
		[SerializeField] private Transform _muzzle;
		[SerializeField] private Transform _gunBarrel;
		[SerializeField] private TextMeshPro _forceText;
		[SerializeField] private ParticleSystem _shootVFX;
		[SerializeField] private float _maxFoce=50;
		[SerializeField] private float _minFoce=10;
		[SerializeField] private float _forceBack=0.3f;
		[SerializeField] private float _durationAnimation=0.3f;
		
		private TrajectoryRenderer _trajectoryRenderer;
		private IPool<Projectile> _projectilePool;
		private float _force;
		
		private Vector3 _startGunPosition;
		private Sequence _shotAnimation;
		
		public event Action OnSpawnProjectile;
		
		public void Construct(IPool<Projectile> projectilePool){
			_projectilePool = projectilePool;
			_trajectoryRenderer = gameObject.AddComponent<TrajectoryRenderer>();
			
			_trajectoryRenderer.Construct(_muzzle);
		}

		public void Initialize(){
			_force = _minFoce;
			_startGunPosition = _gunBarrel.localPosition;
			
			_trajectoryRenderer.Initialize();
			_trajectoryRenderer.SetForce(_force);
		}
		
		public void ChangeForce(float deltaForce){
			_force = Mathf.Clamp(_force+deltaForce,_minFoce,_maxFoce);
			_trajectoryRenderer.SetForce(_force);

			StringBuilder sb = new StringBuilder($"{_force}");
			_forceText.text = sb.ToString();
		}
		
		public void Attack(){
			var projectile = _projectilePool.Spawn(_muzzle.position, _muzzle.rotation);
			projectile.AddForce(_force);
			PlayShotAnimation();
			
			OnSpawnProjectile?.Invoke();
		}

		public void StopAttack(){
			
		}

		private void PlayShotAnimation(){
			if (_shotAnimation != null){
				_shotAnimation.Kill();
			}

			var firstPartduration = _durationAnimation*0.03f;
			var secondPartduration = _durationAnimation*0.97f;
			
			_shotAnimation = DOTween.Sequence();
			_shotAnimation.Append(_gunBarrel.DOLocalMove(_startGunPosition+Vector3.back*_forceBack, firstPartduration)).SetEase(Ease.Linear);
			_shotAnimation.Append(_gunBarrel.DOLocalMove(_startGunPosition, secondPartduration));

			_shootVFX.Play();
			_shotAnimation.Play();
		}
	}
}