using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private static BallManager _instance;
    [SerializeField] float _kickForce;
    [SerializeField] float _ballRadius;
    [SerializeField] LayerMask _goalLayer;
    [SerializeField] List<GameObject> _listBall;
    [SerializeField] GameObject _player;
    [SerializeField] CameraController _cameraController;
    [SerializeField] List<GameObject> _listGoal;
    [SerializeField] GameObject _starParticle;
    bool isFlying;
    public static BallManager Instance()
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
        EventCenter.Instance()._kick += KickBallNearest;
        EventCenter.Instance()._autoKick += KickBallFarthest;
        _cameraController.SetTarget(_player, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void KickBallNearest()
    {
        if (isFlying) return;
        Debug.Log("kicked");
        GameObject nearestBall =null;
        GameObject nearestGoal = null;
        float minDistance = Mathf.Infinity;
        foreach(GameObject ball in _listBall)
        {
            float distanceToPlayer = Vector3.Distance(ball.transform.position, _player.transform.position);
            if ( distanceToPlayer< minDistance)
            {
                minDistance = distanceToPlayer;
                nearestBall = ball;
            }
        }
        minDistance = Mathf.Infinity;
        foreach(GameObject goal in _listGoal)
        {
            float distanceToGoal = Vector3.Distance(nearestBall.transform.position, goal.transform.position);
            if (distanceToGoal < minDistance)
            {
                minDistance = distanceToGoal;
                nearestGoal = goal;
            }
        }
        Kick(nearestBall, nearestGoal);
    }
    public void KickBallFarthest()
    {
        if (isFlying) return;
        GameObject farthestBall = null;
        GameObject nearestGoal = null;
        float maxDistance = 0;
        foreach (GameObject ball in _listBall)
        {
            float distanceToPlayer = Vector3.Distance(ball.transform.position, _player.transform.position);
            if ( distanceToPlayer > maxDistance)
            {
                maxDistance = distanceToPlayer;
                farthestBall = ball;
            }
        }
        float minDistance = Mathf.Infinity;
        foreach (GameObject goal in _listGoal)
        {
            float distanceToGoal = Vector3.Distance(farthestBall.transform.position, goal.transform.position);
            if (distanceToGoal < minDistance)
            {
                minDistance = distanceToGoal;
                nearestGoal = goal;
            }
        }
        Kick(farthestBall, nearestGoal);
    }
    public void Kick(GameObject ball, GameObject goal)
    {
        Vector3 force = (goal.transform.position - ball.transform.position).normalized * _kickForce;
        ball.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        isFlying = true;
        StartCoroutine(TrackingBall(ball, goal));
    }
    IEnumerator TrackingBall(GameObject ball, GameObject goal)
    {
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();
        _cameraController.SetTarget(ball, 0);
        float timer = 5;//max time for camera follow ball
        while (true)
        {
            if(Physics.CheckSphere(ball.transform.position, _ballRadius, _goalLayer))
            {
                Instantiate(_starParticle, goal.transform.position, Quaternion.identity);
                break;
            }
            timer -= Time.deltaTime;
            if(timer<=0) break;
            yield return null;
        }
        isFlying = false;
        _cameraController.SetTarget(_player, 2);
    }
}
