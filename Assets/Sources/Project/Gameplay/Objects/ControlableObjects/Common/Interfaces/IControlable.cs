using UnityEngine;

namespace Sources.Project.Gameplay.Objects{
	public interface IControlable{
		public Transform AnchorTransform{ get; } // Transform for follow;
	}
}