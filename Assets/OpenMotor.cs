using UnityEngine;

public class OpenMotor : MonoBehaviour
{
    public GameObject menu;
    public bool open = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   

    }

    public void ToggleMenu(){
        if(open == false){
            menu.SetActive(true);
            open = true;
        }
        else{
            menu.SetActive(false);
            open = false;
        }
    }

}


