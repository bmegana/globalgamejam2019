using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    // Default shootable layer is set to 'Nothing'; be sure to set it
    [SerializeField] LayerMask shootableLayer;
    [SerializeField] Color gunColor;
    [SerializeField] float rotationSpeed = 0.2f;

    LineRenderer gunLine;

	// Use this for initialization
	void Start () {
        gunLine = GetComponent<LineRenderer>();
    }
    
    // Update is called once per frame
    void Update () {
        //Ray2D ray = new Ray2D(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition + transform.position));
        Ray2D ray = new Ray2D(transform.position, transform.right);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        gunLine.startColor = gunColor;
        gunLine.endColor = gunColor;

        Rotate();

        if (Input.GetButton("Fire1")) {
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
                //gunLine.SetPosition(1, ray.origin + hit.point);
                // have gunLine hit environment
                gunLine.enabled = false;
            }
        }
    }

    // TODO: add smoothing
    private void Rotate() {
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMouse.z = transform.position.z;
        transform.right = Vector3.MoveTowards(transform.right, worldMouse - transform.position, rotationSpeed);
    }
}
