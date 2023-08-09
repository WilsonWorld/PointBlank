using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace VOO
{
    public class Inspect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Inspect Parameters")]
        public float scrollSpeed = 10f;
        public Vector2 sensitivityRot = new Vector2(4, 4);      // Sensitivity of the review
        public Vector2 sensitivityMove = new Vector2(4, 4);
        public Vector2 limitRotX = new Vector2(-360, 360);      // Rotation constraints of the model
        public Vector2 limitRotY = new Vector2(-360, 360);      // Rotation constraints of the model
        public Vector2 limitRotZ = new Vector2(-360, 360);      // Rotation constraints of the model
        public Vector2 limitPosX = new Vector2(310f, -100f);

        [Header("Light")]
        public GameObject lightScene;
        public Slider lightSlider;

        [Header("Menu")]
        public Animator menuAnimator;
        public Slider itemRotSlider;
        public Slider lightIntensitySlider;
        public GameObject[] hidingObjects;
        public Toggle hideIcons;

        [Header("DEBUG")]
        public GameObject vspModel;
        public Vector3 modelPosition = Vector3.zero;

        private bool isActive;

        public static Inspect instance;

        private void Awake()
        {
            instance = this;

            menuAnimator.SetBool("itWasOpened", true);
            menuAnimator.SetBool("isOpened", false);
        }

        private void Update()
        {
            if(isActive)
                Update3DView();
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isActive = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isActive = false;
        }

        public void UpdateMenuPosition()
        {
            if (menuAnimator.GetBool("itWasOpened"))
            {
                menuAnimator.SetBool("itWasOpened", false);
                menuAnimator.SetBool("isOpened", false);
            }
            else
                menuAnimator.SetBool("isOpened", !menuAnimator.GetBool("isOpened"));

            menuAnimator.SetBool("canChangeState", true);
        }

        #region Slider

        public void UpdateLightRotation()
        {
            float delta = 360.0f * lightSlider.value;

            lightScene.transform.localEulerAngles = new Vector3(lightScene.transform.localEulerAngles.x, delta, lightScene.transform.localEulerAngles.z);
        }

        public void UpdateItemRotation()
        {
            RandomlyItemsSpawnInScene.instance.rotationSpeed = itemRotSlider.value;
        }

        public void UpdateLightIntensity()
        {
            lightScene.GetComponent<Light>().intensity = lightIntensitySlider.value;
        }

        public void UpdateVisibleIcons()
        {
            if (hidingObjects == null && hidingObjects.Length == 0)
                return;

            foreach (GameObject icon in hidingObjects)
                icon.SetActive(!hideIcons.isOn);
        }

        #endregion

        private void Update3DView()
        {
            if (Input.GetMouseButton(0))
            {
                Quaternion m_ObjectTargetRot = vspModel.transform.localRotation;

                float xRot = Input.GetAxis("Mouse Y") * sensitivityRot.x;
                float yRot = Input.GetAxis("Mouse X") * sensitivityRot.y;

                m_ObjectTargetRot *= Quaternion.Euler(xRot * 0.25f, -yRot, xRot);

                m_ObjectTargetRot = ClampRotationAroundAxis(m_ObjectTargetRot, limitRotX, 0);
                m_ObjectTargetRot = ClampRotationAroundAxis(m_ObjectTargetRot, limitRotY, 1);
                m_ObjectTargetRot = ClampRotationAroundAxis(m_ObjectTargetRot, limitRotZ, 2);

                vspModel.transform.localRotation = m_ObjectTargetRot;
            }

            //
            // Scroll
            //

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                modelPosition.x -= scrollSpeed;

                if (modelPosition.x < limitPosX.y)
                    modelPosition.x = limitPosX.y;

                vspModel.transform.localPosition = modelPosition;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                modelPosition.x += scrollSpeed;

                if (modelPosition.x > limitPosX.x)
                    modelPosition.x = limitPosX.x;

                vspModel.transform.localPosition = modelPosition;
            }

            //
            // Move
            //

            if (Input.GetMouseButton(2))
            {
                float yPos = Input.GetAxis("Mouse Y") * sensitivityMove.y;
                float xPos = Input.GetAxis("Mouse X") * sensitivityMove.x;

                modelPosition.y += yPos;
                modelPosition.z += xPos;

                vspModel.transform.localPosition = modelPosition;
            }
        }

        private Quaternion ClampRotationAroundAxis(Quaternion q, Vector2 limitRotation, int axisIndex)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angle = 0f;

            if (axisIndex == 0) // X
            {
                angle = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
                angle = Mathf.Clamp(angle, limitRotation.x, limitRotation.y);
                q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);
            }
            else if (axisIndex == 1) // Y
            {
                angle = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);
                angle = Mathf.Clamp(angle, limitRotation.x, limitRotation.y);
                q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);
            }
            else if (axisIndex == 2) // Z
            {
                angle = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);
                angle = Mathf.Clamp(angle, limitRotation.x, limitRotation.y);
                q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);
            }

            return q;
        }
    }
}
