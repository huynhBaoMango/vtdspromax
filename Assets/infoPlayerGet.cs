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
    public TMP_Text hptext1;
    public TMP_Text sptext1;
    public TMP_Text damagetext1;
    public TMP_Text critrtext1;
    public TMP_Text critdtext1;
    public TMP_Text aspeed1;
    public TMP_Text maxbullettext1;
    public TMP_Text reloadtext1;
    public TMP_Text fireratetext1;

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

        hptext1.text = "Take: " + player.GetComponent<PlayerManager>().buffSelectionCounts["HP"];
        sptext1.text = "Take: " + player.GetComponent<PlayerManager>().buffSelectionCounts["Speed"];
        damagetext1.text = "Take: " + player.GetComponent<PlayerManager>().buffSelectionCounts["Damage"];
        critrtext1.text = "Take: " + player.GetComponent<PlayerManager>().buffSelectionCounts["CritRate"];
        critdtext1.text = "Take: " + player.GetComponent<PlayerManager>().buffSelectionCounts["CritDamage"];
        aspeed1.text = "Take: " + player.GetComponent<PlayerManager>().buffSelectionCounts["fireReset"];
        maxbullettext1.text = "Take: " + player.GetComponent<PlayerManager>().buffSelectionCounts["MaxBulletCount"];
        reloadtext1.text = "Take: " + player.GetComponent<PlayerManager>().buffSelectionCounts["reloadSpeed"];
        fireratetext1.text = "Take: " + player.GetComponent<PlayerManager>().buffSelectionCounts["fireRate"];
    }
}
