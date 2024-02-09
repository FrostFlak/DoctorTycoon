using Player;
using System.Collections.Generic;
using UnityEngine;

namespace People
{
    public class HumansManager : MonoBehaviour
{
        [Header("Humans")]
        [SerializeField] private List<Human> _allHumans = new List<Human>();
        [SerializeField] private int _maxHumanCount;
        [Header("Human Spawn Prefabs")]
        [SerializeField] private WomanDress _womanDress;
        [SerializeField] private ManCasual _manCasual;
        [SerializeField] private FemaleCasual _femaleCasual;

        [Header("Managers")]
        [SerializeField] private BedManager _bedManager;

        [Header("SpawnSettings")]
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private int _spawnRate;
  
        private void Start()
        {
            _maxHumanCount = _bedManager.Beds.Count + 4;
            EventsManager.Instance.OnPatientLeaveBed += RemovePeople;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnPatientLeaveBed -= RemovePeople;
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
                //SpawnWomanDress();
                //SpawnManCasual();
                SpawnFemaleCasual();


            }
    }

        private void SpawnManCasual()
        {
            ManCasual spawnManCasual = Instantiate(_manCasual, _spawnPosition.transform.position, Quaternion.identity, transform.parent);
            _allHumans.Add(spawnManCasual);
            EventsManager.Instance.OnPatientSpawnedEvent(_allHumans.Count);
        }

        private void SpawnWomanDress()
        {
            var spawnWomanDress = Instantiate(_womanDress, _spawnPosition.transform.position, Quaternion.identity, transform.parent);
            _allHumans.Add(spawnWomanDress);
            EventsManager.Instance.OnPatientSpawnedEvent(_allHumans.Count);
        }
        private void SpawnFemaleCasual()
        {
            FemaleCasual spawnFemaleCasual = Instantiate(_femaleCasual, _spawnPosition.transform.position, Quaternion.identity, transform.parent);
            _allHumans.Add(spawnFemaleCasual);
            EventsManager.Instance.OnPatientSpawnedEvent(_allHumans.Count);
        }

        private void RemovePeople(Human human)
        {
            _allHumans.Remove(human);
            EventsManager.Instance.OnPatientLeaveHospitalEvent(_allHumans.Count);
        }
    }

}
