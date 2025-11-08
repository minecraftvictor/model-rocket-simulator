using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LaunchButton : MonoBehaviour
{
    public float length;
    public LayerMask layermask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public UnityEvent buttonClicked;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, length, layermask))
            {
                buttonClicked.Invoke();
            }
        }
    }
}
