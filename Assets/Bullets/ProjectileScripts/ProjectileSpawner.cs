using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> projectileFx = new List<GameObject>();


    private GameObject projectileToSpawn;
    private int projectileTypeId = 0;
    private Quaternion rotation;
    private float timeToFire = 0.0f;

    public AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        if (audiosource == null)
        {
            Debug.Log("Projectile Spawner has no Audio Source!");
        }

        if (firePoint == null)
        {
            Debug.Log("No Fire Point on Projectile Spawner!");
        }

        if (projectileFx.Count == 0)
        {
            Debug.Log("No assigned projectile FX on Projectile Spawner!");
        }

        projectileToSpawn = projectileFx[projectileTypeId];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            // Wrap forwards
            projectileTypeId = (projectileTypeId + 1) % projectileFx.Count;
            projectileToSpawn = projectileFx[projectileTypeId];
            // Debug.Log(projectileToSpawn);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            // Wrap backwards
            projectileTypeId = (projectileFx.Count + projectileTypeId - 1) % projectileFx.Count;
            projectileToSpawn = projectileFx[projectileTypeId];
            // Debug.Log(projectileToSpawn);
        }
        
            if (Input.GetMouseButton(0) && Time.time >= timeToFire)
            {
                var newProjectile = projectileToSpawn.GetComponent<ProjectileData>();

                timeToFire = Time.time + 1 / newProjectile.fireRate;

                if (newProjectile.shootSound != null)
                {
                    audiosource.clip = newProjectile.shootSound;
                    audiosource.pitch = Random.Range(newProjectile.minPitch, newProjectile.maxPitch);
                    audiosource.PlayOneShot(audiosource.clip);
                }

                SpawnProjectile();
            }  
    }

    void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectileToSpawn, firePoint.transform.position, Quaternion.identity);

        ProjectileData projectileData = projectile.GetComponent<ProjectileData>();

        float projectileLifetime = 2.0f;

        if (projectileData != null)
        {
            projectileLifetime = projectileData.maxLifetime;
        }

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        rotation = Quaternion.LookRotation(dir);

        projectile.transform.localRotation = rotation;
        Destroy(projectile, projectileLifetime);
    }

    public Quaternion GetDirection()
    {
        return rotation;
    }
}
