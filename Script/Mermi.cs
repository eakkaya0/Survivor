using UnityEngine;

public class Mermi : MonoBehaviour
{
    public int hasar;
    private bool hasarVerildi = false; // Çift hasar önleme

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasarVerildi && collision.collider.CompareTag("zombi"))
        {
            ZombiHareket zombi = collision.collider.GetComponent<ZombiHareket>();
            if (zombi != null)
            {
                zombi.HasarAl(hasar);
                Debug.Log($"Mermi zombiye çarptý! Verilen hasar: {hasar}");
            }
            hasarVerildi = true;
            Destroy(gameObject);
        }
        else if (!collision.collider.CompareTag("Player")) // Oyuncuya çarpmadýysa
        {
            Destroy(gameObject);
        }
    }
}