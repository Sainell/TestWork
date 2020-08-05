using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


namespace TestWork
{
    public sealed class GunShoot : MonoBehaviour
    {
        #region Fields

        public GameObject Bullet;
        public GameObject Gun;

        private bool _autoShooting;
        private FirstPersonController _fpsController;
        private float _delay = 0.5f;
        private float _timer;
        private List<GameObject> _bulletPool = new List<GameObject>();

        #endregion


        #region UnityMethods

        private void Awake()
        {
            IdleAttack.SwitchAutoShootingEvent += SwitchAutoShootingHandler;
            _fpsController = GetComponent<FirstPersonController>();
            for (int i = 0; i < 10; i++)
            {
                var temp = Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
                temp.SetActive(false);
                _bulletPool.Add(temp);
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (IsShooting() || _autoShooting)
            {
                if (_timer < _delay)
                {
                    return;
                }
                Shot();
            }
        }

        #endregion


        #region Methods

        private bool IsShooting()
        {
            var isShoot = false;
            if( Input.GetButtonDown("Fire1"))
            {
                isShoot = true;
            }
            return isShoot;
        }

        private void Shot()
        {
            foreach (var bullet in _bulletPool)
            {
                if (!bullet.activeInHierarchy)
                {
                    bullet.transform.position = Gun.transform.position;
                    bullet.transform.rotation = Gun.transform.rotation;
                    bullet.SetActive(true);
                    break;
                }
            }
            _timer = 0;
        }

        private void SwitchAutoShootingHandler(bool isEnable)
        {
            _autoShooting = isEnable;
            _fpsController.enabled = !isEnable;
        }

        #endregion
    }
}