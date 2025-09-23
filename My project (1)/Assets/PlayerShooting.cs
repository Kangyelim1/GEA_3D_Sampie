using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject projectilePrefab2;

    public Transform firePoint;
    Camera cam;

    int currentWeapon = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        currentWeapon = 1 - currentWeapon;
    }

    //void Shoot()
    //  {
    //      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //      Vector3 targetPoint;
    //      targetPoint = ray.GetPoint(50f);
    //     Vector3 direction = (targetPoint - firePoint.position).normalized;
    //
    //      GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
    //  }

    void Shoot()
    {
        // 현재 무기에 따라 속도 설정
        if (currentWeapon == 1)
        {
            // ... 기존 코드
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Projectile 스크립트 가져오기
            Projectile projectileScript = proj.GetComponent<Projectile>();

            projectileScript.speed = 10f; // 첫 번째 무기 속도
        }
        else
        {
            // ... 기존 코드
            GameObject proj = Instantiate(projectilePrefab2, firePoint.position, Quaternion.identity);

            // Projectile 스크립트 가져오기
            Projectile projectileScript = proj.GetComponent<Projectile>();
            projectileScript.speed = 20f; // 두 번째 무기 속도
        }

    }

      
}
