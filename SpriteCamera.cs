using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteCamera : MonoBehaviour
{
    public GameObject objectToRender;
    public Image img;
    
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        Vector3 pos = _camera.transform.position;
        objectToRender.transform.position = new Vector3(pos.x, pos.y, pos.z + 2.5f);

        _camera.enabled = true;
        Texture2D texture2D = RTImage(_camera);
        _camera.enabled = false;
        img.sprite = Texture2DToSprite(texture2D);
    }
    
    public static Sprite Texture2DToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
    
    Texture2D RTImage(Camera cam)
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;

        // Render the camera's view.
        cam.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
    }
}
