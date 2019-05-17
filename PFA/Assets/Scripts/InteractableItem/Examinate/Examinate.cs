using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examinate : InteractableItem
{
    #region Fields
    private Vector3 _startPosition;
    private Quaternion _startQuaternion;
    [SerializeField] private GameObject _mesh;

    private float _lerpDelay;
    [SerializeField] float _rotateSpeed = 1;

    bool _takingItem = false;
    bool _puttingBackItem = false;
    bool _interacting = false;
    bool _zoomed = false;

    [SerializeField] Transform _itemCanvas;
    [SerializeField] Transform _playerCanvas;
    private Vector3 _cameraPosition;
    private Quaternion _cameraQuaternion;
    GameObject _player;
    GameObject _selectedMesh;
    BoxCollider _boxCollider;
    private Quaternion _meshBaseRotation;
    private Quaternion _meshModificatedRotation;


    int _xRotation;
    int _yRotation;
    Space _rotationSpace;
    #endregion Fields
    
    void OnEnable(){
        _startPosition = _mesh.transform.localPosition;
        _startQuaternion = _mesh.transform.localRotation;
        _meshBaseRotation = _mesh.transform.rotation;
        _boxCollider = GetComponent<BoxCollider>();
    }

    void Update(){
        #region Interacting
        _lerpDelay += Time.deltaTime * 2;
        if(_takingItem){
            if(_lerpDelay <= 1){
                _mesh.transform.localPosition = Vector3.Lerp(_startPosition, _itemCanvas.localPosition, _lerpDelay);
                //_mesh.transform.localRotation = Quaternion.Lerp(_startQuaternion, _itemCanvas.localRotation, _lerpDelay);
                _player.transform.position = Vector3.Lerp(_player.transform.position, _playerCanvas.position, _lerpDelay);
                _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, _playerCanvas.rotation, _lerpDelay);
            }else{_takingItem = false; _interacting = true;}
        }
        if(_puttingBackItem){
            if(_lerpDelay <= 1){
                _mesh.transform.localPosition = Vector3.Lerp(_itemCanvas.localPosition, _startPosition, _lerpDelay);
                //_mesh.transform.localRotation = Quaternion.Lerp(_itemCanvas.localRotation, _startQuaternion, _lerpDelay);
                _player.transform.position = Vector3.Lerp(_playerCanvas.position, _player.transform.position, _lerpDelay);
                _player.transform.rotation = Quaternion.Lerp(_playerCanvas.rotation, _player.transform.rotation, _lerpDelay);
                _mesh.transform.rotation = Quaternion.Lerp(_meshModificatedRotation, _meshBaseRotation, _lerpDelay);
            }else{_puttingBackItem = false; _interacting = false;}
        }
        #endregion Interacting
        
        #region Interaction

        if(_interacting){
            if(Input.GetMouseButtonDown(0)){
                RaycastHit moveHit;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out moveHit, 2f)){
                    if(moveHit.collider.transform.parent == (gameObject || _mesh)){
                        _selectedMesh = moveHit.collider.gameObject;
                        _xRotation = _selectedMesh.GetComponent<PartInfo>()._xRotation;
                        _yRotation = _selectedMesh.GetComponent<PartInfo>()._yRotation;
                        _rotationSpace = _selectedMesh.GetComponent<PartInfo>()._rotationSpace;
                    }
                }
            }

            if(Input.GetMouseButton(0)){
                if(_selectedMesh != null){
                    _selectedMesh.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * _xRotation, -Input.GetAxis("Mouse X") * _yRotation, 0)
                    * Time.deltaTime * _rotateSpeed, _rotationSpace);
                }
            }

            if(Input.GetMouseButtonUp(0)){
                _selectedMesh = null;
            }

            /*if(Input.GetMouseButtonDown(2)){
                if(_zoomed == false){
                    transform.localScale *= 2;
                }else{
                    transform.localScale *= .5f;
                }
                _zoomed = !_zoomed;
            }*/
        }

        #endregion Interaction
    }

    public override void Use(GameObject player){
         _cameraPosition = player.transform.position;
        _cameraQuaternion = player.transform.rotation;
        _player = player;
        _takingItem = true;
        _lerpDelay = 0;
        _boxCollider.enabled = false;
    }

    public override void UnUse(){
        _lerpDelay = 0;
        _puttingBackItem = true;
        _boxCollider.enabled = true;
        _meshModificatedRotation = _mesh.transform.rotation;
    }
}
