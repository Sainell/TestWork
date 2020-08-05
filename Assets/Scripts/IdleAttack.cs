using System;
using UnityEngine;


namespace TestWork
{
    public sealed class IdleAttack : MonoBehaviour
    {
        #region Fields

        public static event Action<bool> SwitchAutoShootingEvent;
        public Transform StartPosition;

        private float _waitingTime = 5f;
        private float _currentTime = 0f;
        private float _rotateSpeed = 30f;
        private GameObject[] _enemyList;
        private GameObject _aimTarget;
        private bool isAiming;
        [SerializeField]
        private Transform _currentTarget;
        [SerializeField]
        private float _currentDistance;
        private float _mouseMove;
        private float _move;
        private float _multiplier = 100f;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _aimTarget = transform.Find("Camera/AimTarget").gameObject;
        }

        private void Update()
        {
            if (!isMove())
            {
                Timer();
                if (_currentTarget != null)
                {
                    AttackTarget(_currentTarget.transform);
                }
                else
                {
                    ClearTargets();
                }
            }
            else
            {
                if (_currentTarget != null)
                {
                    ClearTargets();
                }
                _currentTime = 0;
            }
        }

        #endregion


        #region Methods

        private bool isMove()
        {
            _mouseMove =  Input.GetAxis("Mouse X") *  Input.GetAxis("Mouse Y") * _multiplier;
            _move = Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") * _multiplier;
            if (_move != 0 || _mouseMove !=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Timer()
        {

            if (_currentTime < _waitingTime)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                SearchTarget();
            }
        }

        private void SearchTarget()
        {
            _enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < _enemyList.Length; i++)
            {
                var tempDistance = (_enemyList[i].transform.position - gameObject.transform.position).magnitude;
                if (_currentDistance == 0 || _currentDistance > tempDistance)
                {
                    _currentDistance = tempDistance;
                    _currentTarget = _enemyList[i].transform;
                    isAiming = true;
                }
            }
        }

        private void AttackTarget(Transform target)
        {
            _aimTarget.transform.position = target.transform.position;
            var targetQuaternion = Quaternion.LookRotation(target.transform.position - gameObject.transform.position);
            targetQuaternion.z = 0;
            targetQuaternion.x = 0;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetQuaternion, _rotateSpeed * Time.deltaTime);
            SwitchAutoShootingEvent?.Invoke(true);
        }

        private void ClearTargets()
        {
            SwitchAutoShootingEvent?.Invoke(false);
            _currentTarget = null;
            _currentDistance = 0;
            _enemyList = null;
            if (isAiming)
            {
                _aimTarget.transform.position = StartPosition.position;
                isAiming = false;
            }
        }

        #endregion
    }
}