using UnityEngine;

public class SceneBorderLimitLoop : MonoBehaviour
{
     private float upperBorder = 5.5f;
     private float bottomBorder = -5.5f;
     private float leftBorder = -9.25f;
     private float rightBorder = 9.25f;

     private void Update()
     {
          var pos = transform.position;

          CheckBorder(pos);
     }

     private void CheckBorder(Vector3 pos)
     {
          if (pos.x > rightBorder)
          {
               pos.x = leftBorder;
               transform.position = pos;
          }
          if (pos.x < leftBorder)
          {
               pos.x = rightBorder;
               transform.position = pos;
          }
          if (pos.y > upperBorder)
          {
               pos.y = bottomBorder;
               transform.position = pos;
          }
          if (pos.y < bottomBorder)
          {
               pos.y = upperBorder;
               transform.position = pos;
          }
     }

}
