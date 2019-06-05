using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Examinate : InteractableItem
{
    #region Fields
    private Vector3 _startPosition;
    private Quaternion _startQuaternion;
    [SerializeField] private GameObject _mesh;

    private float _lerpDelay;
    [SerializeField] float _transformSpeed = 1;

    bool _takingItem = false;
    bool _puttingBackItem = false;
    bool _interacting = false;
    bool _zoomed = false;

    [SerializeField] Transform _itemCanvas;
    [SerializeField] Transform _playerCanvas;
    private Vector3 _cameraPosition;
    private Quaternion _cameraQuaternion;
    GameObject _player;
    [HideInInspector] public GameObject _selectedMesh;
    public BoxCollider _boxCollider;
    private Quaternion _meshBaseRotation;
    private Quaternion _meshModificatedRotation;
    RotOrPos _transformType;
    int _xTransform;
    int _yTransform;
    Space _transformSpace;
    CinemachineVirtualCamera _camera;
    #endregion Fields
    
    void OnEnable(){
        _startPosition = _mesh.transform.localPosition;
        _startQuaternion = _mesh.transform.localRotation;
        _meshBaseRotation = _mesh.transform.rotation;
        _boxCollider = GetComponent<BoxCollider>();
        _camera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CharController>()._mainCamera;
    }

    void Update(){
        #region Interacting
        _lerpDelay += Time.deltaTime * 2;
        if(_takingItem){
            if(_lerpDelay <= 1){
                _mesh.transform.localPosition = Vector3.Lerp(_startPosition, _itemCanvas.localPosition, _lerpDelay);
                _player.transform.position = Vector3.Lerp(_player.transform.position, _playerCanvas.position, _lerpDelay);
                _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, _playerCanvas.rotation, _lerpDelay);
            }else{_takingItem = false; _interacting = true;}
        }
        if(_puttingBackItem){
            if(_lerpDelay <= 1){
                _mesh.transform.localPosition = Vector3.Lerp(_itemCanvas.localPosition, _startPosition, _lerpDelay);
                _player.transform.position = Vector3.Lerp(_playerCanvas.position, _player.transform.position, _lerpDelay);
                _player.transform.rotation = Quaternion.Lerp(_playerCanvas.rotation, _player.transform.rotation, _lerpDelay);
                _mesh.transform.rotation = Quaternion.Lerp(_meshModificatedRotation, _meshBaseRotation, _lerpDelay);
            }else{_puttingBackItem = false; _interacting = false;}
        }
        #endregion Interacting
        
        #region Interaction

        if(_interacting){
            if(Input.GetMouseButtonDown(0)){
                SelectingMesh();
            }

            if(Input.GetMouseButton(0)){
                MovingMesh();
            }

            if(Input.GetMouseButtonUp(0)){
                DeselectingMesh();
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

    public void SelectingMesh(){
        RaycastHit moveHit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out moveHit, 2f)){
            if(moveHit.collider.transform.parent == (gameObject || _mesh)){
                PartInfo partInfo = moveHit.collider.gameObject.GetComponentInChildren<PartInfo>();
                //Debug.Log(partInfo.gameObject);
                _xTransform = partInfo._xTransform;
                _yTransform = partInfo._yTransform;
                _transformSpace = partInfo._transformSpace;
                _transformType = partInfo._transformType;
                if(partInfo._gameObject != null){
                    _selectedMesh = partInfo._gameObject;
                }else{
                    _selectedMesh = moveHit.collider.gameObject;
                }
            }
        }
    }

    public void MovingMesh(){
        if(_selectedMesh != null){
            switch(_transformType){
                case RotOrPos.Rotation :
                    _selectedMesh.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * _xTransform, -Input.GetAxis("Mouse X") * _yTransform, 0)
                    * Time.deltaTime * _transformSpeed * 100, _transformSpace);
                    break;
                
                case RotOrPos.Position :
                    _selectedMesh.transform.Translate(new Vector3(Input.GetAxis("Mouse X") * _xTransform, Input.GetAxis("Mouse Y") * _yTransform, 0)
                    * Time.deltaTime * _transformSpeed, _transformSpace);
                    break;
            }
        }
    }

    public void DeselectingMesh(){
        _selectedMesh = null;
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
