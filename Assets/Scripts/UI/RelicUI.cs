using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicUI : MonoBehaviour
{
    //public PlayerController player;
    //public int index;

    //public Image icon;
    //public GameObject highlight;
    //public TextMeshProUGUI label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        // if a player has relics, this is how you *could* show them
        //player = GameManager.Instance.player;
        //Relic r = RelicSystem.Instance.activeRelics[index];
    //}

    // Update is called once per frame
    //void Update()
    //{
        // Relics could have labels and/or an active-status
        //Relic r = player.relics[index];
        //label.text = r.GetLabel();
        //highlight.SetActive(r.IsActive());
    //}

    public Image icon;
    public GameObject highlight;
    public Relic relic;
    void Start()
    {
        //last_text_update = 0;
    }

    public void SetRelic(Relic relic)
    {
        this.relic = relic;
        icon = this.GetComponent<Image>();
        icon.enabled = true;
        GameManager.Instance.relicIconManager.PlaceSprite(relic.sprite, icon);
    }

    void Update()
    {

    }
}
