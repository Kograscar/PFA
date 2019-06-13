using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CharController : MonoBehaviour
{
	#region Fields
	public Transform _reticule;

	public bool _carryingItem = false;
	[SerializeField] GameObject[] _meshs;
	[SerializeField] public GameObject _itemCanvas;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] public CinemachineVirtualCamera _mainCamera;
    private bool _canMove = true;
	private bool _desintearcting = false;
	private InteractableItem _interactingItem;
	PlayerLook _playerLook;
	List<GameObject> _pickUps;
	PlayerMove _playerMove;
	Vector3 _cameraBasePosition;
	Quaternion _cameraBaseRotation;
	float _lerpDelay;
	bool _bigReticule;
	Vector3 _reticuleTargetScale = new Vector3 (10,10,10);
	Vector3 _reticuleActualScale = new Vector3 (10,10,10);
	#endregion Fields
 
	void Start ()
	{		
		_playerLook = GetComponentInChildren<PlayerLook>();
		_playerMove = GetComponentInChildren<PlayerMove>();
	}
	
 
	void Update ()
	{
        if(_canMove){
        #region Interact
        
            RaycastHit hit;

			Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 10f, Color.red, 2f);

            if(Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 3f)){

                if(hit.collider != null){
					if(hit.collider.CompareTag("InteractableItem")){

						if(_bigReticule == false){
							OverItem();
						}

						if(Input.GetButtonDown("Interact")){
							
							_interactingItem = hit.collider.GetComponentInChildren<InteractableItem>();
							if(_interactingItem is Examinate){
								_cameraBasePosition = _mainCamera.transform.localPosition;
								_cameraBaseRotation = _mainCamera.transform.rotation;
								_interactingItem.Use(_mainCamera.gameObject);
								_canMove = false;
								_playerLook.enabled = false;
								_playerMove.enabled = false;
								_reticule.gameObject.SetActive(false);
								Cursor.lockState = CursorLockMode.None;
								Cursor.visible = true;
								foreach(GameObject item in _meshs){
									item.SetActive(false);
								}
							}else if(_interactingItem is PickUp){
								if(_carryingItem == false){
									_interactingItem.transform.parent = _itemCanvas.transform;
									_interactingItem.transform.localPosition = Vector3.zero;
									_interactingItem.Use(_itemCanvas);
									_carryingItem = true;
									_interactingItem.gameObject.tag = "CanSnap";
								}
							}else if(_interactingItem is SolveButton || _interactingItem is RotatingTableau || _interactingItem is DoorKey || _interactingItem is LockedDoor || _interactingItem is SoundPlayer){
								_interactingItem.Use(gameObject);
							}
						}
                	}else if(_bigReticule == true){
						NoItem();
					}
            	}else if(_bigReticule == true){
					NoItem();
				}
        	}else if(_bigReticule == true){
				NoItem();
			}
			if(_carryingItem == true && Input.GetButtonDown("Cancel")){
				_itemCanvas.transform.DetachChildren();
				_interactingItem.UnUse();
				_carryingItem = false;
				_interactingItem.gameObject.tag = "InteractableItem";
			}
			#endregion Interact
				
		}else if(Input.GetButtonDown("Cancel") && _desintearcting == false){
			StartCoroutine(GoBackToReality());
		}
		_lerpDelay += Time.deltaTime * 10;
		_reticule.localScale = Vector3.Lerp(_reticuleActualScale, _reticuleTargetScale, _lerpDelay);
    }

	void OverItem(){
		_reticuleActualScale = _reticule.localScale;
		_reticuleTargetScale = new Vector3(25,25,25);
		_lerpDelay = 0;
		_bigReticule = true;
	}

	void NoItem(){
		_reticuleActualScale = _reticule.localScale;
		_reticuleTargetScale = new Vector3(10,10,10);
		_lerpDelay = 0;
		_bigReticule = false;
	}

	public IEnumerator GoBackToReality(){
		_interactingItem.UnUse();
		_desintearcting = true;
		yield return new WaitForSeconds(.5f);
		foreach(GameObject item in _meshs){
			item.SetActive(true);
		}
		_canMove = true;
		_playerLook.enabled = true;
		_playerMove.enabled = true;
		_reticule.gameObject.SetActive(true);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		_mainCamera.transform.localPosition = _cameraBasePosition;
		_mainCamera.transform.rotation = _cameraBaseRotation;
		_desintearcting = false;
	}
}