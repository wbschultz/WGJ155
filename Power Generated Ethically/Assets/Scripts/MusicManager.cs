using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Music manager: 
/// Start music by calling PlayMusic()
/// Stop Music by calling StopMusic()
/// 
/// To play an effect, call PlayEffect(AudioClip clip), where clip is the sound file you want to play
/// 
/// Adjust the volume by calling SetVolume(float vol), with vol being a float between 0 and 1.
/// </summary>
public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource music_source;
    public AudioSource effects_source;
    public Slider volume_slider;

    public static MusicManager Instance = null;
    // Update is called once per frame

    //Initialize the singleton instance
    private void Awake()
    {
        //if there is no instance of MusicManager, set to this
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this) //Otherwise, destroy this gameobject to enforce the singleton
        {
            Destroy(gameObject);
        }
        //Set to don't destroy on load so that it remains when reloading the scene
        DontDestroyOnLoad(gameObject);

        SetVolume(volume_slider.value); //set the volume to what the slider is initially set to
    }
    void Update()
    {

    }

    
    public void PlayMusic()
    {
        music_source.Play();
    }
    public void PlayEffect(AudioClip clip)
    {
        effects_source.clip = clip;
        effects_source.Play();
    }
    public void StopMusic()
    {
        music_source.Stop();
    }
    public void SetVolume(float vol)
    {
        if (vol > 1)
            vol = 1;
        else if (vol < 0)
            vol = 0;
        music_source.volume = vol;
        effects_source.volume = vol;
    }
}
