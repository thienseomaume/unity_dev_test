using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;
    [SerializeField] Button _buttonKick;
    [SerializeField] Button _buttonAutoKick;
    [SerializeField] Button _buttonReset;
    public static UiManager Instance()
    {
        return _instance;
    }
    private void Awake()
    {
        if (_instance == null)
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
        _buttonKick.onClick.AddListener(EventCenter.Instance().OnKick);
        _buttonAutoKick.onClick.AddListener(EventCenter.Instance().OnAutoKick);
        _buttonReset.onClick.AddListener(EventCenter.Instance().OnReset);
        EventCenter.Instance()._nearBallAction += ToggleButtonKick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleButtonKick(bool active)
    {
        _buttonKick.gameObject.SetActive(active);
    }
}
