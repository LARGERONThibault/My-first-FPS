using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody myBody;
    Transform mytransform;
    public float health;
    public float rotationSpeed;
    public float speed;
    public float pushStrenght;
    public float pushInflation;
    public float pushCooldown;
    public bool pushAvailable;
    public float pullCooldown;
    public bool pullAvailable;
    public float furniturePushBoost;
    public float furniturePushInflationBoost;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        mytransform = transform;
    }

    //gère la rotation de la caméra en déterminant la rotation  selon l'axe de la souris et en l'ajoutant à la rotation de la caméra.
    //side note : je vais imploser j'ai sué si longtemps pour 2 lignes de code.
    void RotateCamera()
    {
        float rotation = rotationSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, rotation, 0);
    }

    //void
    IEnumerator Pushing(GameObject pushed)
    {
        //Sauvegarde des valeurs initiales du push a cas où elles seraient changées.
        float initialspeed = pushStrenght;
        float initialinflation = pushInflation;
        //Création de l'espace de stockage pour la vitesse de l'ennemi afin de pouvoir l'immobiliser en la concervant.
        float enemyspeed = 0;
        //Si c'est un ennemi, on sauvegarde sa vitesse.
        if (pushed.GetComponent<EnemyScript>() == true)
        {
            enemyspeed = pushed.GetComponent<EnemyScript>().speed;
            pushed.GetComponent<EnemyScript>().speed = 0;
        }
        //Si c'est un meuble, on augmente la puissance du push et on envoie l'information au script du meuble qu'il est poussé pour son prorpe code.
        else if (pushed.GetComponent<FurnitureScript>() == true)
        {
            pushStrenght += furniturePushBoost;
            pushInflation += furniturePushInflationBoost;
            pushed.GetComponent<FurnitureScript>().isPushed = true;
        }

        //Définition du code qui remet à la norme toutes les valeurs modifiées durant la Coroutine.
        void ShutdownSafety()
        {
            if (pushed.GetComponent<EnemyScript>() == true)
            {
                pushed.GetComponent<EnemyScript>().speed = enemyspeed;
            }
            if (pushed.GetComponent<FurnitureScript>() == true)
            {
                pushStrenght = initialspeed;
                pushInflation = initialinflation;
                pushed.GetComponent<FurnitureScript>().isPushed = false;
            }
        }

        //D'abord, pousse de plus en plus fort l'ennemi, puis de moins en moins fort.
        if (pushed.GetComponent<FurnitureScript>() == true || pushed.GetComponent<EnemyScript>() == true)
        {
            float regularPushStrenght = pushStrenght;
            for (int s = 0; s < 5; s++)
            {
                pushStrenght += pushInflation;
                pushed.transform.Translate(mytransform.TransformDirection(Vector3.forward) * pushStrenght * Time.deltaTime);
                yield return new WaitForSecondsRealtime(0.05f);
            }
            for (int i = 0; i < 5; i++)
            {
                pushStrenght -= pushInflation;
                pushed.transform.Translate(mytransform.TransformDirection(Vector3.forward).normalized * pushStrenght * Time.deltaTime);
                yield return new WaitForSecondsRealtime(0.05f);
            }
            pushStrenght = regularPushStrenght;
        }
        ShutdownSafety();
    }

    void Push()
    {
        RaycastHit pushHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out pushHit))
        {
            Debug.Log("Push)");
            GameObject collided = pushHit.transform.gameObject;
            if (collided.GetComponent<EnemyScript>() == true && pushAvailable == true)
            {
                StartCoroutine(Pushing(collided));
                pushAvailable = false;
                StartCoroutine(PushCooldown());
            }
        }
    }

    void Pulling(GameObject pulled)
    {
        pulled.transform.position = transform.position + transform.TransformDirection(Vector3.forward) * 2;
    }

    void Pull()
    {
        RaycastHit pushHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out pushHit))
        {
            Debug.Log("Pull");
            GameObject collided = pushHit.transform.gameObject;
            if (collided.GetComponent<EnemyScript>() == true && pullAvailable == true)
            {
                Pulling(collided);
                pullAvailable = false;
                StartCoroutine(PullCooldown());
            }
        }
    }

    //Coroutine qui gère le cooldown du pull.
    IEnumerator PullCooldown()
    {
        for (int i = 0; i < pullCooldown; i++) 
        {
            yield return new WaitForSecondsRealtime(1);
            Debug.Log(i);
        }
        pullAvailable = true;
    }
    
    //Coroutine qui gère le cooldown du pull.
    IEnumerator PushCooldown()
    {
        for (int i = 0; i < pushCooldown; i++)
        {
            yield return new WaitForSecondsRealtime(1);
            Debug.Log(i);
        }
        pushAvailable = true;
    }


    void Update()
    {

        
        //rotate la caméra si la souris est bougée avec assez d'intensité.
        if (Input.GetAxis("Mouse X") < 0.1 || Input.GetAxis("Mouse X") > 0.1) 
        { 
            RotateCamera();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Push();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Pull();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            health -= 20;
        }
    }

    private void LateUpdate()
    {
        //Mouvements.
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (health == 0) { Application.Quit(); }
    }
}
/*
    void QuatRotateCamera()
        
    {
        
        //Saves camera rotation and adds speed*mouseaxis.
        float currentAxis = transform.rotation.y;
        currentAxis = (currentAxis+(rotationSpeed * Input.GetAxis("Mouse X")) * Time.deltaTime);
        //Checks and fixes impossible rotations.
        if (currentAxis > 180)
        {
            float difference = currentAxis - 180;
            currentAxis = -180 + difference;
        }
        if (currentAxis < -180)
        {
            float difference = currentAxis + 180;
            currentAxis = 180 - difference;
        }
        transform.rotation = new Quaternion (transform.rotation.x, currentAxis, transform.rotation.z, transform.rotation.w);
        
    }
        */

/*
void EulerRotateCamera()
{
    float currentAxis = transform.eulerAngles.y;
    currentAxis = (currentAxis+(rotationSpeed * Input.GetAxis("Mouse X"))*Time.deltaTime);
    transform.eulerAngles = new Vector3(0, currentAxis, 0);
}
*/