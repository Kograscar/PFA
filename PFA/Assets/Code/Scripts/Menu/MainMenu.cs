using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Scene _gameScene;
    //[SerializeField] AudioMixer audioMixer;
    [SerializeField] Button _chooseSceneButton;
    [SerializeField] Button _explorerSceneButton;
    [SerializeField] Button _guiderSceneButton;
    [SerializeField] Button _creditsButton;
    [SerializeField] Button _creditsReturnButton;
    [SerializeField] Button _sceneChooseReturnButton;
    [SerializeField] Button _quitButton;
    [SerializeField] GameObject _chooseScenePanel;
    [SerializeField] GameObject _creditsPanel;
    [SerializeField] GameObject _mainMenuPanel;
    [SerializeField] GameObject _loadIcon;


    void Start(){
        _chooseSceneButton.onClick.AddListener(ChooseScene);
        _explorerSceneButton.onClick.AddListener(ExplorerScene);
        _guiderSceneButton.onClick.AddListener(GuiderScene);
        _creditsButton.onClick.AddListener(ShowCredits);
        _creditsReturnButton.onClick.AddListener(ShowMainMenu);
        _sceneChooseReturnButton.onClick.AddListener(ShowMainMenu);
        _quitButton.onClick.AddListener(QuitGame);
    }
    
    void ChooseScene()
    {
        _chooseScenePanel.SetActive(true);
        _mainMenuPanel.SetActive(false);
    }

    public void ExplorerScene(){
        SceneManager.LoadSceneAsync("Explorer");
        _loadIcon.SetActive(true);

    }

    public void GuiderScene(){
        SceneManager.LoadSceneAsync("Instructor");
        _loadIcon.SetActive(true);
    }

    void ShowCredits(){
        _mainMenuPanel.SetActive(false);
        _creditsPanel.SetActive(true);
    }

    void ShowMainMenu(){
        _creditsPanel.SetActive(false);
        _chooseScenePanel.SetActive(false);
        _mainMenuPanel.SetActive(true);
    }

    public void QuitGame ()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }

    /*public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }*/
}
