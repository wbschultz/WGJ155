using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    private void Awake()
    {
        GetComponent<Slider>().value = MusicManager.Instance.music_source.volume;
    }

    public void OnChangeVolume()
    {
        MusicManager.Instance.SetVolume(GetComponent<Slider>().value);
    }
}
