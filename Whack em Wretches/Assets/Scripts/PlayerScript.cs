using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody myBody;
    public float rotationSpeed;
    public float speed;
    public float pushStrenght;
    public float pushInflation;
    public float pushCooldown;
    public bool pushAvailable;

    //gère la rotation de la caméra en déterminant la rotation  selon l'axe de la souris et en l'ajoutant à la rotation de la caméra.
    //side note : je vais imploser j'ai galéré si longtemps pour 2 lignes de code.
    void RotateCamera()
    {
        float rotation = rotationSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, rotation, 0);
        myBody = GetComponent<Rigidbody>();
    }

    //Coroutine qui gère la poussée de façon graduelle : d'abord, la poussée monte en intensité, puis elle redescend.
    IEnumerator Pushing(GameObject pushed)
    {
        Debug.Log("Pushing rn");
        float regularPushStrenght = pushStrenght;
        for (int s = 0; s < 5; s++)
        {
            pushStrenght += pushInflation;
            pushed.transform.Translate(transform.TransformDirection(Vector3.forward) * pushStrenght * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.05f);
        }
        for (int i = 0; i < 5; i++)
        {
            pushStrenght -= pushInflation;
            pushed.transform.Translate(transform.TransformDirection(Vector3.forward) * pushStrenght * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
    
      //Gère l'appel de la Coroutie Pushing() grâce )à un raycast qui prend l'objet au centre de la visée, vérifie s'il est valide, puis lui applique la poussée.
    void Push()
    {
        RaycastHit pushHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out pushHit))
        {
            Debug.Log("Push)");
            GameObject collided = pushHit.transform.gameObject;
            if (collided.GetComponent<FurnitureScript>() == true)
            {
                StartCoroutine(Pushing(collided));
            }
        }
    }

    //Méthode qui téléporte un objet devant le joueur avec un petit offset.
    void Pulling(GameObject pulled)
    {
        pulled.transform.position = transform.position + transform.TransformDirection(Vector3.forward) * 2;
    }

    //Méthode qui envoie un raycast et qui pull si l'objet touché est élligible.
    void Pull()
    {
        RaycastHit pushHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out pushHit))
        {
            Debug.Log("Pull");
            GameObject collided = pushHit.transform.gameObject;
            if (collided.GetComponent<EntityScript>() == true)
            {
                Pulling(collided);
            }
        }
    }

    //Coroutine qui gère le cooldown du pull.
    IEnumerator PullCooldown()
    {
        for (int i = 0; i < pushCooldown; i++) 
        {
            yield return new WaitForSecondsRealtime(1);
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