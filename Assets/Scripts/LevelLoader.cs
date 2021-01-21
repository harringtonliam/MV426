using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //parameters
    [SerializeField] float loadDelalySeconds = 1.5f;

    //member varuiables
    int sceneToLoad = 1;

    // Start is called before the first frame update
    void Start()
    {

        SplashSceneProcessing();
    }

    private void SplashSceneProcessing()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneBuildIndex == 0)
        {
            sceneToLoad = 1;
            StartCoroutine("LoadSceneWithDelay");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCurrentScene()
    {
        sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine("LoadSceneWithDelay");
    }

    public IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(loadDelalySeconds);
        SceneManager.LoadScene(sceneToLoad);
    }
}
