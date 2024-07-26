using Sources.Common.AssetManager;
using UnityEngine;

namespace Sources.Project.Gameplay.VFX{
	public sealed class BulletHoleController : MonoBehaviour{
		[SerializeField] private Color _clearColour;
		[SerializeField] private Material _paintShader;
		private RenderTexture PaintTarget;
		private RenderTexture TempRenderTarget;
		private Material ThisMaterial;

		private void OnValidate(){
			_paintShader = AssetManager.Load<Material>("Materials/VFX/PaintTexture");
		}

		public void PaintHole(RaycastHit hit){
			Vector2 LocalHit2 = hit.textureCoord;
			PaintAt(LocalHit2);
		}

		void PaintAt(Vector2 Uv){
			SetNewTexture();

			if (TempRenderTarget == null){
				TempRenderTarget = new RenderTexture(PaintTarget.width, PaintTarget.height, 0);
			}

			_paintShader.SetVector("PaintUv", Uv);
			Graphics.Blit(PaintTarget, TempRenderTarget);
			Graphics.Blit(TempRenderTarget, PaintTarget, _paintShader);
		}

		void SetNewTexture(){
			if (ThisMaterial == null)
				ThisMaterial = this.GetComponent<Renderer>().material;

			if (PaintTarget != null)
				if (ThisMaterial.mainTexture == PaintTarget)
					return;

			//	copy texture
			if (ThisMaterial.mainTexture != null){
				if (PaintTarget == null)
					PaintTarget = new RenderTexture(ThisMaterial.mainTexture.width, ThisMaterial.mainTexture.height, 0);
				Graphics.Blit(ThisMaterial.mainTexture, PaintTarget);
				ThisMaterial.mainTexture = PaintTarget;
			}
			else{
				if (PaintTarget == null)
					PaintTarget = new RenderTexture(1024, 1024, 0);

				//	clear if no existing texture
				Texture2D ClearTexture = new Texture2D(1, 1);
				ClearTexture.SetPixel(0, 0, _clearColour);
				Graphics.Blit(ClearTexture, PaintTarget);
				ThisMaterial.mainTexture = PaintTarget;
			}
		}
	}
}