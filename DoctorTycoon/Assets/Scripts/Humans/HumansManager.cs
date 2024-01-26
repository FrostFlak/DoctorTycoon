using System.Collections.Generic;
using UnityEngine;



public class HumansManager : MonoBehaviour
{
    [Header("Humans")]
    [SerializeField] private List<Human> _allHumans = new List<Human>();
    [SerializeField] private List<WomanDress> _womansDress = new List<WomanDress>();
    [SerializeField] private List<ManCasual> _mansCasual = new List<ManCasual>();
    private int _maxHumanCount;
    private int _maxOneTypeHumanCount = 3;
    [Header("Human Prefabs")]
    [SerializeField] private WomanDress _womanDress;
    [SerializeField] private ManCasual _manCasual;

    [Header("Bed")]
    [SerializeField] private BedManager _bedManager;

    [Header("SpawnSettings")]
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private int _spawnRate;
    private void Start()
    {
        _maxHumanCount = _bedManager.Beds.Count + 5;
    }


    private void CheckHumanTypeCount()
    {
        for (int i = 0; i < _allHumans.Count; i++)
        {
            if (_allHumans[i].HumanType == HumanType.WomanDrees)
            {
                _womanDress = (WomanDress)_allHumans[i];
                _womansDress.Add(_womanDress);
                _allHumans.Add(_womanDress);
            }
            else if(_allHumans[i].HumanType == HumanType.ManCasual)
            {
                _manCasual = (ManCasual)_allHumans[i];
                _mansCasual.Add(_manCasual);
                _allHumans.Add(_manCasual);

            }
        }
    }

    private bool CheckSpawnPosibility()
    {
        for(int i = 0; i < _allHumans.Count; i++)
        {
            if (_allHumans.Count < _maxHumanCount)
                return true;
            else 
                return false;
        }
        return false;
    }



    public void TryToSpawnHuman()
    {
        if (CheckSpawnPosibility())
        {
            //CheckHumanTypeCount();
            SpawnManCasual();   
            SpawnWomanDress();

        }
    }

    private void SpawnManCasual()
    {
        var spawnManCasual = Instantiate(_manCasual, _spawnPosition.transform.position, Quaternion.identity, transform.parent);
    }

    private void SpawnWomanDress()
    {
        var spawnWomanDress = Instantiate(_womanDress, _spawnPosition.transform.position, Quaternion.identity, transform.parent);
    }

}
