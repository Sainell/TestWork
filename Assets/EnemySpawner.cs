using UnityEngine;


namespace TestWork
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        #region Fields

        public GameObject EnemyPrefab;
        public Transform[] WayPoints;
        private GameObject _instance;
        private Patrol _patrol;

        #endregion


        #region UnityMethods

        private void Awake()
        {
           _patrol = EnemyPrefab.GetComponent<Patrol>();
        }

        private void Update()
        {
            if(!_instance)
            {
                _patrol.WayPoints = WayPoints;
                _instance = Instantiate(EnemyPrefab, transform.position, transform.rotation);
            }
        }

        #endregion
    }
}