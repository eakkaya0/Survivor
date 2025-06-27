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
        // T�m silahlar� ba�lang��ta kapat
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
        Silahdegisim.text = $"Silah de�i�imine kalan s�re: {dakika:D2}:{saniye:D2}";
    }

    public Silah GetAktifSilah()
    {
        return aktifSilahScript;
    }

    void SilahSec(int index)
    {
        if (index < 0 || index >= silahlar.Length)
        {
            Debug.LogError("Ge�ersiz silah indexi!");
            return;
        }

        // �nceki silah� kapat
        if (aktifSilahScript != null)
        {
            aktifSilahScript.gameObject.SetActive(false);
        }

        // Yeni silah� a�
        aktifSilahIndex = index;
        silahlar[index].SetActive(true);
        aktifSilahScript = silahlar[index].GetComponent<Silah>();

        Debug.Log($"Aktif silah de�i�ti: {silahlar[index].name}, Hasar: {aktifSilahScript.hasar}");
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