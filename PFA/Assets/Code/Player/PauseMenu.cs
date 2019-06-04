using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject _pauseMenu;
    GameObject _settingsPanel;
    GameObject _mainPanel;
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _settingsButton;
    #region SettingsVar

    #endregion SettingsVar
    [SerializeField] Button _mainMenuButton;
    [SerializeField] Button _quitButton;
    [SerializeField] Button _returnButton;
    bool _paused;

    void Start(){
        _resumeButton.onClick.AddListener(Resume);
        _settingsButton.onClick.AddListener(Settings);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
        _returnButton.onClick.AddListener(Return);
    }

    void Update(){
        if(Input.GetButtonDown("Cancel")){
            ChangeState();
        }
    }

    void Resume(){
        ChangeState();
    }

    void Settings(){
        _settingsPanel.SetActive(true);
        _mainPanel.SetActive(false);
    }

    #region SettingsVoid
    
    void Return(){
        _settingsPanel.SetActive(false);
        _mainPanel.SetActive(true);
    }

    #endregion SettingsVoid

    void MainMenu(){
        SceneManager.LoadSceneAsync(0);
    }

    void Quit(){
        Application.Quit();
    }

    void ChangeState(){
        _paused = !_paused;
        if(_paused){
            _settingsPanel.SetActive(false);
            _mainPanel.SetActive(true);
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
        _pauseMenu.SetActive(_paused);
        GetComponent<CharController>().enabled = _paused;
        GetComponent<PlayerMove>().enabled = _paused;
        GetComponentInChildren<PlayerLook>().enabled = _paused;
    }
}
