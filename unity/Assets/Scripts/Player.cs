using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour
{
    public TextMesh dorsalText;
    public Transform followingCam, seenCam;

    private ThirdPersonCharacter thirdPerson;
    private new Rigidbody rigidbody;

    public DataSetRow lastRow { get; private set; }

    private Vector3 pos;
    private Vector3 nextPos;

    public int tagID { get; private set; }

    private void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        thirdPerson = this.gameObject.GetComponent<ThirdPersonCharacter>();
    }

    public void SetUp(int tag)
    {
        this.tagID = tag;
        dorsalText.text = tag.ToString();
    }

    public void UpdatePlayer(DataSetRow row, DataSetRow? nextRow)
    {
        lastRow = row;
        nextPos = pos = new Vector3(row.x_pos, 0, row.y_pos);
        //this.transform.position = new Vector3(row.x_pos, 0, row.y_pos);
        if (nextRow != null)
        {
            nextPos = nextRow.Value.GetPos();
            Vector3 currentPos = row.GetPos();

            this.transform.LookAt(nextPos);

            //Vector3 direction = Vector3.Lerp(currentPos, nextPos, 0.6f);
            Vector3 move = nextPos - currentPos;
            thirdPerson.Move(move, false, false);
        }

    }

    public void StopMoving()
    {
        rigidbody.isKinematic = true;
        rigidbody.isKinematic = false;
    }

    private void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(pos, nextPos, 0.2f);
        pos = this.transform.position;
    }
}
