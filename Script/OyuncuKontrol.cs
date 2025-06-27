using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyuncuKontrol : MonoBehaviour
{
    public Transform mermiPos;  // Merminin çýkacaðý nokta
    public Image CanImajý;  // Can barý
    private float canDegeri = 100f;
    public OyunKontrol oKontrol;
    private AudioSource aSource;
    public AudioClip Atissesi, Olmesesi, Yaralanmasesi;

    public SilahYonetimi silahYonetimi; // Bunu Inspector'dan atayacaðýz

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AtesEt();
        }
    }

    void AtesEt()
    {
        Silah aktifSilah = silahYonetimi.GetAktifSilah();
        if (aktifSilah == null)
        {
            Debug.LogError("Aktif silah null!");
            return;
        }

        aSource.PlayOneShot(Atissesi, 1f);
        GameObject go = Instantiate(aktifSilah.mermiPrefab, mermiPos.position, mermiPos.rotation);
        go.GetComponent<Rigidbody>().velocity = mermiPos.transform.forward * 30f;
        go.GetComponent<Mermi>().hasar = aktifSilah.hasar;
        Destroy(go.gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("zombi"))
        {
            aSource.PlayOneShot(Yaralanmasesi, 1f);
            canDegeri -= 10f;
            float x = canDegeri / 100f;
            CanImajý.fillAmount = x;
            CanImajý.color = Color.Lerp(Color.red, Color.green, x);

            if (canDegeri<=0)
            {
                aSource.PlayOneShot(Olmesesi, 1f);
                oKontrol.OyunBitti();
            }
        }
    }
}