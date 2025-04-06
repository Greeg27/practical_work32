using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private Transform[] _targets;
    private bool[] _busy;

    private System.Random _rnd = new System.Random();

    private void Awake()
    {
        _targets = new Transform[transform.childCount];

        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i] = transform.GetChild(i);
        }

        _busy = new bool[_targets.Length];
    }

    public int TargetSet(int j)
    {
        int i;
        do
        {
            i = _rnd.Next(_targets.Length);
        } while (_busy[i]);

        _busy[i] = true;
        _busy[j] = false;

        return i;
    }

    public Transform TargetTransformGet(int i)
    {
        return _targets[i];
    }
}
