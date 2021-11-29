using System;
using UnityEngine;

namespace VisualStudioEX3.Artemis.Framework.Core.Contracts.Models
{
    /// <summary>
    /// Serializable Scene asset structure.
    /// </summary>
    /// <remarks>This structure allow to show <see cref="UnityEngine.SceneManagement.Scene"/> fields on inspectors.</remarks>
    [Serializable]
    public struct SceneAsset
    {
        #region Inspector fields
#pragma warning disable 649
        [SerializeField]
        private UnityEngine.Object _asset;
        [SerializeField]
        private string _assetPath;
        [SerializeField]
        private string _scenePath;
        [SerializeField]
        private int _sceneIndex;
#pragma warning restore
        #endregion

        #region Properties
        /// <summary>
        /// Path to asset file in project.
        /// </summary>
        public string AssetPath => this._assetPath;

        /// <summary>
        /// Scene path on Unity project (the path use to load the scene using the <see cref="UnityEngine.SceneManagement.SceneManager"/>).
        /// </summary>
        public string ScenePath => this._scenePath;

        /// <summary>
        /// Scene index on project (the index use to load the scene using the <see cref="UnityEngine.SceneManagement.SceneManager"/>).
        /// </summary>
        public int SceneIndex => this._sceneIndex;
        #endregion

        #region Operators
        public static implicit operator string(SceneAsset scene)
        {
            return scene._scenePath;
        }
        #endregion
    }
}