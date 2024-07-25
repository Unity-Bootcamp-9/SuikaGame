using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class ARPlaceObject : MonoBehaviour
{
    [SerializeField]
    GameObject m_PrefabToPlace;
    [SerializeField]
    ARRaycastHitEventAsset m_RaycastHitEvent;

    [SerializeField]
    private float maxDistance;

    public bool installable;

    GameObject m_SpawnedObject;

    void OnEnable()
    {
        if (m_RaycastHitEvent == null || m_PrefabToPlace == null)
        {
            Debug.LogWarning($"{nameof(ARPlaceObject)} component on {name} has null inputs and will have no effect in this scene.", this);
            return;
        }

        if (m_RaycastHitEvent != null)
            m_RaycastHitEvent.eventRaised += PlaceObjectAt;
    }

    void OnDisable()
    {
        if (m_RaycastHitEvent != null)
            m_RaycastHitEvent.eventRaised -= PlaceObjectAt;
    }

    void PlaceObjectAt(object sender, ARRaycastHit hitPose)
    {
        if (installable == false)
        {
            return;
        }

        Vector3 spawnPosition = hitPose.pose.position;
        Vector3 cameraPosition = Camera.main.transform.position;
        float distance = Vector3.Distance(cameraPosition, spawnPosition);

        if (distance > maxDistance)
        {
            Vector3 direction = (spawnPosition - cameraPosition).normalized;
            Vector3 clampedPosition = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z) + direction * maxDistance;
            clampedPosition.y = spawnPosition.y;
            spawnPosition = clampedPosition;
        }

        if (m_SpawnedObject == null)
        {
            m_SpawnedObject = Instantiate(m_PrefabToPlace, spawnPosition, hitPose.pose.rotation, hitPose.trackable.transform.parent);
        }
        else
        {
            m_SpawnedObject.transform.position = spawnPosition;
            m_SpawnedObject.transform.parent = hitPose.trackable.transform.parent;
        }
    }

    public GameObject GetSpawnedGameObject()
    {
        return m_SpawnedObject;
    }
}
