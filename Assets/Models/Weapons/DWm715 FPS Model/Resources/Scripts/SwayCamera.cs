using UnityEngine;

namespace VOO
{
    public class SwayCamera : MonoBehaviour
    {
        [Header("ATTRIBUTES")]
        [SerializeField] private float amount = 5f;
        [SerializeField] private float maxAmount = 10f;
        [SerializeField] private float smoothMouseLook = 3f;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.localPosition;
        }

        private void Update()
        {
            float factorX = -Input.GetAxis("Mouse X") * amount;
            float factorY = -Input.GetAxis("Mouse Y") * amount;

            if (factorX > maxAmount)
                factorX = maxAmount;
            else if (factorX < -maxAmount)
                factorX = -maxAmount;
            if (factorY > maxAmount)
                factorY = maxAmount;
            else if (factorY < -maxAmount)
                factorY = -maxAmount;

            Vector3 final = new Vector3(startPosition.x + factorX, startPosition.y + factorY, startPosition.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * smoothMouseLook);
        }
    }
}