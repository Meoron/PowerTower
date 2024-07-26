using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Sources.Project.Gameplay.Objects.VFX{
	public class VFXController : MonoBehaviour{
		[SerializeField] private ParticleSystem _explosionVFX;
		private CancellationTokenSource _cancellationTokenSource;
		
		private void Awake(){
			_explosionVFX.gameObject.SetActive(false);
			_cancellationTokenSource = new CancellationTokenSource();
		}

		private void OnDestroy(){
			_cancellationTokenSource.Cancel();
		}

		public async void PlayVFX(Vector3 normal){
			var token = _cancellationTokenSource.Token;
			
			_explosionVFX.transform.SetParent(null);
			_explosionVFX.transform.position = transform.position;
			_explosionVFX.transform.rotation = Quaternion.LookRotation(normal, _explosionVFX.transform.up);
			_explosionVFX.gameObject.SetActive(true);
			_explosionVFX.Play();
			
			while (!token.IsCancellationRequested && _explosionVFX.gameObject.activeSelf){
				await Task.Yield();
			}

			if (!token.IsCancellationRequested){
				_explosionVFX.transform.SetParent(transform);
				_explosionVFX.transform.rotation = Quaternion.identity;
			}
		}
	}
}