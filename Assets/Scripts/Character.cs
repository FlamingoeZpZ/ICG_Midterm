using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Vector3 MovementVector;
    [SerializeField] private float health = 100;
    [SerializeField] protected float speed;
    [SerializeField] private float hurtDir = 0.5f;
    [SerializeField] private Projectile bullet;
    [SerializeField] private float timeBetweenShots;
    private float curTime;
    private Coroutine takingDamage;

    private MeshRenderer[] _myRenderers;

    private void Start()
    {
        _myRenderers = GetComponentsInChildren<MeshRenderer>();
    }


    // Update is called once per frame
    private void Update()
    {
        transform.position += Time.deltaTime * speed * MovementVector;

        curTime -= Time.deltaTime;
        if (curTime < 0)
        {
            curTime = timeBetweenShots;
            GameObject go = Instantiate(bullet, transform.position, transform.rotation).gameObject;
            Destroy(go, 10);
            go.layer = gameObject.layer;
        }
    }

    public void TakeDamage(float amt)
    {
        //When the player takes damage
        health -= amt;

        if (health < 0)
        {
            Destroy(gameObject);
            return;
        }

        if (takingDamage != null) return;
        takingDamage =  StartCoroutine(HandleTexture());
    }
    //Gotta be a better way than this right?

    private IEnumerator HandleTexture()
    {
        ColorManager.Reset();
        
        foreach (MeshRenderer renderer in _myRenderers)
        {
            renderer.materials[1] = ColorManager.hurtShader;
        }

        yield return new WaitForSeconds(hurtDir);
        
        foreach (MeshRenderer renderer in _myRenderers)
        {
            renderer.materials[1] = renderer.materials[0];
        }

        takingDamage = null;

    }

}
