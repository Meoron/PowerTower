using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sources.Project.Managers.UpdateManager{
	public sealed class UpdateManager : MonoBehaviour, IUpdateManager{
		private static UpdateManager _instance;
		private readonly List<IUpdatable> _updatableObjects = new List<IUpdatable>();
		private readonly List<IFixedUpdatable> _fixedUpdatableObjects = new List<IFixedUpdatable>();
		private readonly List<ILateUpdate> _lateUpdatableObjects = new List<ILateUpdate>();
		
		public bool HasRegisteredObjects => _updatableObjects?.Count > 0 || _fixedUpdatableObjects?.Count >0 || _lateUpdatableObjects?.Count>0;
		
		public static UpdateManager Instance => _instance;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Register(IManagedObject updatableObject){
			if(gameObject==null) return;
			
			if (updatableObject is IUpdatable updatable){
				_updatableObjects.Add(updatable);
			}
			
			if (updatableObject is IFixedUpdatable fixedUpdatable){
				_fixedUpdatableObjects.Add(fixedUpdatable);
			}

			if (updatableObject is ILateUpdate lateUpdatable){
				_lateUpdatableObjects.Add(lateUpdatable);
			}

			enabled = HasRegisteredObjects;
			Debug.Log(updatableObject);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Unregister(IManagedObject monoUpdatable){
			if(gameObject==null) return;
			
			if (monoUpdatable is IUpdatable updatable){
				_updatableObjects.Remove(updatable);
			}

			enabled = HasRegisteredObjects;
			Debug.Log(monoUpdatable.GetType().Name);
		}

		public void Clear()
		{
			_updatableObjects.Clear();
			enabled = false;
		}
		
		private void Awake(){
			_instance = this;
		}

		private void Update(){
			var count = _updatableObjects.Count;
			for (int i = 0; i < count; i++){
				if (i >= _updatableObjects.Count){
					break;
				}
				
				try{
					_updatableObjects[i].OnUpdate(Time.deltaTime);
				}
				catch (Exception ex){
					Debug.LogException(ex);
				}
			}
		}
		
		private void FixedUpdate(){
			var count = _fixedUpdatableObjects.Count;
			for (int i = 0; i < count; i++){
				if (i >= _fixedUpdatableObjects.Count){
					break;
				}
				
				try{
					_fixedUpdatableObjects[i].OnFixedUpdate(Time.fixedDeltaTime);
				}
				catch (Exception ex){
					Debug.LogException(ex);
				}
			}
		}

		private void LateUpdate(){
			var count = _lateUpdatableObjects.Count;
			for (int i = 0; i < count; i++){
				if (i >= _lateUpdatableObjects.Count){
					break;
				}
				
				try{
					_lateUpdatableObjects[i].OnLateUpdate(Time.deltaTime);
				}
				catch (Exception ex){
					Debug.LogException(ex);
				}
			}
		}
	}
}