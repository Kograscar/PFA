using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SALUT LEOPOLD
public class CharController : MonoBehaviour
{
	#region Fields
	[SerializeField] GameObject[] _meshs;
	List<GameObject> _pickUps;
	[SerializeField] GameObject _itemCanvas;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _mainCamera;
    private bool _canMove = true;
	private bool _desintearcting = false;
	public bool _carryingItem = false;
    //[SerializeField] private bool _interacting = true;
	private InteractableItem _interactingItem;

	[SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerBody;

    private float xAxisClamp;
/*
    #region SmoothFPSFields
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
 
	public float minimumX = -360F;
	public float maximumX = 360F;
 
	public float minimumY = -60F;
	public float maximumY = 60F;
 
	float rotationX = 0F;
	float rotationY = 0F;
 
	private List<float> rotArrayX = new List<float>();
	float rotAverageX = 0F;	
 
	private List<float> rotArrayY = new List<float>();
	float rotAverageY = 0F;
 
	public float frameCounter = 20;
 
	Quaternion originalRotation;
    #endregion SmoothFPSFields
	*/
	#endregion Fields
 
	void Start ()
	{		
        xAxisClamp = 0f;
		/*Rigidbody rb = GetComponent<Rigidbody>();	
		if (rb)
		rb.freezeRotation = true;
		originalRotation = _mainCamera.transform.localRotation;*/
	}
 
	void Update ()
	{
        if(_canMove){
        #region movement
        float horizontalAxis;
        horizontalAxis = Input.GetAxis("Horizontal");

        float verticalAxis;
        verticalAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3();
        movement.x += horizontalAxis;
        // movement.y += -90 * Time.deltaTime;
        movement.z += verticalAxis;
        movement *= _speed * Time.deltaTime;
		Vector3 moveDirection = _rigidbody.transform.TransformDirection(horizontalAxis * _speed * Time.deltaTime, 0, (verticalAxis * _speed * Time.deltaTime));

        _rigidbody.MovePosition(_rigidbody.position + moveDirection);
		_mainCamera.transform.position  = new Vector3 (_rigidbody.position.x, _mainCamera.transform.position.y, _rigidbody.transform.position.z);
        //_rigidbody.MovePosition(_rigidbody.transform.position + movement);
        #endregion movement

		CameraRotation();
		/*
        #region Look
		if (axes == RotationAxes.MouseXAndY)
		{			
			rotAverageY = 0f;
			rotAverageX = 0f;
 
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
 
			rotArrayY.Add(rotationY);
			rotArrayX.Add(rotationX);
 
			if (rotArrayY.Count >= frameCounter) {
				rotArrayY.RemoveAt(0);
			}
			if (rotArrayX.Count >= frameCounter) {
				rotArrayX.RemoveAt(0);
			}
 
			for(int j = 0; j < rotArrayY.Count; j++) {
				rotAverageY += rotArrayY[j];
			}
			for(int i = 0; i < rotArrayX.Count; i++) {
				rotAverageX += rotArrayX[i];
			}
 
			rotAverageY /= rotArrayY.Count;
			rotAverageX /= rotArrayX.Count;
 
			rotAverageY = ClampAngle (rotAverageY, minimumY, maximumY);
			rotAverageX = ClampAngle (rotAverageX, minimumX, maximumX);
 
			Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
 
			_mainCamera.transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{			
			rotAverageX = 0f;
 
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
 
			rotArrayX.Add(rotationX);
 
			if (rotArrayX.Count >= frameCounter) {
				rotArrayX.RemoveAt(0);
			}
			for(int i = 0; i < rotArrayX.Count; i++) {
				rotAverageX += rotArrayX[i];
			}
			rotAverageX /= rotArrayX.Count;
 
			rotAverageX = ClampAngle (rotAverageX, minimumX, maximumX);
 
			Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
			_mainCamera.transform.localRotation = originalRotation * xQuaternion;
		}
		else
		{			
			rotAverageY = 0f;
 
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
 
			rotArrayY.Add(rotationY);
 
			if (rotArrayY.Count >= frameCounter) {
				rotArrayY.RemoveAt(0);
			}
			for(int j = 0; j < rotArrayY.Count; j++) {
				rotAverageY += rotArrayY[j];
			}
			rotAverageY /= rotArrayY.Count;
 
			rotAverageY = ClampAngle (rotAverageY, minimumY, maximumY);
 
			Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
			_mainCamera.transform.localRotation = originalRotation * yQuaternion;
		}
		
		//Quaternion.EulerAngles(_rigidbody.transform.rotationX) = Quaternion.EulerAngles() 
		//_rigidbody.transform.localRotation = Quaternion.Euler(0,_mainCamera.transform.localRotation.eulerAngles.y,0);
		_rigidbody.transform.localRotation = _mainCamera.transform.localRotation;
		_rigidbody.transform.localRotation = Quaternion.Euler(0, _rigidbody.transform.localRotation.eulerAngles.y, 0);

        #endregion Look
*/
        #region Interact
        if(Input.GetButtonDown("Interact")){

            RaycastHit hit;

			Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 10f, Color.red, 2f);

            if(Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 2f)){

                if(hit.collider != null){
					if(hit.collider.CompareTag("InteractableItem")){
                    	_interactingItem = hit.collider.GetComponent<InteractableItem>();
						if(_interactingItem is Examinate || _interactingItem is Puzzle){
							_interactingItem.Use(_mainCamera.gameObject);
							_canMove = false;
							foreach(GameObject item in _meshs){
								item.SetActive(false);
							}
						}else if(_interactingItem is PickUp){
							if(_carryingItem == false){
								_interactingItem.transform.parent = _itemCanvas.transform;
								_interactingItem.transform.localPosition = Vector3.zero;
								_interactingItem.Use(_itemCanvas);
								_carryingItem = true;
							}
						}
					}
                }
            }
        }
		if(_carryingItem == true && Input.GetButtonDown("Cancel")){
			_itemCanvas.transform.DetachChildren();
			_interactingItem.UnUse();
			_carryingItem = false;
		}
        #endregion Interact
		/*if(Input.GetButtonDown("Cancel")){
			if(_carryingItem){

			}
		}*/
			
        }else if(Input.GetButtonDown("Cancel") && _desintearcting == false){
			_interactingItem.UnUse();
			StartCoroutine(GoBackToReality());
			_desintearcting = true;
		}
    }
 
 /*
	public static float ClampAngle (float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F)) {
			if (angle < -360F) {
				angle += 360F;
			}
			if (angle > 360F) {
				angle -= 360F;
			}			
		}
		return Mathf.Clamp (angle, min, max);
	}
*/
	private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if(xAxisClamp > 90f)
        {
            xAxisClamp = 90f;
            mouseY = 0f;
            ClampXAxisRotationToValue(270f);
        }
        else if (xAxisClamp < -90f)
        {
            xAxisClamp = -90f;
            mouseY = 0f;
            ClampXAxisRotationToValue(90f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

	IEnumerator GoBackToReality(){
		yield return new WaitForSeconds(.5f);
		foreach(GameObject item in _meshs){
			item.SetActive(true);
		}
		_canMove = true;
		_desintearcting = false;
	}
}