using System.Collections;
using UnityEngine;

namespace VOO
{
    public class RandomlyItemsSpawnInScene : MonoBehaviour
    {
        public Transform rootSpawnItemsInScene;
        public GameObject[] items;
        public float waitForSeconds = 4.0f;
        public float rotationSpeed = 1.0f;
        public float rotationAnglePerFrame = 1.0f;

        private GameObject item;
        private int currentID;

        public static RandomlyItemsSpawnInScene instance;

        private void Start()
        {
            instance = this;
            SpawnItem(0);
        }

        private void Update()
        {
            UpdateItemRotation();
        }

        public void BackItem()
        {
            SpawnItem(GetPreviousIndex());
        }

        public void NextItem()
        {
            SpawnItem(GetNextIndex());
        }

        private void SpawnItem(int id)
        {
            if (items.Length != 0 && id != -1 && id < items.Length)
            {
                Vector3 newPos = Vector3.zero;
                Vector3 newRot = Vector3.zero;

                if (item)
                {
                    newPos = item.transform.localPosition;
                    newRot = item.transform.localEulerAngles;
                    Destroy(item);
                }

                item = (GameObject)Instantiate(items[id], rootSpawnItemsInScene);
                item.transform.localScale = Vector3.one;
                item.transform.localEulerAngles = newRot;
                item.transform.localPosition = newPos;

                Inspect.instance.vspModel = item;
            }
        }

        private void UpdateItemRotation()
        {
            if (item)
            {
                Vector3 newRot = new Vector3(item.transform.localEulerAngles.x, item.transform.localEulerAngles.y + (rotationAnglePerFrame + Time.deltaTime) * rotationSpeed, item.transform.localEulerAngles.z);
                item.transform.localEulerAngles = newRot;
            }
        }

        private int GetNextIndex()
        {
            if (items.Length != 0)
            {
                currentID++;

                if (currentID >= items.Length)
                    currentID = 0;

                return currentID;
            }

            return -1;
        }

        private int GetPreviousIndex()
        {
            if (items.Length != 0)
            {
                currentID--;

                if (currentID < 0)
                    currentID = items.Length - 1;

                return currentID;
            }

            return -1;
        }
    }
}
