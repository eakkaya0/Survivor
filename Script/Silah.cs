using UnityEngine;

public class Silah : MonoBehaviour
{
    public int hasar = 10; // Varsayýlan deðer
    public GameObject mermiPrefab;

    void OnValidate()
    {
        if (mermiPrefab != null && mermiPrefab.GetComponent<Mermi>() == null)
        {
            Debug.LogError($"{name} silahýnýn mermi prefabýnda Mermi scripti yok!", this);
        }
    }
}