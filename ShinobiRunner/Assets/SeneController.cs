using UnityEngine;
using UnityEngine.SceneManagement;
public class SeneController : MonoBehaviour
{
    public static SeneController instance;

    public void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int sceneId){
        SceneManager.LoadSceneAsync(sceneId);
    }
}
