using UnityEngine;
using UnityEngine.AI;

public class ZombiHareket : MonoBehaviour
{
    private GameObject Oyuncu;
    private int zombican = 100;
    private OyunKontrol Okontrol;
    private bool oldumu = false;
    private AudioSource aSource;
    private NavMeshAgent agent;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        Oyuncu = GameObject.FindWithTag("Player"); // Daha güvenli bulma yöntemi
        Okontrol = GameObject.Find("_script").GetComponent<OyunKontrol>();
    }

    void Update()
    {
        if (oldumu) return;

        if (Oyuncu != null)
        {
            agent.SetDestination(Oyuncu.transform.position);

            float mesafe = Vector3.Distance(transform.position, Oyuncu.transform.position);

            Animation anim = GetComponentInChildren<Animation>();

            if (mesafe < 10f)
            {
                if (!aSource.isPlaying) aSource.Play();
                if (!anim.IsPlaying("Zombie_Attack_01"))
                {
                    anim.Play("Zombie_Attack_01");
                }
            }
            else
            {
                if (!aSource.isPlaying) aSource.Stop();

                // Yürüme animasyonu sadece baþka animasyon çalmýyorsa oynatýlýyor
                if (!anim.IsPlaying("Zombie_Walk_01"))
                {
                    anim.Play("Zombie_Walk_01");
                }
            }
        }
    }


    public void HasarAl(int hasar)
    {
        if (oldumu) return;

        zombican -= hasar;
        Debug.Log($"Zombi hasar aldý! Kalan can: {zombican}");

        if (zombican <= 0)
        {
            oldumu = true;
            agent.isStopped = true;
            GetComponentInChildren<Animation>().Play("Zombie_Death_01");
            Destroy(gameObject, 1.5f);
            Okontrol.puanArttir(10);
        }
    }
}