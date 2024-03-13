using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private bool _reached = false;
    [SerializeField] private bool _recivedReward = false;

    public bool Reached { get { return _reached; } set { _reached = value; } }
    public bool RecivedReward {  get { return _recivedReward; } set { _recivedReward = value;} }

    public void RecivedRewardStateToTrue() => _recivedReward = true;
    public void RecivedRewardStateToFalse() => _recivedReward = false;
}
