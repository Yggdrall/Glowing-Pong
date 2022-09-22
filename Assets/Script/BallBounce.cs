using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    // Start is called before the first frame update
        public BallMouvement BallMouvement;
        public ScoreManager scoreManager;
        public GameObject hitSFX;

        private void Bounce(Collision2D collision){

            Vector3 ballPosition = transform.position;
            Vector3 racketPosition = collision.transform.position;
            float racketHeigth = collision.collider.bounds.size.y;

            float positionX;
            if(collision.gameObject.name =="Player 1"){
                positionX=1;
            }
            else{
                positionX=-1;
            }
            float positionY =(ballPosition.y - racketPosition.y)/ racketHeigth ;

            BallMouvement.IncreaseHitCounter();
            BallMouvement.MoveBall(new Vector2(positionX, positionY));

        }
        void Update(){
             if (Input.GetKeyDown("left")){
                 scoreManager.Player1Goal();  
            }
            if (Input.GetKeyDown("right")){
                scoreManager.Player2Minus();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision){
            if(collision.gameObject.name == "Player 1" || collision.gameObject.name == "Player 2")
            {
                Bounce(collision);
            }
            else if (collision.gameObject.name == "Right Border"){
                scoreManager.Player1Goal();
                BallMouvement.player1Start = false;
                StartCoroutine(BallMouvement.Launch());    
            }
             else if (collision.gameObject.name == "Left Border"){
                scoreManager.Player2Goal();  
                BallMouvement.player1Start = true;
                StartCoroutine(BallMouvement.Launch());  
            }
            Instantiate(hitSFX,transform.position, transform.rotation );
           
        }
}
