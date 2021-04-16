using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class ImageLoader : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] ImageClient client;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Makes a Tecture2D from the picture in FilePath.
    /// Might throw error if image is not found.
    /// </summary>
    /// <param name="FilePath">File address path to the image to load</param>
    /// <returns>Texture2D with the image on FilePath</returns>
    public Texture2D LoadTexture(string FilePath)
    {
        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails
        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(1, 1);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))
            {           // Load the imagedata into the texture (size is set automatically)
                image.texture = Tex2D;         //Display on UI
                client.imagePath = FilePath;
                return Tex2D;                 // If data = readable -> return texture
            }
        }
        return null;                     // Return null if load failed
    }
}
