using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ColorObject
{
    [SerializeField] FixedJoystick joystick;
    [SerializeField] LayerMask layerGround;
    [SerializeField] private float moveSpeed;
    [SerializeField] private PlayerBrick playerBrickPrefab;
    [SerializeField] private Transform brickHolder;
    private List<PlayerBrick> playerBricks= new List<PlayerBrick>();
    private bool canMoving = true;

    public Stage stage;
    void Start() {
        ChangeColor(ColorType.Blue);  
    }
    void Update() {
        Move();
    }

    protected void Move(){
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                // Calculate the target position
                Vector3 nextPoint = transform.position + direction * moveSpeed * Time.deltaTime;
                // Rotate the player to face the movement direction
                transform.rotation = Quaternion.LookRotation(direction);
                if (canMoving){      
                    //ChangeAnim("run");
                    // Move the player to the new position
                    transform.position = CheckGround(nextPoint);
                }
            }
        }
    }
    // Check if the nextPoint is movable to go to that point
    private Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 5f, layerGround))
        {
            return hit.point + Vector3.up * 1.1f;
        }
        return transform.position;
    }

    public void AddBrick(){
        PlayerBrick playerBrick = Instantiate(playerBrickPrefab, brickHolder);
        playerBrick.ChangeColor(colorType);
        playerBrick.transform.localPosition = Vector3.up * 0.25f * playerBricks.Count;
        playerBricks.Add(playerBrick);
    }
    public void RemoveBrick(){
        if (playerBricks.Count > 0){
            PlayerBrick playerBrick = playerBricks[playerBricks.Count - 1];
            playerBricks.RemoveAt(playerBricks.Count - 1);
            Destroy(playerBrick.gameObject);
        }
    }
    public void ClearBrick(){
        for (int i = 0; i < playerBricks.Count; i++){
            Destroy(playerBricks[i]);
        }

        playerBricks.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Brick")){
            Brick brick = other.GetComponent<Brick>();
            if (brick.colorType == colorType){
                Destroy(brick.gameObject);
                AddBrick();
            }
        }
        if (other.CompareTag("BrickStair")){
            Stair stair = other.GetComponent<Stair>();
            if (stair.colorType != colorType && playerBricks.Count > 0){
                stair.ChangeColor(colorType);
                RemoveBrick();
            }
            if (stair.colorType != colorType && playerBricks.Count == 0 && transform.forward.z > 0){
                canMoving = false;
            }
        }
    }
}
