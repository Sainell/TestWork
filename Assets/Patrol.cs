using UnityEngine;


namespace TestWork
{
    public sealed class Patrol : MonoBehaviour
    {
        #region Fields

        public Transform[] WayPoints;
        public float Speed = 2f;
        public float RotateSpeed = 20f;

        private float _waitingTime = 1f;
        private float _currentTime = 0;
        private Transform CurrentPosition;
        private int _pointCount = 0;

        #endregion


        #region UnityMethods

        private void Start()
        {
            CurrentPosition = WayPoints[_pointCount].transform;
        }

        private void Update()
        {       
            Walk(CurrentPosition);
            if (CheckPoint(CurrentPosition))
            {
                Timer();
            }
        }

        #endregion


        #region Methods

        private void Walk(Transform point)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, point.position, Speed * Time.deltaTime);
            var target = Quaternion.LookRotation(point.transform.position - gameObject.transform.position);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, target, RotateSpeed * Time.deltaTime);
        }

        private void Timer()
        {
            _currentTime += 1 * Time.deltaTime;
            if (_currentTime >= _waitingTime)
            {
                _pointCount++;
                if (_pointCount >= WayPoints.Length)
                {
                    _pointCount = 0;
                }
                _currentTime = 0;
                CurrentPosition = WayPoints[_pointCount].transform;
            }
        }

        private bool CheckPoint(Transform point)
        {
            if (gameObject.transform.position == point.position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}