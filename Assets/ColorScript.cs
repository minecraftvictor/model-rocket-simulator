using UnityEngine;

public class ColorScript : MonoBehaviour
{
    public GameObject rocket;

    public MeshRenderer mr; 
   
    public void changecolor(Material color){
        if(mr == null){
            Debug.Log("Meshrenderer not found!");
        }
        mr.material = color;
    }
    
}
 