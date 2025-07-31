using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameManager _instance;
    public GameManager Instance()
    {
        return _instance;
    }
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance()._reset += ResetScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ResetScene()
    {
        SceneManager.LoadScene(0);
    }
}
