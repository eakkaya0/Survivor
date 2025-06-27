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

    // Zombi spawn dinamiði için zaman ayarlarý
    private float zamanIlerleme; // Zaman geçtikçe artacak
    private float minSure = 1.0f; // Minimum spawn süresi sýnýrý
    private float maxSure = 5.0f; // Baþlangýçtaki maksimum süre
    private float azalmaHizi = 0.02f; // Spawn hýzlanma oraný

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
            // Zombi oluþtur
            Vector3 pos = new Vector3(Random.Range(381f, 624f), 23.5f, Random.Range(500f, 632f));
            Instantiate(zombi, pos, Quaternion.identity);

            // Zaman geçtikçe spawn süresi azalsýn
            float zamanFaktor = zamanIlerleme / 30f; // Her 60 saniyede etki artsýn
            float yeniMaxSure = Mathf.Max(minSure, maxSure - zamanFaktor * azalmaHizi);
            float yeniMinSure = Mathf.Max(minSure, yeniMaxSure - 1f);

            olusumsureci = Random.Range(yeniMinSure, yeniMaxSure);
            zamansayaci = olusumsureci;

            Debug.Log($"Yeni süre aralýðý: {yeniMinSure:F2} - {yeniMaxSure:F2} | Seçilen süre: {olusumsureci:F2}");
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
