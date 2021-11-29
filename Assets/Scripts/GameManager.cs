using System.Collections;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Components;
using VisualStudioEX3.Artemis.Framework.InputManager.Components;

namespace VisualStudioEX3.Artemis
{
    public class GameManager : MonoBehaviour
    {
        #region Initializer
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => InputManager.IsInitialized);
            yield return new WaitUntil(() => SceneManager.IsInitialized);

            SceneManager.Instance.LoadStartScreen();
        } 
        #endregion
    }
}
