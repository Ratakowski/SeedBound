using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public GameObject backgroundMusic; 
    public GameObject ButtonCheckOn;
    public Button On;
    public Button Off;
    public Slider musicSlider;
    private AudioSource audioSource;
    public bool isCheckOn = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = backgroundMusic?.GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (On != null)
        {
            On.onClick.AddListener(TurnMusicOff);
            Debug.Log("Button On terhubung dengan fungsi TurnMusicOff.");
        }
        else
        {
            Debug.LogError("Button On tidak ditemukan atau belum terhubung!");
        }

        if (Off != null)
        {
            Off.onClick.AddListener(TurnMusicOn);
            Debug.Log("Button Off terhubung dengan fungsi TurnMusicOn.");
        }
        else
        {
            Debug.LogError("Button Off tidak ditemukan atau belum terhubung!");
        }
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            Debug.Log("Music Slider ditemukan dan listener ditambahkan.");

            if (audioSource != null)
            {
                musicSlider.value = audioSource.volume;
                Debug.Log($"AudioSource volume: {audioSource.volume}. Music Slider nilai awal: {musicSlider.value}");
            }
            else
            {
                Debug.LogError("AudioSource tidak ditemukan di backgroundMusic!");
            }
        }
        else
        {
            Debug.LogError("Music Slider tidak ditemukan atau belum terhubung!");
        }

        UpdateButtonState();
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        if (ButtonCheckOn == null)
        {
            ButtonCheckOn = GameObject.Find("CheckBoxOn");
        }

        if (On == null || Off == null)
        {
            On = GameObject.Find("CheckBoxOn")?.GetComponent<Button>();
            Off = GameObject.Find("CheckBoxOff")?.GetComponent<Button>();
            if (On != null) On.onClick.AddListener(TurnMusicOff);
            if (Off != null) Off.onClick.AddListener(TurnMusicOn);
        }

        if (musicSlider == null)
        {
            musicSlider = GameObject.Find("MusicSlider")?.GetComponent<Slider>();
            if (musicSlider != null)
            {
                musicSlider.onValueChanged.AddListener(SetMusicVolume);
            }
        }

        if (musicSlider != null && audioSource != null)
        {
            musicSlider.value = audioSource.volume;
        }

        UpdateButtonState();
    }

    public void TurnMusicOff()
    {
        isCheckOn = false;
        if (audioSource != null)
        {
            audioSource.mute = true;
        }
        UpdateButtonState();
    }

    public void TurnMusicOn()
    {
        isCheckOn = true;
        if (audioSource != null)
        {
            audioSource.mute = false;
        }
        UpdateButtonState();
    }

    public void SetMusicVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            Debug.Log("Music volume set to: " + volume);
        }
    }

    void UpdateButtonState()
    {
        if (isCheckOn)
        {
            ButtonCheckOn?.SetActive(true);
            On?.gameObject.SetActive(true);
            Off?.gameObject.SetActive(false);
        }
        else
        {
            ButtonCheckOn?.SetActive(false);
            On?.gameObject.SetActive(false);
            Off?.gameObject.SetActive(true);
        }
        if (musicSlider != null)
        {
            musicSlider.interactable = isCheckOn;
        }
    }
    public void PauseMusic()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
            Debug.Log("Background music paused.");
        }
    }

    public void ResumeMusic()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
            Debug.Log("Background music resumed.");
        }
    }

}
