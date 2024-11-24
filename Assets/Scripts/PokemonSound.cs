using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSound : MonoBehaviour
{
    public string audioUrl; // URL of the audio file
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
    public void ButtonPress()
    {
        StartCoroutine(LoadAndPlayAudio(audioUrl));
    }
    

    private IEnumerator LoadAndPlayAudio(string url)
    {
        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN))
        {
            // Send the web request and wait for it to complete
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError ||
                request.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error loading audio: {request.error}");
            }
            else
            {
                // Get the downloaded AudioClip
                AudioClip audioClip = UnityEngine.Networking.DownloadHandlerAudioClip.GetContent(request);

                // Assign it to the AudioSource and play
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }
    }
}
