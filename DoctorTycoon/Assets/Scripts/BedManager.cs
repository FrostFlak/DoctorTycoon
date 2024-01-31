using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace People
{
    public class BedManager : MonoBehaviour
{
    [SerializeField] private List<Bed> _beds = new List<Bed>();
    public List<Bed> Beds {  get { return _beds; } }
    public static event UnityAction<bool> OnBedBusy;

    private void Start()
    {
    }
    private IEnumerator CheckPlaceDisponibility()
    {
        for (int i = 0; i < _beds.Count; i++)
        {
            if (_beds[i].IsBusy)
            {
                Debug.Log($"Place : {_beds[i].name} is Busy");
            }
            else
            {
                Debug.Log($"Current index : {i}");
                Debug.Log($"Free Place : {_beds[i].name}");
                //_beds.FreePlace = _beds[i].transform.position;
                _beds[i].IsBusy = true;
                yield break;
            }
        }
    }
}

}
