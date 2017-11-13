using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstudioLuncherScript : MonoBehaviour, IInputClickHandler, IInputHandler
{
     public GameObject mesh;
     bool meshCreated;
    private void Start()
    {
        meshCreated = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("llegamosssss"); 
        if (!meshCreated)
        {
            mesh.SetActive(true);
            mesh = Instantiate(mesh, transform.position + transform.forward * 1 , Quaternion.Euler(-90,0,0));
            meshCreated = true;
        }
        else 
        {
            mesh.SetActive(!mesh.active);
        }
        Debug.Log("nos vamosss");
    }
    public void OnInputDown(InputEventData eventData)
    { }
    public void OnInputUp(InputEventData eventData)
    { }
}