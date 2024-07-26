using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Common.AssetManager{
	public sealed class AssetManager{
		public static async Task<AsyncOperation> LoadSceneAsync(string sceneName){
			return SceneManager.LoadSceneAsync(sceneName);
		}

		public static async Task<T> LoadPrefabAsync<T>(string path) where T : Object{
			var asset = await LoadAsync<GameObject>(path);
			if (asset != null){
				return asset.GetComponent<T>();
			}

			return null;
		}
		
		public static T LoadPrefab<T>(string path) where T : Object{
			var asset = Load<GameObject>(path);
			if (asset != null){
				return asset.GetComponent<T>();
			}

			return null;
		}

		public static async Task<Sprite> LoadSpriteAsync(string path){
			return await LoadAsync<Sprite>(path);
		}

		public static async Task<AudioClip> LoadAudioAsync(string path){
			return await LoadAsync<AudioClip>(path);
		}

		public static async Task<T> LoadAsync<T>(string path) where T : Object{
			if (string.IsNullOrEmpty(path)) return null;

			var resourceObject = Resources.LoadAsync<T>(path);
			while (!resourceObject.isDone){
				await Task.Yield();
			}

			return (T)resourceObject.asset;
		}
		
		public static T Load<T>(string path) where T : Object{
			if (string.IsNullOrEmpty(path)) return null;

			var resourceObject = Resources.Load<T>(path);
			
			if (resourceObject == null){
				Debug.LogError($"Prefab is missing on path: {path}");
			}
			
			return resourceObject;
		}
		
		public static T CreateInstance<T>(string path) where T : MonoBehaviour{
			var charPrefab = Load<T>(path);
			var charObject = MonoBehaviour.Instantiate(charPrefab);
			return charObject;
		}
	}
}