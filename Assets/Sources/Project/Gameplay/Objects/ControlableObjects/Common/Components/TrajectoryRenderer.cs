using Sources.Common.AssetManager;
using Sources.Project.Managers.UpdateManager;
using UnityEngine;

namespace Sources.Project.Gameplay.Objects.Components{
	public class TrajectoryRenderer : MonoUpdatable, IUpdatable{
		private readonly int maxSegments = 50;
		private readonly float _timeStep = 0.1f;
		private readonly float _width = 0.1f;
		private readonly bool _enableCollisionDetect = false;
		
		private LineRenderer _lineRenderer;
		private Vector3[] _trajectoryPoints;
		private RaycastHit[] _rayHits;
		private Transform _muzzle;
		private float _force;

		public void Construct(Transform muzzle){
			_muzzle = muzzle;
		}
		
		public void Initialize(){
			_trajectoryPoints = new Vector3[maxSegments];
			_rayHits = new RaycastHit[maxSegments];
			

			InitializeLineRenderer();
		}

		public void SetForce(float newForce){
			_force = newForce;
		}

		private void InitializeLineRenderer(){
			_lineRenderer = gameObject.AddComponent<LineRenderer>();
			_lineRenderer.material = AssetManager.Load<Material>("Materials/TransparentGreen");
			_lineRenderer.endWidth = _width;
			_lineRenderer.startWidth = _width;
		}

		public void OnUpdate(float deltaTime){
			DrawTrajectory(_force);
		}

		private void DrawTrajectory(float force){
			Vector3 startPosition = _muzzle.transform.position;
			Vector3 position = startPosition;
			Vector3 velocity = _muzzle.transform.forward * force;

			_lineRenderer.positionCount = 0;
			int segmentIndex = 0;
			_trajectoryPoints[segmentIndex] = position;

			for (int i = 1; i < maxSegments; i++){
				float t = i * _timeStep;
				Vector3 nextPosition = startPosition + velocity * t + 0.5f * Vector3.down * Constants.GRAVITY * t * t;

				segmentIndex = i;

				if (_enableCollisionDetect && Physics.RaycastNonAlloc(position, (nextPosition - position).normalized, _rayHits,
						(nextPosition - position).magnitude) > 0){
					_trajectoryPoints[segmentIndex] = _rayHits[0].point;
					_lineRenderer.positionCount = segmentIndex + 1;
					_lineRenderer.SetPositions(_trajectoryPoints);
					break;
				}

				_trajectoryPoints[segmentIndex] = nextPosition;
				position = nextPosition;
			}

			_lineRenderer.positionCount = segmentIndex + 1;
			_lineRenderer.SetPositions(_trajectoryPoints);
		}
	}
}