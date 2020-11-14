using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWorker : MonoBehaviour
{
    public static AudioWorker instance;
    public List<AudioProfil> audios;
    public AudioSource audiosource;

    private void Awake()
    {
        instance = this;
    }
    public static void PlayAudio(string AudioID)
    {
        AudioProfil findaudio = AudioWorker.instance.audios.Find(x => x.AudioID == AudioID);
        AudioWorker.instance.audiosource.PlayOneShot(findaudio.clip);

    }



    [System.Serializable]
    public class AudioProfil
    {
        public string AudioID;
        public AudioClip clip;
    }
}
