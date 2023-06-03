using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] private WeaponData weaponData;
    private Character Owner;
    [SerializeField] private GameObject Hit_VFX;
    [SerializeField] Rigidbody rb;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform weaponTransform;
    private bool isGoBack = false;

    private void FixedUpdate() 
    {
        if(Owner == null) return;
        if(weaponData.GetWeapon(Owner.weaponType).rotate)
        {
            weaponTransform.eulerAngles += Vector3.up * rotationSpeed * Time.fixedDeltaTime;
        }
        if(isGoBack)
        {
            TF.position = Vector3.Lerp(TF.position, Owner.TF.position, (Owner.weaponData.GetWeapon(Owner.weaponType).Speed - 3f) * Time.fixedDeltaTime);
        }
        if(((Vector3.Distance(TF.position, Owner.TF.position) < 2f) || Owner.isDead) && isGoBack)
        {
            OnDespawn();
        }
    }
    public override void OnInit()
    {
        
    }

    public void OnInit(Character character) 
    {
        Owner = character;
        TF.position = Owner.ThrowPoint.transform.position;
        TF.rotation = Owner.ThrowPoint.transform.rotation;
        SizeUp(Owner.sizeCharacter);
    }

    public override void OnDespawn()
    {
        rb.velocity = Vector3.zero;
        SimplePool.Despawn(this);
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character character = Cache.GetCharacter(other);
            if(character == Owner) return;
            Owner.ScaleUp();
            if(Owner == LevelManager.Ins.player)
            {
                CameraFollow.Ins.SetUpWhenKill(2f);
                LevelManager.Ins.player.SetNumberKillBot(1);
                LevelManager.Ins.player.UpCoin((int)LevelManager.Ins.player.rateUpGold);
            }
            else
            {
                Bot bot = Owner.GetComponent<Bot>();
                bot.SetMoveSpeedBot();
            }

            if(character == LevelManager.Ins.player)
            {
                LevelManager.Ins.player.nameKiller = Owner.nameChar;
            }
            else
            {
                character.nameKiller = Owner.nameChar;
            }

            Owner.missionWaypoint.Setoffset(0.5f);
            Owner.missionWayPoint2.Setoffset(0.5f);

            Owner.characterList.Remove(character);
            Owner.isAttack = false;
            Instantiate(Hit_VFX, TF.position + Vector3.up * 2f, Quaternion.identity);
            character.OnHit();
            OnDespawn();
        }

        if(other.CompareTag(Constant.TAG_OBSTACLE))
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Ring"))
        {
            if(weaponData.GetWeapon(Owner.weaponType).goBack)
            {
                isGoBack = true;
                return;
            }
            OnDespawn();
        }
    }

    //TODO: ro rang hon trong viec dat ten
    private void SizeUp(float size)
    {
       TF.localScale = Vector3.one * size;
    }

    public void AddForce()
    {
        rb.AddForce(TF.forward * weaponData.GetWeapon(Owner.weaponType).Speed, ForceMode.Impulse);
    }
}
