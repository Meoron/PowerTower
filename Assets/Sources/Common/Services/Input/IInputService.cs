using UnityEngine;

namespace Sources.Common.Services.Input{
	public interface IInputService{
		public bool GetButton(string action);
		public bool GetButtonDown(string action);
		public bool GetButtonUp(string action);
		public float GetAxis(string action);
		public Vector2 GetVector(string action);
	}
}