using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace People
{
    public class HumansManager : MonoBehaviour
{
        #region SerializedFields
        [Header("Human Setting")]
        [SerializeField] private List<Human> _allHumans = new List<Human>();

        [Header("Human Spawn Prefabs")]
        [SerializeField] private List<Human> _humanPrefabs;

        [Header("SpawnSettings")]
        [SerializeField] private Transform[] _spawnPositions;


        [Header("Managers")]
        [SerializeField] private RegistrationTable _registrationTable;
        [SerializeField] private BedManager _bedManager;

        #endregion

        #region PrivateFields
        private int _maxHumanCount;
        private ObjectPool<Human> _humanPool;
        private WaitForSeconds _waitForCheckSpawnInterval;
        [SerializeField] private float _spawnRate = 3.5f;
        private float _spawnCheckInterval = 3f;
        private float _multiplier = 1.35f;

        #endregion

        #region Properties
        public ObjectPool<Human> Pool {  get { return _humanPool; } }

        #endregion

        #region MonoBehaviour
        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
            EventsManager.Instance.OnPatientLeaveBed += RemovePeopleFromList;
            EventsManager.Instance.OnGameStarted += StartSpawnCoroutine;
            EventsManager.Instance.OnTutorialEnd += StartSpawnCoroutine;
            EventsManager.Instance.OnBedPurchased += RaiseSpawnRate;
        }
        private void OnDisable()
        {
            EventsManager.Instance.OnPatientLeaveBed -= RemovePeopleFromList;
            EventsManager.Instance.OnGameStarted -= StartSpawnCoroutine;
            EventsManager.Instance.OnTutorialEnd -= StartSpawnCoroutine;
            EventsManager.Instance.OnBedPurchased -= RaiseSpawnRate;
        }
        public void Initialize()
        {
            _maxHumanCount = _bedManager.Beds.Count;
            _humanPool = new ObjectPool<Human>(CreatePoolObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _maxHumanCount, _maxHumanCount);
            _waitForCheckSpawnInterval = new WaitForSeconds(_spawnCheckInterval);
        }

        #endregion

        #region Pool
        private Human CreatePoolObject()
        {
            Human human = Instantiate(_humanPrefabs[Random.Range(0, _humanPrefabs.Count)], _spawnPositions[Random.Range(0, _spawnPositions.Length)].transform.position, Quaternion.identity, transform);
            human.gameObject.SetActive(false);
            Debug.Log("Create");
            return human;
        }

        private void OnTakeFromPool(Human human)
        {
            human.gameObject.SetActive(true);
            human.transform.SetParent(transform, true);
            human.EnterInQueue(_registrationTable , _bedManager);
            _allHumans.Add(human);
            Debug.Log("OnTake");
        }

        public void OnReturnToPool(Human human)
        {
            human.gameObject.SetActive(false);
            human.transform.position = _spawnPositions[Random.Range(0, _spawnPositions.Length)].transform.position;
            human.OnHumanEndHealing();
            RemovePeopleFromList(human);
            Debug.Log("OnReturn");
        }

        private void OnDestroyObject(Human human)
        {
            Destroy(human.gameObject);
            Debug.Log("OnDestroy");
        }

        #endregion

        #region Spawn
        private bool CheckSpawnPosibility() 
        {
            if (_allHumans.Count > _maxHumanCount) return false;
            else return true;
        }

        private void RaiseSpawnRate()
        {
            if (_bedManager.CurrentPurchasedBedsCount >= 3 && _bedManager.CurrentPurchasedBedsCount <= 6)
                _spawnRate *= _multiplier;
            else if (_bedManager.CurrentPurchasedBedsCount >= 6 && _bedManager.CurrentPurchasedBedsCount <= 9)
                _spawnRate *= _multiplier;
            else if (_bedManager.CurrentPurchasedBedsCount >= 9 && _bedManager.CurrentPurchasedBedsCount <= 12)
                _spawnRate *= _multiplier;
            else if (_bedManager.CurrentPurchasedBedsCount >= 12)
                _spawnRate *= _multiplier;
        }
        private void StartSpawnCoroutine() => StartCoroutine(SpawnCoroutine());
        public IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < _maxHumanCount; i++)
            {
                if (CheckSpawnPosibility() && GameStateController.Instance.Started && !GameStateController.Instance.FirstPlaySetting && !GameStateController.Instance.Tutorial)
                {
                    yield return new WaitForSeconds(_spawnRate);
                    _humanPool.Get();
                    EventsManager.Instance.OnPatientEnterHospitalEvent(_allHumans.Count);
                }
                else yield break;
            }
            yield return _waitForCheckSpawnInterval;

        }

        private void RemovePeopleFromList(Human human)
        {
            _allHumans.Remove(human);
            EventsManager.Instance.OnPatientLeaveHospitalEvent(_allHumans.Count);
        }

        #endregion
    }

}
