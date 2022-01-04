using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radial_Bullet_Pattern : MonoBehaviour
{

    [Header("Cinema")]
    public GameObject LightObj;
    public AudioSource ShootSFX;
    public float ShakeAmount;
    public SpriteRenderer Merender;
    public Vector2 WhereToGo;
    public Vector2 WhereToGo2;

    [Header("Projectile Settings")]
    public int NumberOfProjectiles;
    public float ProjectileSpeed;
    public GameObject ProjectilePrefab;

    [Header("Private Shit")]
    private Vector3 StartPoint;
    private const float radius = 1f;
    private float AddF = 0f;
    private float CoolDown;
    private int Go = 2;


    #region Start
    [System.Serializable]
    public class Pool {public string tag; public GameObject prefab; public int Size;}

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in Pools) 
        {
            Queue<GameObject> ObjectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++) 
            {GameObject obj = Instantiate(pool.prefab); obj.SetActive(false); ObjectPool.Enqueue(obj);}
            poolDictionary.Add(pool.tag, ObjectPool);
        }

    }
    #endregion

    void Update()
    {
        if (Merender.isVisible)
        {
            // Двигай
            if (Go == 2) { transform.position = Vector2.MoveTowards(transform.position, WhereToGo, 0.8f); }
            if (Go == 1) { transform.position = Vector2.MoveTowards(transform.position, WhereToGo2, 0.1f); }

            // Дошел до 1 точки
            if (transform.position.x == WhereToGo.x && transform.position.y == WhereToGo.y && Go == 2) { Go = 1; }

            // Дошел до 2 точки
            if (transform.position.x == WhereToGo2.x && transform.position.y == WhereToGo2.y && Go == 1) { Go = 0; CoolDown = 2f; }

            //Стреляй
            if (CoolDown >= 0 && Go == 0) { CoolDown -= Time.deltaTime; LightObj.SetActive(false); }
            if (CoolDown < 0 && Go == 0) { CoolDown = 0.1f; StartPoint = transform.position; LightObj.SetActive(true); SpawnProjectile(NumberOfProjectiles); }
        }
    }


    #region Pull shit
    public GameObject SpawnFromPool (string tag) 
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = gameObject.transform.position;
        objectToSpawn.transform.rotation = gameObject.transform.rotation;
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    #endregion

    #region Shoot
    private void SpawnProjectile(int _NumberofProj) 
    {
        float AngleStep = 360f / _NumberofProj;
        AddF = AddF + 20f;

        float Angle = 0f + AddF;
        if (ShootSFX != null) {ShootSFX.Play();}

        for (int i = 0; i <= _NumberofProj - 1; i++)
        {
            //Direction calculations
            float projectileDirXPosition = StartPoint.x + Mathf.Sin((Angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = StartPoint.y + Mathf.Cos((Angle * Mathf.PI) / 180) * radius;

            Vector3 ProjectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (ProjectileVector - StartPoint).normalized * ProjectileSpeed;

            GameObject tmpObj = SpawnFromPool("Bullet");
            tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, projectileMoveDirection.y);

            Angle += AngleStep;
        }
    }
    #endregion
}
