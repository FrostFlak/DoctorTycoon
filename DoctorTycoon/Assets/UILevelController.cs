using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILevelController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNumber;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Button _button;
        private const string LOCK_ROTATE = "LockRotate";

        public Image LockImage { get { return _lockImage; } }
        public Button Button { get { return _button; } set { _button = value; } }
     
        private bool IsPosibleToStartAnimation()
        {
            if(_levelNumber.gameObject.activeSelf) return false;
            else return true;
        }

        public void StartRotateAnimation(Animator animator)
        {
            if(IsPosibleToStartAnimation())
                animator.Play(LOCK_ROTATE);
        }

        public void OpenLevelUI()
        {
            _lockImage.gameObject.SetActive(false);
            _levelNumber.gameObject.SetActive(true);
        }

        public void LockLevelUI()
        {
            _levelNumber.gameObject.SetActive(false);
            _lockImage.gameObject.SetActive(true);
            _button.interactable = true;
        }

    }

}
