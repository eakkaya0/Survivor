using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SilahYonetimi : MonoBehaviour
{
    public GameObject[] silahlar;
    private int aktifSilahIndex = 0;
    private Silah aktifSilahScript;

    public Text Silahdegisim;
    private float silahDegisimSuresi =300f;
    private float kalanSure;

    void Start()
    {
        // Tüm silahlarý baþlangýçta kapat
        foreach (var silah in silahlar)
        {
            silah.SetActive(false);
        }

        SilahSec(aktifSilahIndex);
        StartCoroutine(SilahDegistirRutini());
    }

    void Update()
    {
        kalanSure -= Time.deltaTime;
        if (kalanSure < 0) kalanSure = 0;

        int dakika = Mathf.FloorToInt(kalanSure / 60f);
        int saniye = Mathf.FloorToInt(kalanSure % 60f);
        Silahdegisim.text = $"Silah deðiþimine kalan süre: {dakika:D2}:{saniye:D2}";
    }

    public Silah GetAktifSilah()
    {
        return aktifSilahScript;
    }

    void SilahSec(int index)
    {
        if (index < 0 || index >= silahlar.Length)
        {
            Debug.LogError("Geçersiz silah indexi!");
            return;
        }

        // Önceki silahý kapat
        if (aktifSilahScript != null)
        {
            aktifSilahScript.gameObject.SetActive(false);
        }

        // Yeni silahý aç
        aktifSilahIndex = index;
        silahlar[index].SetActive(true);
        aktifSilahScript = silahlar[index].GetComponent<Silah>();

        Debug.Log($"Aktif silah deðiþti: {silahlar[index].name}, Hasar: {aktifSilahScript.hasar}");
    }

    IEnumerator SilahDegistirRutini()
    {
        while (true)
        {
            kalanSure = silahDegisimSuresi;
            yield return new WaitForSeconds(silahDegisimSuresi);

            int yeniIndex = (aktifSilahIndex + 1) % silahlar.Length;
            SilahSec(yeniIndex);
        }
    }
}