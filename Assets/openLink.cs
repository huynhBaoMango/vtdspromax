using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openLink : MonoBehaviour
{
    public void OpenThis(string link)
    {
        link = link.ToLower();
        Application.OpenURL(link);
    }
}
