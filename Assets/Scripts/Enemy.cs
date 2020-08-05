using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace TestWork
{
    public sealed class Enemy : MonoBehaviour, IDamageable
    {
        #region Fields

        public static event Action DeathEvent;
        public static event Action DamageEvent;
        public int CurrentHealth;
        public GameObject Coin;

        private int _health = 100;
        private Transform[] _enemyParts;
        private int minForce = 20;
        private int maxForce = 50;
        private int mass = 15;
        private bool isDead;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _enemyParts = new Transform[transform.childCount+1];
            _enemyParts[0] = transform;
            for (int i = 0; i < transform.childCount; i++)
            {
                _enemyParts[i+1] = transform.GetChild(i);
            }
        }

        private void Start()
        {
            CurrentHealth = _health;
        }

        #endregion


        #region Methods

        public void GetDamage(int damage)
        {
            DamageEvent?.Invoke();
            CurrentHealth -= damage;
            if(CurrentHealth<=0)
            {
                DeathEvent?.Invoke();
                Die();
            }
        }

        private void Die()
        {
            if (!isDead)
            {
                isDead = true;
                transform.DetachChildren();
                foreach (var transform in _enemyParts)
                {
                    var rb = transform.gameObject.AddComponent<Rigidbody>();
                    rb.mass = mass;
                    rb.AddForce(Vector3.back * Random.Range(minForce, maxForce), ForceMode.Impulse);
                    Destroy(transform.gameObject, 1f);
                }
                Instantiate(Coin, transform.position, new Quaternion());
            }
        }

        #endregion
    }
}