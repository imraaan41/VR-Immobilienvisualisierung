using UnityEngine;

public class ApartmentManager : MonoBehaviour
{
    [Header("Spawn")]
    public Transform apartmentRoot;

    [Header("Apartment Prefabs")]
    public GameObject apartment2RoomPrefab;
    public GameObject apartment3RoomPrefab;
    public GameObject apartment4RoomPrefab;

    private GameObject currentApartment;

    public void LoadApartment2Room()
    {
        LoadApartment(apartment2RoomPrefab);
    }

    public void LoadApartment3Room()
    {
        LoadApartment(apartment3RoomPrefab);
    }

    public void LoadApartment4Room()
    {
        LoadApartment(apartment4RoomPrefab);
    }

    private void LoadApartment(GameObject apartmentPrefab)
    {
        if (apartmentPrefab == null)
        {
            Debug.LogWarning("Apartment Prefab fehlt.");
            return;
        }

        if (apartmentRoot == null)
        {
            Debug.LogWarning("ApartmentRoot fehlt.");
            return;
        }

        if (currentApartment != null)
        {
            Destroy(currentApartment);
        }

        currentApartment = Instantiate(apartmentPrefab, apartmentRoot);
        currentApartment.transform.localPosition = Vector3.zero;
        currentApartment.transform.localRotation = Quaternion.identity;
        currentApartment.transform.localScale = Vector3.one;

        Debug.Log("Wohnung geladen: " + apartmentPrefab.name);
    }
}