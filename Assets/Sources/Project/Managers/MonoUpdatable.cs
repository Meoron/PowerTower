using System;
using UnityEngine;

namespace Sources.Project.Managers.UpdateManager{
	public abstract class MonoUpdatable : MonoBehaviour, IManagedObject{
		private UpdateManager _updateManager;
		private bool _isSetup;
		
		private void OnEnable(){
			if (_isSetup == false){
				TrySetup();
			}

			if (_isSetup && _updateManager!=null){
				_updateManager.Register(this);
			}
			
			OnEnabled();
		}

		private void OnDisable(){
			if (_isSetup && _updateManager!=null){
				_updateManager.Unregister(this);
			}
			OnDisabled();
			
		}

		private void TrySetup(){
			if (Application.isPlaying){
				_updateManager = UpdateManager.Instance;
				_isSetup = true;
			}
			else{
				throw new Exception(
					$"You tries to get {nameof(UpdateManager)} instance when application is not playing!");
			}
		}
		
		protected virtual void OnEnabled(){
		}

		protected virtual void OnDisabled(){
		}
	}
}