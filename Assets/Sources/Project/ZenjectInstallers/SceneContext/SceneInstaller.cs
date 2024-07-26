using Sources.Common.AssetManager;
using Sources.Project.Factories;
using Sources.Project.Factories.Zenject;
using Sources.Project.Gameplay;
using Sources.Project.Gameplay.Objects;
using UnityEngine;
using Zenject;

namespace Sources.Project.ZenjectInstallers.SceneContext{
	public sealed class SceneInstaller : MonoInstaller{
		public override void InstallBindings(){
			Container.BindInterfacesAndSelfTo<UserInputController>().AsSingle();

			BindPools();
			BindFactories();
		}

		private void BindFactories(){
			Container.BindInterfacesAndSelfTo<StateFactory<Zenject.SceneContext>>().AsSingle();
			Container.BindInterfacesAndSelfTo<UserCharacterFactory>().AsSingle();
		}

		private void BindPools(){
			BindProjectilePool();
		}

		private void BindProjectilePool(){
			var projectileFactory = new ProjectilePrefabFactory<Projectile>();
			var prefab = AssetManager.LoadPrefab<Projectile>("Prefabs/Objects/Projectiles/Projectile");
			Container.BindInterfacesAndSelfTo<PoolWithFactory<Projectile>>().AsSingle()
				.WithArguments(projectileFactory, prefab, 5);
		}
	}
}