using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] int sceneToLoad = 0;
 
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {
            StartCoroutine(Transition());
        }
    }

  private IEnumerator Transition()
    {

        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        print("load scene");

        Destroy(gameObject);
        

         
    }
}
