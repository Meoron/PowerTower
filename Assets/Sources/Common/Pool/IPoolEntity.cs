using UnityEngine;

namespace Sources.Common.PoolManager{
	public interface IPoolEntity{
		public bool IsFree{ get; }
		public void Spawn(Vector3 position, Quaternion quaternion);
		public void Despawn();
	}
}