using UnityEngine;
using UnityEngine.InputSystem.UI;
using Zenject;

namespace Sources.Common.Services.Input{
	public sealed class InputSystemService : IInputService, IInitializable<ProjectContext> {
		private static InputBindings _binding;
		private Transform _infrastructureTransform;

		public InputSystemService(Transform infrastructureTransform){
			_infrastructureTransform = infrastructureTransform;
		}
		
		public void Initialize(){
			var inputSystemUIInputModule = new GameObject($"[{typeof(InputSystemUIInputModule).Name}]").AddComponent<InputSystemUIInputModule>();
			inputSystemUIInputModule.transform.parent = _infrastructureTransform;

			_binding = new InputBindings();
			_binding.Enable();
		}
		
		public bool GetButton(string action){
			var input = _binding.FindAction(action);
			return input != null && input.IsPressed();
		}

		public bool GetButtonDown(string action){
			var input = _binding.FindAction(action);
			return input != null && input.WasPressedThisFrame();
		}

		public bool GetButtonUp(string action){
			var input = _binding.FindAction(action);
			return input != null && input.WasReleasedThisFrame();
		}

		public float GetAxis(string action){
			var input = _binding.FindAction(action);
			return input != null ? input.ReadValue<float>() : 0f;
		}

		public Vector2 GetVector(string action){
			var input = _binding.FindAction(action);
			var vector = input != null ? input.ReadValue<Vector2>() : Vector2.zero;
			return vector;
		}
	}
}