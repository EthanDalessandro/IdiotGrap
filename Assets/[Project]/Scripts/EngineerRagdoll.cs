using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EngineerRagdoll : MonoBehaviour
{
    [SerializeField] private float _recoveryTime;
    [SerializeField] private float _getUpSpeed;
    private Rigidbody _engineerBody;

    private void Start()
    {
        _engineerBody = GetComponent<Rigidbody>();
    }

    public void SetRagdollActivateRagdoll(bool isEnabled, bool canRecover = true)
    {
        if (isEnabled) 
        {
            _engineerBody.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            _engineerBody.constraints = RigidbodyConstraints.None;
            if (canRecover)
            {
                StartCoroutine(RecoverFromRagdoll());
            }
        }
    }

    IEnumerator RecoverFromRagdoll()
    {
        yield return new WaitForSeconds(_recoveryTime);
        _engineerBody.constraints = RigidbodyConstraints.FreezeRotationZ; 
        transform.DORotate(new Vector3(90, 0, 0), _getUpSpeed);
    }
}
