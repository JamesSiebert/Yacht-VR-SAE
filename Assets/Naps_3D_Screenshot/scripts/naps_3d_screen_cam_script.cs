using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;


public class naps_3d_screen_cam_script : MonoBehaviour {

    [Tooltip("adjust for better results on large depth scenes")]
    public float depth_contrast=2.0f;
    [Tooltip("adjust for better results on large depth scenes")]
    public float depth_brightness = 0.75f;

    public Material mat;

    private bool savescreen = false;

    public RenderTexture rtActive;

    private void Awake()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        Debug.Log("awake");

        // rtActive = GetComponent<Camera>().targetTexture;


    }

    private void Update()
    {
        //Change the key at your preference
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Debug.Log("press");
            savescreen = true;
            
        }
        
    }
    public void Capture()
    {
        // rtActive = RenderTexture.active;
        
        savescreen = true;

        OnRenderImageMod(rtActive, null);
    }
    
    

    void OnRenderImageMod(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("call capture");
        RenderTexture _destination = destination;

        if (!savescreen)
        {
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
            Graphics.Blit(source, destination);
            Debug.Log("false");
        }
        else
        {
            Debug.Log("save");
            string name = "";
            if (destination==null)
            {
                destination = new RenderTexture(source.width, source.height, 24,source.format,RenderTextureReadWrite.Default);
            }
            mat.SetFloat("_contrast", depth_contrast);
            mat.SetFloat("_brightness", depth_brightness);

            //Save Depth Screen Texture
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
            Graphics.Blit(source, destination, mat);
            Texture2D depth = new Texture2D(destination.width, destination.height, TextureFormat.RGB24, false);
            depth.ReadPixels(new Rect(0, 0, destination.width, destination.height), 0, 0);
            depth.Apply();
            byte[] jpg_d = depth.EncodeToJPG(100);
            //Change the path and name at your preference
            name = Application.dataPath + "/../screen3D_depth" + ".jpg";
            File.WriteAllBytes(name, jpg_d);

            //Save Rendered Screen Texture
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
            Graphics.Blit(source, destination);
            Texture2D normal = new Texture2D(destination.width, destination.height, TextureFormat.RGB24, false);            
            normal.ReadPixels(new Rect(0, 0, destination.width, destination.height), 0, 0);
            normal.Apply();
            byte[] jpg = normal.EncodeToJPG(100);
            //Change the path and name at your preference
            name = Application.dataPath + "/../screen3D"+ ".jpg";
            File.WriteAllBytes(name, jpg);
            Debug.Log("saved " + name);



            savescreen = false;
            Debug.Log("saved " + name);

            GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
            Graphics.Blit(source, _destination);
        }
    }
}
