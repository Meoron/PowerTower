using Sources.Common.PoolManager;
using UnityEngine;

namespace Sources.Project.Gameplay.Objects{
	public interface IProjectile : IPoolEntity{
		public float Size{ get; }
		public void SetMesh(Mesh mesh);
	}
}