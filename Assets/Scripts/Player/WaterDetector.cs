using Player;
using UnityEngine;

public class WaterDetector : MonoBehaviour
{
   public GameObject waterEffect;

   private float _jumpSpeed;
   
   private void OnTriggerEnter(Collider other)
   {
      Debug.Log("Enter");
      waterEffect.gameObject.SetActive(true);
      PlayerController._instance.gravity = Physics.gravity.y/3;
      _jumpSpeed = PlayerController._instance.jumpSpeed;
      PlayerController._instance.jumpSpeed /= 2;
   }

   private void OnTriggerExit(Collider other)
   {
      Debug.Log("Left");
      waterEffect.gameObject.SetActive(false);
      PlayerController._instance.gravity = Physics.gravity.y;
      PlayerController._instance.jumpSpeed = _jumpSpeed;
   }
}
