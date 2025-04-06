using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [Header("Delay parameters")]
    [SerializeField] int danceTimeMin;
    [SerializeField] int danceTimeMax;
    [SerializeField] int barTimeMin;
    [SerializeField] int barTimeMax;
    [SerializeField] int chilloutTimeMin;
    [SerializeField] int chilloutTimeMax;
    [Header("Components")]
    [SerializeField] TargetManager targetManager;
    [SerializeField] Animator animator;
    
    private NavMeshAgent _agent;
    private int _targetNumber;
    private int _animNumber;
    private Transform _target;
    private System.Random _rnd = new System.Random();

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetTarget();
        StartCoroutine(Delay(1));
    }

    private void FixedUpdate()
    {
        _agent.destination = _target.position;
        animator.SetInteger("Transition", _animNumber);
        Moving();
    }

    private void SetTarget()
    {
        _targetNumber = targetManager.TargetSet(_targetNumber);
        _target = targetManager.TargetTransformGet(_targetNumber);
    }

    private void ZoneDefinition()
    {
        int delayTime = 0;
        switch (_target.position.y)
        {
            case < 0:
                delayTime = _rnd.Next(danceTimeMin, danceTimeMax);
                _animNumber = 1;
                break;

            case 0:
                delayTime = _rnd.Next(barTimeMin, barTimeMax);
                _animNumber = 2;
                break;

            case > 0:
                delayTime = _rnd.Next(chilloutTimeMin, chilloutTimeMax);
                _animNumber = 3;
                break;
        }

        StartCoroutine(Delay(delayTime));
    }

    IEnumerator Delay(int s)
    {
        yield return new WaitForSeconds(s);
        SetTarget();
        StopAllCoroutines();
    }

    private void Moving()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            ZoneDefinition();
        }
        else _animNumber = 0;
    }

    //IEnumerator Walk()
    //{

    //    do
    //    {
    //        yield return new WaitForFixedUpdate();
    //        _animNumber = 0;

    //        if (_agent.remainingDistance <= _agent.stoppingDistance)
    //        {
    //            StopAllCoroutines();
    //            ZoneDefinition();
    //        }
    //    } while (true);
    //}
}
