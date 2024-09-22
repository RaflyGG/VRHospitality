using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;

    [SerializeField] private Material _material;

    private Texture2D _templateDirtMask;
    private XRController _controller; // Reference to the VR controller

    private void Start()
    {
        CreateTexture();
        _controller = GetComponent<XRController>(); // Get the XRController component
    }

    private void Update()
    {
        // Check if the controller is pressing the primary button
        if (_controller.inputDevice.IsPressed(InputHelpers.Button.PrimaryButton, out bool isPressed) && isPressed)
        {
            // Perform a raycast from the controller's position
            Ray ray = new Ray(_controller.transform.position, _controller.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector2 textureCoord = hit.textureCoord;

                int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
                int pixelY = (int)(textureCoord.y * _templateDirtMask.height);

                for (int x = 0; x < _brush.width; x++)
                {
                    for (int y = 0; y < _brush.height; y++)
                    {
                        Color pixelDirt = _brush.GetPixel(x, y);
                        Color pixelDirtMask = _templateDirtMask.GetPixel(pixelX + x, pixelY + y);

                        _templateDirtMask.SetPixel(pixelX + x,
                            pixelY + y,
                            new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
                    }
                }

                _templateDirtMask.Apply();
            }
        }
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();

        _material.SetTexture("_DirtMask", _templateDirtMask);
    }
}
