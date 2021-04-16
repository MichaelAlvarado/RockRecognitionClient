using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


/// <summary>
/// This Client inheritated class acts like Client using UI elements like buttons and input fields.
/// </summary>
public class ImageClient : Client
{
    [Header("UI References")]
    [SerializeField] private Button m_StartClientButton = null;
    [SerializeField] private Button m_SendImageToServerButton = null;
    [SerializeField] private Button m_SendCloseButton = null;
    [SerializeField] private Text m_ClientLogger = null;
    [SerializeField] public string imagePath = null;

    //Set UI interactable properties
    private void Awake()
    {
        //Start Client
        m_StartClientButton.onClick.AddListener(base.StartClient);

        //Send to Image Server
        m_SendImageToServerButton.interactable = false;
        m_SendImageToServerButton.onClick.AddListener(SendImageToServer);

        //SendClose
        m_SendCloseButton.interactable = false;
        m_SendCloseButton.onClick.AddListener(SendCloseToServer);

        //Populate Client delegates
        OnClientStarted = () =>
        {
            //Set UI interactable properties        
            m_SendCloseButton.interactable = true;
            m_SendImageToServerButton.interactable = true;
            m_StartClientButton.interactable = false;
        };

        OnClientClosed = () =>
        {
            //Set UI interactable properties        
            m_StartClientButton.interactable = true;
            m_SendImageToServerButton.interactable = false;
            m_SendCloseButton.interactable = false;
        };
    }

    public void SendImageToServer()
    {
        base.SendImageToServer(LoadImage(imagePath));
    }

    private byte[] LoadImage(string FilePath)
    {
        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            return FileData;                 // If data = readable -> return texture
        }
        Debug.Log("Image Not Found");
        return null;                     // Return null if load failed
    }

    private void SendCloseToServer()
    {
        base.SendMessageToServer("Close");
        //Set UI interactable properties        
        m_SendCloseButton.interactable = false;
    }

    //Custom Client Log
    #region ClientLog
    protected override void ClientLog(string msg, Color color)
    {
        base.ClientLog(msg, color);
        m_ClientLogger.text += '\n' + "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">- " + msg + "</color>";
    }
    protected override void ClientLog(string msg)
    {
        base.ClientLog(msg);
        m_ClientLogger.text += '\n' + "- " + msg;
    }
    #endregion
}