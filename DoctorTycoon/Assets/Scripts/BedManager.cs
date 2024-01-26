using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BedManager : MonoBehaviour
{
    [SerializeField] private List<Bed> beds = new List<Bed>();
    public List<Bed> Beds {  get { return beds; } }
    public static event UnityAction<bool> OnBedBusy;

    private void Update()
    {
        FindFreePlaces();
    }
    private void FindFreePlaces()
    {
        foreach (Bed bed in beds)
        {
            if (bed.IsFree())
            {

            }
        }
    }
}
