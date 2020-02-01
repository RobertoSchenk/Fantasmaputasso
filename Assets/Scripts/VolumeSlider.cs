using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string name;
    public float minVol;
    // Start is called before the first frame update
    

    public void ChangeVolume (float v)
    {
        v = Mathf.Lerp(minVol, 0, v);
        mixer.SetFloat(name, v);
    }
}
