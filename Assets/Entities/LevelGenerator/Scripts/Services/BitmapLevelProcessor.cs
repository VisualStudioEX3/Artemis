using System;
using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.LevelGenerator.Services
{
    /// <summary>
    /// Bitmap level processor.
    /// </summary>
    /// <remarks>Process a bitmap image searching for a pixelx that matches the <see cref="Color"/> value mask.</remarks>
    public class BitmapLevelProcessor
    {
        #region Internal vars
        private Texture2D _bitmap;
        private Color _colorMask;
        private float _gridSize;
        #endregion

        #region Events
        /// <summary>
        /// Notify when a pixel matchs the <see cref="Color"/> value mask.
        /// </summary>
        /// <remarks>Returns a <see cref="Vector2"/> value with the pixel coordinates.</remarks>
        public event Action<Vector2> OnElementFound;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bitmap"><see cref="Texture2D"/> instance that contain the bitmap image.</param>
        /// <param name="colorMask"><see cref="Color"/> value used as mask to find the required positions in the bitmap.</param>
        /// <param name="gridSize">The grid size, same value for width and height.</param>
        public BitmapLevelProcessor(Texture2D bitmap, Color colorMask, float gridSize)
        {
            this._bitmap = bitmap;
            this._colorMask = colorMask;
            this._gridSize = gridSize;
        }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Process the bitmap searching the color matchs.
        /// </summary>
        public void Process()
        {
            if (this.CheckBitmapSize())
                this.ReadPixels();
            else
                throw this.FormatArgumentException();
        }

        private bool CheckBitmapSize() => this._bitmap.width == this._gridSize && this._bitmap.height == this._gridSize;

        private ArgumentException FormatArgumentException()
        {
            return new ArgumentException($"{nameof(BitmapLevelProcessor)}::{nameof(this.Process)}: The bitmap \"{this._bitmap.name}\" not fit the expected size: {this._gridSize}x{this._gridSize}");
        }

        private void ReadPixels()
        {
            for (int y = 0; y < this._gridSize; y++)
                for (int x = 0; x < this._gridSize; x++)
                    this.ProcessPixel(x, y);
        }

        private Color ReadPixel(int x, int y) => this._bitmap.GetPixel(x, y);

        private void ProcessPixel(int x, int y)
        {
            if (this.ReadPixel(x, y) == this._colorMask)
                this.OnElementFound?.Invoke(new Vector2(x, y));
        }
        #endregion
    }
}
