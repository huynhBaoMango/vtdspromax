using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class infoPlayerGet : MonoBehaviour
{
    public TMP_Text hptext;
    public TMP_Text sptext;
    public TMP_Text damagetext;
    public TMP_Text critrtext;
    public TMP_Text critdtext;
    public TMP_Text aspeed;
    public TMP_Text maxbullettext;
    public TMP_Text reloadtext;
    public TMP_Text fireratetext;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("PLAYER");
    }

    // Update is called once per frame
    void Update()
    {
        hptext.text = player.GetComponent<PlayerHealth>().currentHealth + "/" + player.GetComponent<PlayerManager>().maxHP;
        sptext.text = player.GetComponent<PlayerManager>().speed + "";
        damagetext.text = player.GetComponent<PlayerManager>().damage + "";
        critrtext.text = player.GetComponent<PlayerManager>().critRate * 10 + " %";
        critdtext.text = player.GetComponent<PlayerManager>().critDamage * 10 + " %";
        aspeed.text = player.GetComponent<PlayerManager>().fireReset + "";
        maxbullettext.text = player.GetComponent<PlayerManager>().currentBulletCount + "/" +player.GetComponent<PlayerManager>().maxBulletCount;
        reloadtext.text = player.GetComponent<PlayerManager>().reloadSpeed + "";
        fireratetext.text = player.GetComponent<PlayerManager>().fireRate + "";
    }
}
