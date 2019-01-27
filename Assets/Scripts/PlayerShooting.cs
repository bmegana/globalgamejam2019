using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerShooting : MonoBehaviour {

    // Default shootable layer is set to 'Nothing'; be sure to set it
    [SerializeField] LayerMask shootableLayer;
    [SerializeField] Color gunColor;
    [SerializeField] float rotationSpeed = 0.2f;
    [SerializeField] Gun[] guns;

    LineRenderer gunLine;
    int gunIndex;
    float lastFiredTime = float.MinValue;

	// Use this for initialization
	void Start () {
        gunLine = GetComponent<LineRenderer>();
    }
    
    // Update is called once per frame
    void Update () {
        Gun gun = guns[gunIndex];

        Ray2D ray = new Ray2D(transform.position, transform.right);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        gunLine.startColor = gun.color;
        gunLine.endColor = gun.color;

        Rotate();

        // Shooting
        if (Input.GetButton("Fire1") && Time.time - lastFiredTime >= gun.fireRate) {
            gunLine.SetPosition(0, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, shootableLayer);
            if (hit.collider != null && hit.transform.CompareTag("Enemy")) {
                Debug.Log("Hitting Enemy.");
                print(string.Format("hit {0}", hit.transform.name));
                gunLine.enabled = true;
                gunLine.SetPosition(1, hit.point);
                hit.transform.GetComponent<Enemy>().Death();
            }
            else {
                // mock gunLine hit environment in distance
                gunLine.SetPosition(1, ray.GetPoint(100));
            }

            Invoke("HideGunLine", .1f);
            lastFiredTime = Time.time;
        }

        // Weapon Switching
        // TODO: can add weapon switch timer?
        float weaponSwitchAxis = Input.GetAxis("Mouse ScrollWheel");
        if (weaponSwitchAxis > 0) {
            if (gunIndex == 0) return;
            gunIndex--;
        }
        else if (weaponSwitchAxis < 0) {
            if (gunIndex == guns.Length - 1) return;
            gunIndex++;
        }
        print(string.Format("Using weapon {0}", gun.gunName));
    }

    // TODO: add smoothing
    private void Rotate() {
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMouse.z = transform.position.z;
        transform.right = Vector3.MoveTowards(transform.right, worldMouse - transform.position, rotationSpeed);
    }

    void HideGunLine() {
        gunLine.enabled = false;
    }
}
