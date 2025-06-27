using UnityEngine;

public class Mermi : MonoBehaviour
{
    public int hasar;
    private bool hasarVerildi = false; // �ift hasar �nleme

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasarVerildi && collision.collider.CompareTag("zombi"))
        {
            ZombiHareket zombi = collision.collider.GetComponent<ZombiHareket>();
            if (zombi != null)
            {
                zombi.HasarAl(hasar);
                Debug.Log($"Mermi zombiye �arpt�! Verilen hasar: {hasar}");
            }
            hasarVerildi = true;
            Destroy(gameObject);
        }
        else if (!collision.collider.CompareTag("Player")) // Oyuncuya �arpmad�ysa
        {
            Destroy(gameObject);
        }
    }
}