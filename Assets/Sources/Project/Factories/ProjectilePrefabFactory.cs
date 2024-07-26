using Sources.Project.Gameplay;
using Sources.Project.Gameplay.Objects;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.Project.Factories{
	public class ProjectilePrefabFactory<T> : ISimpleFactory<T> where T : IProjectile{
		public T Create(Object prefab, Transform parent){
			var gameObject = GameObject.Instantiate(prefab);
			gameObject.GameObject().transform.SetParent(parent);
			IProjectile projectile = (IProjectile)gameObject;
			var randomMesh = GenerateMesh(projectile);
			projectile.SetMesh(randomMesh);
			return (T)projectile;
		}
		
		private Mesh GenerateMesh(IProjectile projectile){
			return ProjectileMeshGenerator.Generate(projectile.Size,Constants.RANDOM_MESH_GENERATE_OFFSET);
		}
	}
}