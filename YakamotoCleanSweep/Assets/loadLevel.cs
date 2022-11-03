using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    [SerializeField]
    private int level;

    [SerializeField] public event Action OnLevelFinished;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnLevelFinished?.Invoke();
            SceneManager.LoadScene(level);
        }
    }

}
