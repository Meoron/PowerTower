using UnityEngine;

namespace Sources.Project.Gameplay.Objects{
	public interface IRotatable{
		public void Rotate(Vector3 directionInput, float deltaTime);
	}
}