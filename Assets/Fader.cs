using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{

    CanvasGroup canvasGroup;
    [SerializeField] float fadeTime;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

    }

  

    
    void Update()
    {
        while (canvasGroup.alpha !=1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeTime/100;
        }

    }
}
