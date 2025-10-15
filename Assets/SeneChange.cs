using UnityEngine;
using UnityEngine.SceneManagement; 
public class SeneChange : MonoBehaviour
{
      // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void SceneChange(string scenename){
        SceneManager.LoadScene(scenename);
    }
}