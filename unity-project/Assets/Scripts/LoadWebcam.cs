using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadWebcam : MonoBehaviour
{

    private bool authorized = false;
    private bool playing = false;
    private WebCamTexture webcamTexture;

    // Use this for initialization
    IEnumerator Start()
    {
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.Log("No Webcam devices.");
            return false;
        }

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        Debug.Log("RequestUserAuthorization returned");

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            authorized = true;

            Debug.Log("HasUserAuthorization");
            webcamTexture = new WebCamTexture();
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }
        else
        {
            Debug.Log("!HasUserAuthorization");
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        if (!webcamTexture)
        {
            return;
        }
        if (authorized && !playing && webcamTexture.didUpdateThisFrame)
        {
            Debug.Log("Authorized and actually active.");
            GameObject.Find("Information image 1").GetComponent<Image>().enabled = true;
            GameObject.Find("Information image 1a").GetComponent<Image>().enabled = false;
            playing = true;
        }
    }
}
