using Player;
using UnityEngine;
using UnityEngine.Events;

public class MoneyWallet : MonoBehaviour
{

    //change int to long
    public void AddMoney(int count)
    {
        if (TryAddMoney(count))
        {
            SaveSystem._playerData.Money += count;
            EventsManager.Instance.OnMoneyAddedEvent();
        }
    }
    private bool TryAddMoney(long count)
    {
        if(count < long.MaxValue && count >= 0) return true;
        else return false;
    }
}
