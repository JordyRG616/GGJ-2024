using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private EventReference audioEventAdress;
    private EventInstance audioEventInstance;


    private void Start()
    {
        audioEventInstance = RuntimeManager.CreateInstance(audioEventAdress);
    }

    public void Play()
    {
        audioEventInstance.start();
    }

    public void Stop()
    {
        audioEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
