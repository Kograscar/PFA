using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    [SerializeField] GameObject _loadingScreen;
    public void LoadChoosenScene(string sceneName){
        _loadingScreen.SetActive(true);
        AsyncOperation asyncOperation;
        asyncOperation = SceneManager.LoadSceneAsync("LoadScene");
        while (!asyncOperation.isDone){
            if(asyncOperation.progress >= .9f){
                asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            }
        }
            /*if(asyncOperation.progress >= .9f){
                _loadingScreen.SetActive(false);
            }*/
    }
}
