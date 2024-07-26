using Sources.Project.Gameplay.Objects;
using UnityEngine;
using Zenject;

namespace Sources.Project.Factories.Zenject{
	public class UserCharacterFactory{
		private readonly DiContainer _container;

		public UserCharacterFactory(DiContainer container){
			_container = container;
		}

		public T CreatePrefab<T>(string prefabPath, Vector3 position, Quaternion rotation, Transform transfrom) where T : IControlable{
			var gameObject = _container.InstantiatePrefabResource(prefabPath, position, rotation, transfrom);
			return gameObject.GetComponent<T>();
		}
	}
}