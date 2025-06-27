using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{
    public GameObject zombi;
    private float zamansayaci;
    private float olusumsureci;
    public Text PuanText;
    private int puan;

    // Zombi spawn dinami�i i�in zaman ayarlar�
    private float zamanIlerleme; // Zaman ge�tik�e artacak
    private float minSure = 1.0f; // Minimum spawn s�resi s�n�r�
    private float maxSure = 5.0f; // Ba�lang��taki maksimum s�re
    private float azalmaHizi = 0.02f; // Spawn h�zlanma oran�

    void Start()
    {
        olusumsureci = Random.Range(minSure, maxSure);
        zamansayaci = olusumsureci;
        zamanIlerleme = 0f;
    }

    void Update()
    {
        zamanIlerleme += Time.deltaTime;
        zamansayaci -= Time.deltaTime;

        if (zamansayaci < 0)
        {
            // Zombi olu�tur
            Vector3 pos = new Vector3(Random.Range(381f, 624f), 23.5f, Random.Range(500f, 632f));
            Instantiate(zombi, pos, Quaternion.identity);

            // Zaman ge�tik�e spawn s�resi azals�n
            float zamanFaktor = zamanIlerleme / 30f; // Her 60 saniyede etki arts�n
            float yeniMaxSure = Mathf.Max(minSure, maxSure - zamanFaktor * azalmaHizi);
            float yeniMinSure = Mathf.Max(minSure, yeniMaxSure - 1f);

            olusumsureci = Random.Range(yeniMinSure, yeniMaxSure);
            zamansayaci = olusumsureci;

            Debug.Log($"Yeni s�re aral���: {yeniMinSure:F2} - {yeniMaxSure:F2} | Se�ilen s�re: {olusumsureci:F2}");
        }
    }

    public void puanArttir(int p)
    {
        puan += p;
        PuanText.text = "Puan: " + puan;
    }

    public void OyunBitti()
    {
        PlayerPrefs.SetInt("puan", puan);
        SceneManager.LoadScene("Bitis");
    }
}
