using System.Collections.Generic;
using UnityEngine;

namespace People
{
    public class HumansManager : MonoBehaviour
{
        [Header("Humans")]
        [SerializeField] private List<Human> _allHumans = new List<Human>();
        [SerializeField] private int _maxHumanCount;
        [Header("Human Prefabs")]
        [SerializeField] private WomanDress _womanDress;
        [SerializeField] private ManCasual _manCasual;

        [Header("Managers")]
        [SerializeField] private BedManager _bedManager;
        private RegistrationTable _registrationTable;

        [Header("SpawnSettings")]
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private int _spawnRate;

  
        private void Start()
        {
            _registrationTable = FindObjectOfType<RegistrationTable>();
            _maxHumanCount = _bedManager.Beds.Count + 5;
        }

        public void Initialize()
        {
         //   AddHumansInList();
        }

        private bool CheckSpawnPosibility() 
        {
            if (_allHumans.Count > _maxHumanCount)
            {
                Debug.LogWarning("Too Much People");
                return false;
            }
            else
                return true;
    }



        public void TryToSpawnHuman()
        {
            if (CheckSpawnPosibility())
            {
                SpawnManCasual();
                
            }
    }

        private void SpawnManCasual()
        {
            ManCasual spawnManCasual = Instantiate(_manCasual, _spawnPosition.transform.position, Quaternion.identity, transform.parent);
            _allHumans.Add(spawnManCasual);
        }

        private void SpawnWomanDress()
        {
            var spawnWomanDress = Instantiate(_womanDress, _spawnPosition.transform.position, Quaternion.identity, transform.parent);
        }

        
    }

}
