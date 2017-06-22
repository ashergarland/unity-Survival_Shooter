using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20; // damage
    public float timeBetweenBullets = 0.15f; // fire rate
    public float range = 100f; // how far the bullets can go

    private float timer; // keeps attacks in sync
    private Ray shootRay = new Ray(); // ray cast of what we hit
    private RaycastHit shootHit; // tells us what we hit
    private int shootableMask; // mask of things we can shoot
    private ParticleSystem gunParticles; // particles 
    private LineRenderer gunLine; // line
    private AudioSource gunAudio; // gun shot
    private Light gunLight; // lighting
    private float effectsDisplayTime = 0.2f; // time for effects


    private void Awake()
    {
        shootableMask = LayerMask.GetMask(LayerMaskNames.Shootable); // level and zombunny are on the shootable layer
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // if it's time to shoot and the player is pressing the fire button
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        // if we fired and enough time has passed then we turn off the effects
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    private void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop(); // don't want to play the particles if they are already playing
        gunParticles.Play();

        gunLine.enabled = true;
        // line starts at the tip of the gun
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward; // shoot the line forward, along the x axis

        // if our ray cast hits something, then what we hit is the end point
        // else  we just choose our long range as the end point
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // whatever we hit, we ask for their enemy health script
            // if they don't have an enemy health script then it's a wall or something else
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
