using Assets._Scripts.View;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Image button implement button. On pointer click load image from gallery.
/// </summary>
public class ImageButton : Button
{
	VehicleNameView _vehicleNameView;

	/// <summary>
	/// Load image on pointer click
	/// </summary>
	/// <param name="eventData">Pointer event data</param>
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

		if(_vehicleNameView == null)
        {
			_vehicleNameView = GetComponent<VehicleNameView>();
		}

		NativeGallery.Permission permission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Read);
		if (permission == NativeGallery.Permission.ShouldAsk)
		{
			permission = NativeGallery.RequestPermission(NativeGallery.PermissionType.Read);
			Debug.Log("Asking");
		}
		else if (permission == NativeGallery.Permission.Granted)
        {
			Debug.Log("Granded");
		}
		else
		{
			Debug.Log("Not allowed");
			return;
		}

		LoadImage();

		/*
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
			LoadImage();
		}
#else
		LoadImage();
#endif*/
	}

	/// <summary>
	/// Load image from gallery, convert to grayscale and assign to target image
	/// </summary>
	private void LoadImage()
    {
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				Texture2D texture = NativeGallery.LoadImageAtPath(path, 1024, false);

				string fileName = Path.GetFileNameWithoutExtension(path);

				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}

				texture = ConvertToGrayscale(texture);

				Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

				sprite.name = fileName;
				((Image)targetGraphic).sprite = sprite;

				_vehicleNameView.SetVehicleName(fileName);
			}
		}, "Select a PNG image", "image/png");

		Debug.Log($"Permission Status: {permission}");
	}

	/// <summary>
	/// Method to change colored Texture2D to grayscale Texture2D
	/// </summary>
	/// <param name="graph">colored texture 2D</param>
	/// <returns>texture 2D in grayscale</returns>
	private Texture2D ConvertToGrayscale(Texture2D graph)
	{
		Color32[] pixels = graph.GetPixels32();

		for (int x = 0; x < graph.width; x++)
        {
			for (int y = 0; y < graph.height; y++)
            {
				Color32 pixel = pixels[x + y * graph.width];
				int p = ((256 * 256 + pixel.r) * 256 + pixel.b) * 256 + pixel.g;
				int b = p % 256;
				p = Mathf.FloorToInt(p / 256);
				int g = p % 256;
				p = Mathf.FloorToInt(p / 256);
				int r = p % 256;
				float l = (0.2126f * r / 255f) + 0.7152f * (g / 255f) + 0.0722f * (b / 255f);
				Color c = new Color(l, l, l, 1);
				graph.SetPixel(x, y, c);
			}
		}

		graph.Apply(true);

		return graph;
	}
}
