using UnityEngine;

public class CreditController : MonoBehaviour
{
    public GameObject creditImage;
    public GameObject prefabToHide; 

    void Start()
    {
        
        creditImage.SetActive(true);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            ToggleCreditVisibility();
        }
        if(Input.GetMouseButtonDown(0))
        {
            ToggleCreditVisibility();
        }
    }

    public void ToggleCreditVisibility()
    {
        
        creditImage.SetActive(!creditImage.activeSelf);

        
        if (creditImage.activeSelf)
        {
            prefabToHide.SetActive(false);
        }
        else
        {
            prefabToHide.SetActive(true);
        }
    }
}
