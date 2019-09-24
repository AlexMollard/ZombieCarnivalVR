using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oculus;

public class GunBehaviour : MonoBehaviour
{
	public int BulletPoolTotal = 10;
	public GameObject BulletPrefab;
	public GameObject[] BulletPool;
	public Rigidbody[] BulletRB;
	GameObject BulletSpawnPoint;
	int CurrentBulletIndex = -1;
	public float BulletSpeed = 50.0f;
	LineRenderer AimLine;
    public GameObject LineOBJPos;
    public Image RedDot;


	bool IsVR = false;
	void Start()
	{
		AimLine = GetComponent<LineRenderer>();
		IsVR = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote);

		Vector3 BulletSpawntPos = new Vector3(transform.position.x, transform.position.y + 0.035f, transform.position.z);
		Quaternion BulletSpawnRotate = Quaternion.Euler(transform.rotation.x + 90, transform.rotation.y, transform.rotation.z);

		BulletPool = new GameObject[BulletPoolTotal];
		BulletRB = new Rigidbody[BulletPoolTotal];

		BulletSpawnPoint = Instantiate(new GameObject(), BulletSpawntPos, BulletSpawnRotate);
		BulletSpawnPoint.name = "BulletSpawnPoint";
		BulletSpawnPoint.transform.SetParent(transform);

		for (int i = 0; i < BulletPoolTotal; i++)
		{
			BulletPool[i] = Instantiate(BulletPrefab, new Vector3(BulletSpawnPoint.transform.position.x, BulletSpawnPoint.transform.position.y, BulletSpawnPoint.transform.position.z), BulletSpawnPoint.transform.rotation);
			BulletPool[i].name = "Bullet " + i;
			BulletRB[i] = BulletPool[i].GetComponent<Rigidbody>();
			BulletPool[i].SetActive(false);
		}
		
		AimLine.SetPosition(0, new Vector3(LineOBJPos.transform.position.x, LineOBJPos.transform.position.y, LineOBJPos.transform.position.z));
		AimLine.SetPosition(1, new Vector3(LineOBJPos.transform.position.x, LineOBJPos.transform.position.y, LineOBJPos.transform.position.z + 100));

        if (!IsVR)
        {
            AimLine.SetPosition(0, new Vector3(0,0,0));
            AimLine.SetPosition(1, new Vector3(0, 0, 0));
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;

            BulletSpawnPoint.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

	}
	private void Update()
	{
		if (IsVR)
		{
			if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
			{
				GameObject CurrentBullet;
				CurrentBullet = GetBullet();
				CurrentBullet.SetActive(true);
				CurrentBullet.transform.position = BulletSpawnPoint.transform.position;
				BulletRB[CurrentBulletIndex].velocity = transform.forward * BulletSpeed;
				CurrentBullet.transform.up = transform.forward;

			}
            AimLine.SetPosition(0, new Vector3(LineOBJPos.transform.position.x, LineOBJPos.transform.position.y, LineOBJPos.transform.position.z));
            AimLine.SetPosition(1, LineOBJPos.transform.forward * 100.0f);
		}
		else
		{
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                transform.LookAt(hit.point);
                RedDot.transform.position = Camera.main.WorldToScreenPoint(hit.point);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
                


            if (Input.GetMouseButtonDown(0))
			{
				GameObject CurrentBullet;
				CurrentBullet = GetBullet();
				CurrentBullet.SetActive(true);
				CurrentBullet.transform.position = LineOBJPos.transform.position;
				BulletRB[CurrentBulletIndex].velocity = transform.TransformDirection(Vector3.forward) * BulletSpeed;
				//CurrentBullet.transform.up = transform.forward;

			}
		}
    }

	public GameObject GetBullet()
	{

		CurrentBulletIndex++;
		if (CurrentBulletIndex + 1 > BulletPoolTotal)
			CurrentBulletIndex = 0;
		return BulletPool[CurrentBulletIndex];
	}
}
