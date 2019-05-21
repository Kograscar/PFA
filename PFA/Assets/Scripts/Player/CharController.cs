using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	private InteractableItem _interactingItem;
	PlayerLook _playerLook;
	PlayerMove _playerMove;
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
							_playerLook.enabled = false;
							_playerLook.enabled = false;
							Cursor.lockState = CursorLockMode.None;
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
			
        }else if(Input.GetButtonDown("Cancel") && _desintearcting == false){
			_interactingItem.UnUse();
			StartCoroutine(GoBackToReality());
			_desintearcting = true;
		}
    }

	IEnumerator GoBackToReality(){
		yield return new WaitForSeconds(.5f);
		foreach(GameObject item in _meshs){
			item.SetActive(true);
		}
		_canMove = true;
		_playerLook.enabled = true;
		_playerMove.enabled = true;
		Cursor.lockState = CursorLockMode.Locked;
		_desintearcting = false;
	}
}