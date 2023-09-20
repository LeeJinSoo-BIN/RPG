using UnityEngine;
using System.Collections;
using Photon.Pun;

public class MagicHeal : MonoBehaviourPunCallbacks
{
    //private float Deal;
    private float Heal;
    //private float Shield;
    //private float Power;
    public GameObject target;
    //public Vector2 targetPos;
    public PhotonView parentPV;
    public PhotonView PV;
    
    public void Excute()
    {
        if (parentPV.IsMine)
        {
            CharacterState state = transform.parent.GetComponentInChildren<CharacterState>();
            state.ProcessSkill(1, Heal);
        }
    }

    IEnumerator Vanish(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        GetComponent<Animator>().SetTrigger("vanish");
        PV.RPC("destroySelf", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void destroySelf()
    {
        Destroy(gameObject, 0.45f);
    }

    [PunRPC]
    void initSkill(float deal, float heal, float sheild, float power, float duration, string target_name, Vector2 target_pos)
    {
        //Deal = deal;
        Heal = heal;
        //Shield = sheild;
        //Power = power;
        if (target_name != "")
        {
            target = GameObject.Find(target_name);
            transform.parent = target.transform;
        }
        if (target_pos != default(Vector2))
        {
            //targetPos = target_pos;
        }
        parentPV = transform.parent.GetComponent<PhotonView>();
        StartCoroutine(Vanish(duration));
        Excute();
        
    }
}
