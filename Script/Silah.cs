using UnityEngine;

public class Silah : MonoBehaviour
{
    public int hasar = 10; // Varsay�lan de�er
    public GameObject mermiPrefab;

    void OnValidate()
    {
        if (mermiPrefab != null && mermiPrefab.GetComponent<Mermi>() == null)
        {
            Debug.LogError($"{name} silah�n�n mermi prefab�nda Mermi scripti yok!", this);
        }
    }
}