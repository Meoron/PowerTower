using System.Collections.Generic;
using System.Threading.Tasks;
using Sources.Common;
using Sources.Common.PoolManager;
using Sources.Project.Factories;
using UnityEngine;
using Zenject;

namespace Sources.Project.Gameplay{
	public class PoolWithFactory<T> : IPool<T>, IInitializable<SceneContext> where T : IPoolEntity{
		private Transform _container;
		private Object _objectPrefab;
		private int _startCount;
		private List<IPoolEntity> _objects = new List<IPoolEntity>();
		
		private readonly ISimpleFactory<T> _factory;
		
		public PoolWithFactory(ISimpleFactory<T> factory, Object objectPrefab, int count){
			_factory = factory;
			_objectPrefab = objectPrefab;
			_startCount = count;
		}

		public void Initialize(){
			_container = new GameObject($"[PoolVFX {typeof(T).Name}]").transform;
			
			for (int i = 0; i < _startCount; i++){
				Create();
			}
		}

		private T Create(){
			var createdObject = _factory.Create(_objectPrefab, _container);
			createdObject.Despawn();
			_objects.Add(createdObject);

			return createdObject;
		}

		public T Spawn(Vector3 position, Quaternion rotation){
			IPoolEntity spawnedObject = null;
			
			for (int i = 0; i < _objects.Count; i++){
				if (_objects[i].IsFree){
					spawnedObject = _objects[i];
				}
			}

			if (spawnedObject == null){
				spawnedObject = Create();
			}

			spawnedObject.Spawn(position, rotation);
			return (T)spawnedObject;
		}
	}
}