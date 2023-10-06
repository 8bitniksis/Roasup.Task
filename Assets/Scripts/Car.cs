using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public DrawWithMouse drawControll;
    public ParkerInfo info;
    public float speed = 10;
    public GameObject obj; 

    public bool stopMove = false;
    public bool startMovement = false;
    public bool isHitCar = false;
    public bool isHitCarStop = false;
    Vector3[] positions;
    int moveIndex = 0;

    RaycastHit2D hit;

    public bool conect;
    private Vector3 startPosition;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;

    private void OnMouseDown()
    {
        drawControll.StartLine(transform.position);
    }

    private void OnMouseDrag()
    {
        drawControll.UpdateLines();
    }

    private void OnMouseUp()
    {
        positions = new Vector3[drawControll.line.positionCount];
        drawControll.line.GetPositions(positions);
        moveIndex = 0;

        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
 
        if(hit.collider.tag == "Red" & obj.tag == "CarRed" & obj != null)
        {
            conect = true;
        }
        else if(hit.collider.tag == "Yellow" & obj.tag == "CarYellow")
        {
            conect = true;
        }
        else
        {
            Debug.Log ("We target other: " + hit.collider.gameObject.transform.position);
            drawControll.StartLine(transform.position);
        }
       
    }

    private void Awake()
    {
        startPosition = transform.position;

        //apply the Quaternion.eulerAngles change to the gameObject
        currentRotation = transform.rotation;
    }

    private void Update()
    {
        isHitCar = isHitCarStop;
        if(startMovement == true)
        {
            Vector2 currentPos = positions[moveIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);

            //rotate
            Vector2 dir = currentPos - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x);
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f), speed);

            float distance = Vector2.Distance(currentPos, transform.position);
            if(distance <= 0.05f)
            {
                moveIndex++;
            }

            if(moveIndex > positions.Length - 1)
            {
                startMovement = false;
                moveIndex = moveIndex - 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "CarRed" || collider.tag == "CarYellow")
        {
            Debug.Log ("We hit car!");
            isHitCar = true;
            drawControll.StartLine(transform.position);
            this.transform.position = startPosition;
            this.transform.rotation = currentRotation;
            startMovement = false;
            conect = false;
        }
    }
}
