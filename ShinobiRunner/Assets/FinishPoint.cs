using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    
    private void onTriggerEnter2D(Collider2D collision){
        if(collision.tag =="Player"){
            //go next level
            Debug.Log("Player reached the FinishPoint");
            SeneController.instance.NextLevel();
            SeneController.instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
    }
}
