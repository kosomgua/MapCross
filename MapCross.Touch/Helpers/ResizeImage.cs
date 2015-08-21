using System;
using UIKit;
using System.Drawing;

namespace MapCross.Touch
{
	public static class ResizeImage
	{ 
		public static UIImage MaxResizeImage(UIImage sourceImage, float maxWidth, float maxHeight)
		{
			var sourceSize = sourceImage.Size;
			var maxResizeFactor = Math.Max(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
			if (maxResizeFactor > 1) return sourceImage;
			float width = (float)(maxResizeFactor * sourceSize.Width);
			float height = (float)(maxResizeFactor * sourceSize.Height);
			UIGraphics.BeginImageContext(new SizeF(width, height));
			sourceImage.Draw(new RectangleF((float)0, (float)0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}
	}
}

