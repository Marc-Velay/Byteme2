using UnityEngine;
using System.Collections;

namespace CompleteProject
{

    public class PlayerAttack : MonoBehaviour
    {

        //objet touché par le rayon de la caméra
        RaycastHit hit;
        Vector3 fwd;
        public float distanceDetection = 100;//++
        public int damagePunch = 10;//++
        


        // Use this for initialization

        void Start()
        {


            fwd = transform.TransformDirection(Vector3.forward);//vecteur "tout droit"

        }

        //++
        // Update is called once per frame
        void Update()
        {

            

            //le détenteur du script à hit un ennemi dans son champ de vision "tout droit", à la distance distanceDetection
            if (Physics.Raycast(transform.position, fwd, out hit, distanceDetection) && hit.collider.tag == "Enemy")
            { 
                print("Enemi dans le champ de vision et à portée !");
                Debug.DrawLine(transform.position, hit.transform.position);//dessine une ligne vers la target 

                //est effectif que si on a appuyé sur une des touches d'attaque (cf ifs)
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    print("Attaque punch (appui touche 1)");
                    Attack(hit);

                }


            }

        }

        //++ attaque l'ennemi ciblé 
        void Attack(RaycastHit hit)
        {
            
            //récupère l'ennemi clone  grâce à son nom
            GameObject enemy = GameObject.Find(hit.collider.name);
            print("Nom de l'ennemi : " + hit.collider.name);
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();//récupère la santé de l'ennemi spotté


            //attack une unique fois à l'appuie d'une touche

            //++ touche 1 : coup de poing
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                print("PUNCH ! enemyhealth before punch : " + enemyHealth.currentHealth);
                //active la fonction takeDamage pour la valeur de damage associée à l'attaque utilisée
                 enemyHealth.TakeDamage(damagePunch,hit.transform.position);
                print("FIN PUNCH ! enemyhealth after punch : " + enemyHealth.currentHealth);
            }

            
        }



    }
}
