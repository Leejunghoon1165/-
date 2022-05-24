using UnityEngine;
using System.Collections;

public class RenderTextureDemo : MonoBehaviour
{
	public static RenderTextureDemo Inst { get; private set; }
	public RenderTexture renderTexture;
    public GameObject shatterPrefab;

	// Use this for initialization

	public void StartQuake()
    {
		// Create the shatter
		var shatter = Instantiate(shatterPrefab);

		// Get the ShatterSpawner Component
		var shatterScript = shatter.GetComponent<ShatterSpawner>();

		shatterScript.MaxPlayTime = 2f;

		// Randomize the shatter origin
		shatterScript.RandomizeShatterOrigin = true;

		// Make the background opaque and set the renderTexture as the material texture
		shatterScript.ClearCamera = true;
		shatterScript.BackgroundColor = Color.black;
		shatterScript.ScreenMaterial.mainTexture = renderTexture;
	}
}
