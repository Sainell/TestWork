using UnityEngine;
using DG.Tweening;


namespace TestWork
{
    public sealed class CameraShake : MonoBehaviour
    {
        #region UnityMethods

        private void Awake()
        {
            Enemy.DamageEvent += Shake;
        }

        #endregion


        #region Methods

        public void Shake()
        {
            gameObject.transform.DOShakeRotation(1, 5, 5);
        }

        #endregion
    }
}