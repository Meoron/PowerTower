using Sources.Project.Gameplay.Objects;

namespace Sources.Project.Gameplay.Camera{
	public interface IFollowableCamera : IGiveGetTransform{
		public void SetTarget(IControlable target);
		public void ShakeCamera();
	}
}