using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float sensitivity = 2f;
    private float minZoom = 2f;
    private float maxZoom = 9f;

    public float speed = 4f;

    private float border = 7f;

    public Vector2 startPosition;
    public Vector2 upButtonPosition;
    private new Camera camera;

    private float targetPositionX;
    private float targetPositionY;

    public bool isMove = true; // Можно двигать камеру (true - да)
    public bool isMoved = false; // Находится в движении

    public bool moveToObject;  // Двигаться к объекту (true - да)
    public float speedToMovableObject = 0.3f;

    public Vector2 startPositionCursor;
    public Vector2 endPositionCursor;

    private float _MovableObjectPositionX;

    public float MovableObjectPositionX
    {
        get { return _MovableObjectPositionX; }
        set { _MovableObjectPositionX = value; }
    }

    private float _MovableObjectPositionY;

    public float MovableObjectPositionY
    {
        get { return _MovableObjectPositionY; }
        set { _MovableObjectPositionY = value; }
    }

    private DBase dBase;                   //BO

    void Start()
    {
       
        GameObject dataBase = GameObject.Find("DBase");  // BO
        dBase = dataBase.GetComponent<DBase>();          //BO
        GetComponent<Camera>().orthographicSize = dBase.ReadFloat(3);
        transform.position = new Vector3(dBase.ReadFloat(1), dBase.ReadFloat(2),transform.position.z);
        camera = GetComponent<Camera>();
        targetPositionX = transform.position.x;
        targetPositionY = transform.position.y;
    }

    public void startMove()
    {
        isMove = true;
    }

    public void StopMove()
    {
        isMove = false;
    }

    public void startMoveToObject ()
    {
        moveToObject = true;
    }

    public void stopMoveToObject ()
    {
        moveToObject = false;
    }

    private void ZoomCamera (float increment)
    {
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize - increment * sensitivity, minZoom, maxZoom);
        
    }

    public void StartPositionCursor ()
    {
        startPositionCursor = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void EndPositionCursor()
    {
        endPositionCursor = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        
        if (isMove)
        {
            
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;
                ZoomCamera(difference * 0.01f);
            } 
            else if (Input.GetMouseButtonDown(0)) 
            {
                startPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                float positionX = camera.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x;
                targetPositionX = Mathf.Clamp(transform.position.x - positionX, -1 * border, border);
                float positionY = camera.ScreenToWorldPoint(Input.mousePosition).y - startPosition.y;
                targetPositionY = Mathf.Clamp(transform.position.y - positionY, -1 * border, border);
            }
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPositionX, speed * Time.deltaTime), 
                                             Mathf.Lerp(transform.position.y, targetPositionY, speed * Time.deltaTime), 
                                             transform.position.z);
            ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
            
        }
        
        if (!isMove && moveToObject)
        {
            float distance = Vector2.Distance(camera.transform.position, new Vector2(_MovableObjectPositionX, _MovableObjectPositionY));           

            if (distance > 3)
            {
                float positionObjectX = Mathf.Clamp(_MovableObjectPositionX, -1 * border, border);
                float positionObjectY = Mathf.Clamp(_MovableObjectPositionY, -1 * border, border);

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, positionObjectX, speedToMovableObject * Time.deltaTime),
                                                 Mathf.Lerp(transform.position.y, positionObjectY, speedToMovableObject * Time.deltaTime),
                                                 transform.position.z);
                targetPositionX = transform.position.x;
                targetPositionY = transform.position.y;
            }
        }
        
    }
    void OnApplicationQuit() //работает в редакторе              BO
    {
        dBase.SaveFloat(targetPositionX, 1);                            //BO
        dBase.SaveFloat(targetPositionY, 2);                            //BO
        dBase.SaveFloat(GetComponent<Camera>().orthographicSize, 3);   //BO
    
    }
    void OnApplicationPause(bool pauseStatus) //работает на android     BO
    {
        if (pauseStatus == true)                                                                          //BO
        {
            dBase.SaveFloat(targetPositionX, 1);                            //BO
            dBase.SaveFloat(targetPositionY, 2);                            //BO
            dBase.SaveFloat(GetComponent<Camera>().orthographicSize, 3);   //BO
            
        }
    }
}
