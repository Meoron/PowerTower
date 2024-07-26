using System.Threading.Tasks;
using Sources.Common.StateMachine;
using UnityEngine.SceneManagement;

namespace Sources.Project.StateMachine{
	public sealed class GameLoopProjectState : IState{
		public async void OnEnter(){
			await LoadScene("GameScene");
		}

		public void OnExit(){
		}

		private async Task LoadScene(string sceneName){
			var asyncOp = SceneManager.LoadSceneAsync(sceneName);
			while (!asyncOp.isDone){
				await Task.Delay(100);
			}
		}
	}
}