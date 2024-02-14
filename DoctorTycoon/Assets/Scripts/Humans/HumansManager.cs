using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Pool;

namespace People
{
    public class HumansManager : MonoBehaviour
{
        [Header("Human Setting")]
        [SerializeField] private List<Human> _allHumans = new List<Human>();
        [SerializeField] private int _maxHumanCount;

        [Header("Human Spawn Prefabs")]
        [SerializeField] private List<Human> _humanPrefabs;
        [SerializeField] private ManCasual _manPrefab;

        [Header("SpawnSettings")]
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _spawnRate;


        [Header("Managers")]
        [SerializeField] private BedManager _bedManager;

        private ObjectPool<Human> _humanPool;
        private WaitForSeconds _waitForSpawnInterval;
        private WaitForSeconds _waitForCheckSpawnInterval;
        private float _spawnCheckInterval = 5f;

        public ObjectPool<Human> Pool {  get { return _humanPool; } }


        private void Start()
        {
            _maxHumanCount = _bedManager.Beds.Count + 1;
            _humanPool = new ObjectPool<Human>(CreatePoolObject , OnTakeFromPool , OnReturnToPool , OnDestroyObject , false , _maxHumanCount, _maxHumanCount + 1);
            _waitForSpawnInterval = new WaitForSeconds(_spawnRate);
            _waitForCheckSpawnInterval = new WaitForSeconds(_spawnCheckInterval);
            //StartCoroutine(SpawnCoroutine());
            EventsManager.Instance.OnPatientLeaveBed += RemovePeopleFromList;
        }

        private Human CreatePoolObject()
        {
            Human human = Instantiate(_manPrefab, _spawnPosition.transform.position, Quaternion.identity, transform);
            human.gameObject.SetActive(false);
            Debug.Log("Create");
            return human;
        }

        private void OnTakeFromPool(Human human)
        {
            human.gameObject.SetActive(true);
            human.transform.SetParent(transform, true);
            _allHumans.Add(human);
            human.EnterInQueue();
            Debug.Log("OnTake");
        }

        public void OnReturnToPool(Human human)
        {
            human.gameObject.SetActive(false);
            human.transform.position = _spawnPosition.transform.position;
            human.OnHumanEndHealing();
            RemovePeopleFromList(human);
            Debug.Log("OnReturn");
        }

        private void OnDestroyObject(Human human)
        {
            Destroy(human.gameObject);
            Debug.Log("OnDestroy");
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnPatientLeaveBed -= RemovePeopleFromList;
        }

        private bool CheckSpawnPosibility() 
        {
            if (_allHumans.Count > _maxHumanCount)
            {
               // Debug.LogWarning("Too Much People");
                return false;
            }
            else
                return true;
        }

        public void TryToSpawnHuman()
        {
            if (CheckSpawnPosibility())
            {
                _humanPool.Get();
            }
        }

        private IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < _maxHumanCount; i++)
            {
                if (CheckSpawnPosibility())
                {
                    yield return _waitForSpawnInterval;
                    _humanPool.Get();
                    yield return _waitForSpawnInterval;
                }
            }
            yield return _waitForCheckSpawnInterval;
            StartCoroutine(SpawnCoroutine());   
        }

        private void RemovePeopleFromList(Human human)
        {
            _allHumans.Remove(human);
            EventsManager.Instance.OnPatientLeaveHospitalEvent(_allHumans.Count);
        }
        
    }

}
