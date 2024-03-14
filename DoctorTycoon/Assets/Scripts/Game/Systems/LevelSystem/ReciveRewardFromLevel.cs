using UI;
using UnityEngine;

namespace Player
{
    public class ReciveRewardFromLevel : MonoBehaviour
    {
        [SerializeField] private UILevelController _uiLevelController;
        [SerializeField] private MoneyWallet _moneyWallet;
        public void ReciveMoney(int moneyCount)
        {
            if(!_uiLevelController.LockImage.gameObject.activeSelf)
                _moneyWallet.AddMoney(moneyCount);
        }
        public void RecivePills(int pillsCount)
        {
            if (!_uiLevelController.LockImage.gameObject.activeSelf)
                _moneyWallet.AddPills(pillsCount);
        }

    }

}
