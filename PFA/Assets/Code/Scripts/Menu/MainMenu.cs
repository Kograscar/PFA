using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Scene _gameScene;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Button _chooseSceneButton;
    Button _explorerSceneButton;
    Button _guiderSceneButton;
    Button _settingsButton;
    Button _settingsReturnButton;
    Button _sceneChooseReturnButton;
    Button _quitButton;
    GameObject _chooseScenePanel;
    GameObject _settingsPanel;
    GameObject _mainMenuPanel;

    void Start(){
        _chooseSceneButton.onClick.AddListener(ChooseScene);
        _explorerSceneButton.onClick.AddListener(ExplorerScene);
        _guiderSceneButton.onClick.AddListener(GuiderScene);
        _settingsButton.onClick.AddListener(ShowSettings);
        _settingsReturnButton.onClick.AddListener(ShowMainMenu);
        _sceneChooseReturnButton.onClick.AddListener(ShowMainMenu);
        _quitButton.onClick.AddListener(QuitGame);
    }
    
    void ChooseScene()
    {
        _chooseScenePanel.SetActive(true);
        _mainMenuPanel.SetActive(false);
    }

    void ExplorerScene(){
        SceneManager.LoadSceneAsync("Explorer");
    }

    void GuiderScene(){
        SceneManager.LoadSceneAsync("Guider");
    }

    void ShowSettings(){
        _mainMenuPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    void ShowMainMenu(){
        _settingsPanel.SetActive(false);
        _chooseScenePanel.SetActive(false);
        _mainMenuPanel.SetActive(true);
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
