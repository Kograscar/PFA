using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] string _gameScene;
    [SerializeField] Scene _gameScene;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField]public GameObject _playButton;

    void Start(){
        //_playButton.GetComponent<Button>().
    }
    
   public void Playgame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame ()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }
}
