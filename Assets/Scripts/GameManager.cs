using UnityEngine;
using UnityEngine.SceneManagement;

namespace VisualStudioEX3.Artemis
{
    public class GameManager : MonoBehaviour
    {
        public int startSceneIndex = 1;

        private void Start()
        {
            SceneManager.LoadScene(this.startSceneIndex);
        }
    }
}
