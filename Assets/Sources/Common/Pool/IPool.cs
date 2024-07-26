using System.Threading.Tasks;
using UnityEngine;

namespace Sources.Common.PoolManager{
	public interface IPool<T>{
		public T Spawn(Vector3 position, Quaternion rotation);
	}
}