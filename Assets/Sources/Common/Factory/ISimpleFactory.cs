using UnityEngine;

namespace Sources.Project.Factories{
	public interface ISimpleFactory<T>{
		public T Create(Object prefab, Transform parent);
	}
}