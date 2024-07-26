using System;
using Sources.Project.Gameplay.Objects.VFX;
using Sources.Project.Gameplay.VFX;
using Sources.Project.Managers.UpdateManager;
using UnityEngine;

namespace Sources.Project.Gameplay.Objects{
	public sealed class Projectile : MonoUpdatable, IUpdatable, IProjectile{
		[SerializeField] private VFXController _explosionVFX;
		[SerializeField] private MeshFilter _meshFilter;
		[SerializeField] private int _maxRicochetCount = 2;
		[SerializeField] private float _detectDistance = 0.1f;
		[SerializeField] private float _maxLifeTime = 3f;
		private RaycastHit[] _hits = new RaycastHit[10];

		private float _time;
		private float _force;
		private Vector3 _lastPosition;
		private Vector3 _velocity;
		private int _ricochetCount;
		
		public bool IsFree => !gameObject.activeSelf;
		public float Size => _detectDistance;

		public void AddForce(float force){
			_velocity = transform.forward * force;
			_force = force;
		}

		public void Spawn(Vector3 position, Quaternion quaternion){
			gameObject.SetActive(true);
			transform.position = position;
			transform.rotation = quaternion;
		}

		public void Despawn(){
			gameObject.SetActive(false);
		}

		public void SetMesh(Mesh mesh){
			_meshFilter.mesh = mesh;
		}

		public void OnUpdate(float deltaTime){
			Gravity(deltaTime);
			MoveWithRicochet(deltaTime);
			TimerToExplode(deltaTime);
		}

		
		
		
		private void Gravity(float deltaTime){
			_velocity += Vector3.down * (Constants.GRAVITY*deltaTime);
		}
		
		private void MoveWithRicochet(float deltaTime){
			var directionMagnitude = _velocity.magnitude*deltaTime;
			var distance = directionMagnitude >= Size ? directionMagnitude+Size : Size;
	
			if (Physics.RaycastNonAlloc(transform.position, _velocity.normalized, _hits, distance) > 0){
				_ricochetCount++;
				
				RaycastHit hit = _hits[0];

				var normal = hit.normal;
				_velocity = ReflectVelocity(normal,_velocity);
				
				if (_ricochetCount >= _maxRicochetCount){
					Explode(normal, hit);
				}
			}
			
			Array.Clear(_hits, 0,_hits.Length);
			transform.position += _velocity*deltaTime;
		}
		
		private void TimerToExplode(float deltatime){
			if (_time >= _maxLifeTime){
				Explode(Vector3.up);
				return;
			}

			_time+=deltatime;
		}
		
		
		
		
		private Vector3 ReflectVelocity(Vector3 normal, Vector3 velocity){
			var reflectDir = Vector3.Reflect(_velocity.normalized, normal);
			var surfaceSpringCoeff = Mathf.Clamp(1-Vector3.Dot(reflectDir, normal),0.5f,1f);
			var ricochetForce = _force;
			var newForce = ricochetForce * (1f - _ricochetCount / (float)_maxRicochetCount);
				
			reflectDir *= surfaceSpringCoeff;
			return velocity - Vector3.Scale(_velocity, normal) + reflectDir * newForce;
		}

		private void Explode(Vector3 normal, RaycastHit hit = new RaycastHit()){
			if (hit.collider !=null && hit.collider.TryGetComponent(out BulletHoleController holeController)){
				holeController.PaintHole(hit);
			}
			
			_explosionVFX.PlayVFX(normal);
			ResetParams();
			Despawn();
		}
		
		private void ResetParams(){
			_time = 0;
			_ricochetCount = 0;
		}
	}
}