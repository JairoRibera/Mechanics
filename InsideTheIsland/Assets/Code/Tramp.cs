using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tramp : MonoBehaviour
{
    public Rigidbody2D rB;
    //Creamos una variable para darle un nombre al enemigo
    string enemyName;
    

    public void Start()
    {
        //Destroy(gameObject, 5f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.position = transform.position;
            collision.gameObject.GetComponent<EnemyController>().canMove = false;
            collision.gameObject.GetComponent<EnemyController>().rB.velocity = Vector2.zero;
            //hacemos que la variable enemyName sea el nombre del objeto que a entrado en la trampa
            enemyName = collision.gameObject.name;
            //Iniciamos la corrutina
            StartCoroutine(MakeEnemyMoveCo());
      
        }
       
    }


    private IEnumerator MakeEnemyMoveCo()
    {
        //Creamos una Corrutina para que se desactive la trampa cuando pase 3 segundos
        yield return new WaitForSeconds(3f);
        //desactivamos la trampa 
        Destroy(gameObject);
        //gameObject.SetActive(false);
        //Buscamos el objeto con el nombre de la variable y hacemos que se pueda mover se pueda mover
        GameObject.Find(enemyName).GetComponent<EnemyController>().canMove = true;
    }
}
