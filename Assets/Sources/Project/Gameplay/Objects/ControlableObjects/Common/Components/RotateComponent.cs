using System.Collections.Generic;
using UnityEngine;

namespace Sources.Project.Gameplay.Objects.Components{
	public sealed class RotateComponent : MonoBehaviour{
		private Transform _rotatableTransform;
		private Vector3 _horizontalViewDirection;
		private Dictionary<Vector3, (float min, float max)> _clampedAxle = new Dictionary<Vector3, (float, float)>();

		public void SetRotatableTransform(Transform rotatableTransform){
			_rotatableTransform = rotatableTransform;
		}

		public void SetClamp(Vector3 axis, float min, float max){
			_clampedAxle[axis] = (min, max);
		}

		public void Rotate(Vector3 directionInput){
			_rotatableTransform = _rotatableTransform != null ? _rotatableTransform : transform;

			_rotatableTransform.localEulerAngles +=
				GetClampedDirection(_rotatableTransform.eulerAngles, directionInput);
		}


		
		
		private Vector3 GetClampedDirection(Vector3 currentRotation, Vector3 direction){
			Vector3 limitedDirection = direction;

			foreach (var axisLimit in _clampedAxle){
				Vector3 axis = axisLimit.Key.normalized;
				float min = axisLimit.Value.min;
				float max = axisLimit.Value.max;
				float currentAxisRotation = Vector3.Dot(currentRotation, axis);
				float directionAxisRotation = Vector3.Dot(limitedDirection, axis);
				float rotationCoeff = currentAxisRotation > 180 ? -360 : 0;
				float clampedAxisRotation =
					Mathf.Clamp(currentAxisRotation+rotationCoeff + directionAxisRotation, min, max);
				float deltaRotation = clampedAxisRotation - currentAxisRotation;

				limitedDirection += (deltaRotation - directionAxisRotation) * axis;
			}

			return limitedDirection;
		}
	}
}