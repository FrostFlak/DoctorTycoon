using Player;
using UnityEngine;
using UnityEngine.Events;

public class MoneyWallet : MonoBehaviour
{
    public event UnityAction OnMoneyAdded;

    //change int to long
    public void AddMoney(int count)
    {
        if (TryAddMoney(count))
        {
            SaveSystem._playerData.Money += count;
            OnMoneyAdded?.Invoke();
        }
    }
    private bool TryAddMoney(long count)
    {
        if(count < long.MaxValue && count >= 0) return true;
        else return false;
    }
}
