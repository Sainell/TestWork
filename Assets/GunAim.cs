using UnityEngine;


namespace TestWork
{
    public sealed class GunAim : MonoBehaviour
    {
        #region Fields

        public Transform Target;

        #endregion


        #region UnityMethods

        private void Update()
        {
            transform.LookAt(Target);
        }

        #endregion
    }
}