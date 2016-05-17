using UnityEngine;
using System;

namespace CompleteProject
{
        //Gère les spawns différents des différents types d'ennemis (cf GameObject enemy )
    public class EnemyManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's heatlh.
        public GameObject enemy;                // The enemy prefab to be spawned.
        public float spawnTime = 3f;            // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from. (positions d'apparitions)
        //donc définir plusieurs points sur la carte d'apparitions, ou créer une fonction qui pour chaque ennemi compute une position
        //d'apparition dans le premier demi cercle


        //+:
        static private int indexEnemy = 0;//indice enemy pour pouvoir les repérer entre eux (numéro unique)
        Vector3[] posSpawnPoint;//tous les x y z des spawnpoints
        Vector3 posPlayer;//vector position du player
        public float range = 150;//portée de détection du type d'ennemi concerné par ce script
        public static int nbEnemies=0;//nombre d'ennemis courant
        public int nbEnemiesMax = 2;//nombre d'ennemis maximum


        void Start ()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating ("Spawn", spawnTime, spawnTime);

            GameObject[] sp =  GameObject.FindGameObjectsWithTag("SpawnPoint");//tous les gameobject spawnpoints
            posSpawnPoint = new Vector3[sp.Length];
            for (int i = 0; i < sp.Length; i++)
            {
                posSpawnPoint[i] = sp[i].transform.position; //remplit le tableau des coordonnées des spawnpoints
            }
            posPlayer = GameObject.FindGameObjectWithTag("Player").transform.position;//position du joueur

        }

        //++: 
        //dit si le player est à portée d'un spawnpoint ou non
        bool IsInSphere(Vector3 spawnPoint, Vector3 target) {

            double xo = spawnPoint.x;
            double yo = spawnPoint.y;
            double zo = spawnPoint.z;

            double xTg = target.x;
            double yTg = target.y;
            double zTg = target.z;

            if (Math.Pow(xTg- xo,2) /*+ Math.Pow(yTg - yo, 2) */ + Math.Pow(zTg - zo, 2) <= Math.Pow(range, 2)){
                print("Diana est dans la sphère de portée de rayon !! : " + range+"dist : "+ Math.Pow(xTg - xo, 2) /*+ Math.Pow(yTg - yo, 2) */ + Math.Pow(zTg - zo, 2));
                return true;
            }
            //print("Diana n'est pas dans la sphère de portée de rayon : " + range + "dist : " + Math.Pow(xTg - xo, 2) /*+ Math.Pow(yTg - yo, 2) */ + Math.Pow(zTg - zo, 2));
            return false;

        }

        void Spawn ()
        {
            // If the player has no health left...
            if(playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            posPlayer = GameObject.FindGameObjectWithTag("Player").transform.position;//++ on raffraichit la position du joueur



            //dès qu'on est arrivé au temps de spawn, on regarde pour chaque point de spawn de l'ennemi si diana est in range, si oui on crée un ennemi de ce point
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (IsInSphere(spawnPoints[i].transform.position, posPlayer) && nbEnemies<nbEnemiesMax){//++

                    // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                    Instantiate(enemy, spawnPoints[i].position, spawnPoints[i].rotation);
                    nbEnemies++;//++: +1 au nombre d'ennemis en vie 

                    //++: rajoute un nom à l'ennemi
                    //crée un indice unique à l'ennemy crée et prépare un nouvel indice pour le prochain spawn
                    enemy.name = "enemy" + indexEnemy;
                    indexEnemy++;

                }
                print("Nombres d'ennemis courant : " + nbEnemies);//++
            }

            

        } 
    }
}