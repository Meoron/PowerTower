using Sources.Common.Services.Input;
using Sources.Project.Factories.Zenject;
using Sources.Project.Managers.UpdateManager;
using UnityEngine;
using Zenject;

namespace Sources.Project{
	public sealed class InfrastructureInstaller : MonoInstaller{
		[SerializeField] private Transform _infrastructureTransfrom;
		
		public override void InstallBindings(){
			BindServices();
			BindFactories();
			BindManagers();
		}

		private void BindServices(){
			Container.BindInterfacesAndSelfTo<InputSystemService>().AsSingle().WithArguments(_infrastructureTransfrom).NonLazy();
		}

		private void BindFactories(){
			Container.BindInterfacesAndSelfTo<StateFactory<ProjectContext>>().AsSingle();
		}

		private void BindManagers(){
			var updateManager =
				CreateAndGetMonoBehaviorInstance<UpdateManager>("[UpdateManager]", _infrastructureTransfrom);
			
			Container.Bind<IUpdateManager>().FromInstance(updateManager).AsSingle().NonLazy();
		}
		
		private T CreateAndGetMonoBehaviorInstance<T>(string name,Transform parent) where T : MonoBehaviour{
			var sceneObject = new GameObject(name);
			var component= sceneObject.AddComponent<T>();
		
			sceneObject.transform.SetParent(parent);
		
			return component;
		}
	}
}