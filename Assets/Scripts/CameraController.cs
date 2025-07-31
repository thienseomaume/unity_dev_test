using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] float _height;
    private GameObject _targetObject;
    // Start is called before the first frame update
    void Start()
    {
        _camera.transform.position = new Vector3(0, _height, 0);
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }
    public void SetTarget(GameObject target, float delayTimeSwitch)
    {
        if(target == null)
        {
            return;
        }
        else
        {
            StartCoroutine(SwitchTarget(target, delayTimeSwitch));
        }
    }
    IEnumerator SwitchTarget(GameObject target, float delayTimeSwitch)
    {
        if (delayTimeSwitch > 0)
        {
            _targetObject = null;
            yield return new WaitForSeconds(delayTimeSwitch);
            _targetObject = target;
        }
        else
        {
            _targetObject = target;
        }
    }
    public void FollowTarget()
    {
        if(_targetObject == null)
        {
            return;
        }
        else
        {
            Vector3 targetPosition = _targetObject.transform.position;
            Vector2 xzTargetPosition = new Vector2(targetPosition.x, targetPosition.z);
            _camera.transform.position = new Vector3(xzTargetPosition.x, _height, xzTargetPosition.y);
        }
    }
}
