using UnityEngine;
using TMPro;

public class ApartmentSwitcher : MonoBehaviour
{
    [Header("Apartments")]
    public GameObject apartment2Room;
    public GameObject apartment3Room;
    public GameObject apartment4Room;

    [Header("UI")]
    public TMP_Text apartmentNameText;

    [Header("Optional References")]
    public Transform xrOrigin;
    public SurfaceSelector surfaceSelector;
    public VRSurfaceSelector vrSurfaceSelector;

    private GameObject[] apartments;
    private string[] apartmentNames;
    private int currentIndex = 0;

    private void Start()
    {
        apartments = new GameObject[]
        {
            apartment2Room,
            apartment3Room,
            apartment4Room
        };

        apartmentNames = new string[]
        {
            "2-Zimmer-Wohnung",
            "3-Zimmer-Wohnung",
            "4-Zimmer-Wohnung"
        };

        ShowApartment(currentIndex);
    }

    public void ShowNextApartment()
    {
        currentIndex++;
        if (currentIndex >= apartments.Length)
            currentIndex = 0;

        ShowApartment(currentIndex);
    }

    public void ShowPreviousApartment()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = apartments.Length - 1;

        ShowApartment(currentIndex);
    }

    public void ShowApartment2Room()
    {
        currentIndex = 0;
        ShowApartment(currentIndex);
    }

    public void ShowApartment3Room()
    {
        currentIndex = 1;
        ShowApartment(currentIndex);
    }

    public void ShowApartment4Room()
    {
        currentIndex = 2;
        ShowApartment(currentIndex);
    }

    private void ShowApartment(int index)
    {
        for (int i = 0; i < apartments.Length; i++)
        {
            if (apartments[i] != null)
                apartments[i].SetActive(i == index);
        }

        if (apartmentNameText != null)
            apartmentNameText.text = apartmentNames[index];

        if (surfaceSelector != null)
            surfaceSelector.currentTarget = null;

        if (vrSurfaceSelector != null)
            vrSurfaceSelector.currentTarget = null;

        if (xrOrigin != null)
        {
            xrOrigin.position = new Vector3(0f, 0f, -2f);
            xrOrigin.rotation = Quaternion.identity;
        }
    }
}