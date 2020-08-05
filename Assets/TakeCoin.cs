using UnityEngine;


namespace TestWork
{
    public sealed class TakeCoin : MonoBehaviour
    {
        #region Fields

        private float _rotationSpeed = 100f;
        private float _rotationMultiplier = 5f;

        #endregion


        #region UnityMethods

        private void Update()
        {
            gameObject.transform.Rotate(Vector3.up, _rotationSpeed * _rotationMultiplier * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}