using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehaviourScript : MonoBehaviour {

    public playerBehaviourScript sPlayer;
    public gameManager sManager;
    public bulletBehavior sBullet;
    public float priceHike;
    public float FirerateCost;
    public float DamageCost;
    public float AccuracyCost;
    public float SpeedCost;
    public float JumpCost;
    public float HealthCost;
    public float AmmoCost;
    public float AmmoFillCost;
    public Button Firerate;
    public Button Damage;
    public Button Accuracy;
    public Button Speed;
    public Button Jump;
    public Button Health;
    public Button Ammo;
    public Button AmmoFill;
 

    private void Awake() {
        sPlayer = GameObject.Find("Player").GetComponent<playerBehaviourScript>();
        sManager = GameObject.Find("gameManager").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update () {
		
        if (sPlayer.Money < AccuracyCost) {
            Accuracy.interactable = false;
        } else { Accuracy.interactable = true; }

        if (sPlayer.Money < FirerateCost) {
            Firerate.interactable = false;
        } else { Firerate.interactable = true; }

        if (sPlayer.Money < DamageCost) {
            Damage.interactable = false;
        } else { Damage.interactable = true; }

        if (sPlayer.Money < SpeedCost) {
            Speed.interactable = false;
        } else { Speed.interactable = true; }

        if (sPlayer.Money < JumpCost) {
            Jump.interactable = false;
        } else { Jump.interactable = true; }

        if (sPlayer.Money < HealthCost) {
            Health.interactable = false;
        } else { Health.interactable = true; }

        if (sPlayer.Money < AmmoCost) {
            Ammo.interactable = false;
        } else { Ammo.interactable = true; }

       /* if (sPlayer.Money < AmmoFillCost) {
            AmmoFill.interactable = false;
        } else { AmmoFill.interactable = true; }*/

    }

    public void UpgradeAccuracy() {

        sPlayer.Money -= AccuracyCost;
        AccuracyCost = AccuracyCost * priceHike;
            sPlayer.Spread = sPlayer.Spread * 0.85f;
    }

    public void UpgradeDamage() {

        sPlayer.Money -= DamageCost;
        DamageCost = DamageCost * priceHike;
        sBullet.bulletDamage = sBullet.bulletDamage * priceHike;
    }

    public void UpgradeSpeed() {

        sPlayer.Money -= SpeedCost;
        SpeedCost = SpeedCost * priceHike;
        sPlayer.movePower = sPlayer.movePower * priceHike;

    }

    public void UpgradeJump() {

        sPlayer.Money -= JumpCost;
        JumpCost = JumpCost * priceHike;
        sPlayer.maxJumpPower = sPlayer.maxJumpPower * priceHike;
    }

    public void UpgradeFirerate() {

        sPlayer.Money -= FirerateCost;
        FirerateCost = FirerateCost * priceHike;
        sPlayer.fireInterval = sPlayer.fireInterval * 0.85f;
    }

    public void UpgradeHealth() {

        sPlayer.Money -= HealthCost;
        HealthCost = HealthCost * priceHike;
        sPlayer.health = 100;
    }

    public void UpgradeAmmo() {

        sPlayer.Money -= AmmoCost;
        AmmoCost = AmmoCost * priceHike;
        sBullet.bulletSpeed = sBullet.bulletSpeed * priceHike;
    }

    public void RefillAmmo() {
        
    }
}
