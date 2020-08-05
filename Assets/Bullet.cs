using System.Collections;
using UnityEngine;


namespace TestWork
{
    public sealed class Bullet : MonoBehaviour
    {
        #region Fields

        private Rigidbody _rigidbody;
        private int _damage = 35;
        private float _lifeTime = 3f;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();            
        }

        private void OnEnable()
        {
            Shot();
        }
        
        #endregion


        #region Methods

        private void Shot()
        {
            _rigidbody.AddForce(transform.forward * 200, ForceMode.Impulse);
            StartCoroutine(Deactivate(_lifeTime));
        }

        private IEnumerator Deactivate(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.tag=="Enemy")
            {
                var d = collision.gameObject.GetComponent<IDamageable>();
                d.GetDamage(_damage);
                gameObject.SetActive(false);
            }
        }

        #endregion
    }
}