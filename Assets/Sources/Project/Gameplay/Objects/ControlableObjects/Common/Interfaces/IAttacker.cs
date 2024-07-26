using System;

namespace Sources.Project.Gameplay.Objects{
	public interface IAttacker{
		public event Action OnAttack;
		public void Attack();
		public void StopAttack();
		public void ChangeForce(float force);
	}
}