using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionInGame : MonoBehaviour
{
    public GameObject UIOption;
    public GameObject UIPause;
    void Start()
    {
        UIOption.SetActive(false);
        
    }

    public void BtnOption()
    {
        UIOption.SetActive(true);
        UIPause.SetActive(false);
    }

    public void Back()
    {
        UIOption.SetActive(false);
        UIPause.SetActive(true);
    }
}
