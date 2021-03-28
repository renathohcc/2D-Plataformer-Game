using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to load new scenes
    public void LoadScene(string sceneName){
        StartCoroutine(FadeIn(sceneName));
    }

    public void ExitGame(){
        Application.Quit();
    }

    //To make the FadeIn effect
    IEnumerator FadeIn(string sceneName){
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
