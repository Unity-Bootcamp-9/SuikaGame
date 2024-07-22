using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaceObject : MonoBehaviour
{
    [SerializeField]
    GameObject m_PrefabToPlace;
    [SerializeField]
    ARRaycastHitEventAsset m_RaycastHitEvent;

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
        Debug.Log("placeable");
        if (installable == false)
        {
            return;
        }

        if (m_SpawnedObject == null)
        {
            m_SpawnedObject = Instantiate(m_PrefabToPlace, hitPose.pose.position, hitPose.pose.rotation, hitPose.trackable.transform.parent);
        }
        else
        {
            m_SpawnedObject.transform.position = hitPose.pose.position;
            m_SpawnedObject.transform.parent = hitPose.trackable.transform.parent;
        }
    }

    public GameObject GetSpawnedGameObject()
    {
        return m_SpawnedObject;
    }
}
