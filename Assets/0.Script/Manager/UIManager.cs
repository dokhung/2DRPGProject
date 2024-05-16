using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIManager : Singleton<UIManager>
{
    [Header("UiList")]
    public GameObject ShopDialog;
    public GameObject SkillDialog;
    public GameObject StatInfoDialog;
    public GameObject StatInfo_Present;
    public GameObject SkillInfoDialog_archers;
    public GameObject QuestDialog;
    public GameObject HomeDialog;
    public GameObject EquipDialog;
    public GameObject InvenToryUi_Equip;
    public GameObject InvenToryUi_Etc;
    public GameObject EtcBtn;
    public GameObject EquipBtn;
    
    [Header("HomeDialogList")]
    public GameObject SaveSuccessDialog;
    public GameObject ExitDialog;
    public GameObject LoadDialog;
    public GameObject Load_SeletedDialog;
    public GameObject SoundSettingDialog;
    public Slider SoundBar;
    public Text SoundBarNumber;
    
    [Header("GameStopControl")]
    public GameObject STOPButton;
    public Sprite StartImg;
    public Sprite StopImg;
    public bool GamePlay = true;
    private Image StopButtonImg;
    
    
    [Header("PlayerInfo")]
    public Image HP;
    public Text HPTxt;
    public Image MP;
    public Text MPTxt;
    public Image Exp;
    public Text ExpTxt;
    public Text GoldTxt;

    [Header("StatUI")]
    public Text StatLV;
    public Text StatHP;
    public Text StatMaxHP;
    public Text StatMP;
    public Text StatMaxMP;
    public Text StatEXP;
    public Text StatMaxEXP;
    public Text StatATT;
    public Text StatDEF;
    
    
    
    
    
    [Header("PlayerDamegeSkin")]
    public TMP_Text Damege;

    public bool OnDamege = false;
    private float DamegePosition = 0;
    public Transform DamegeInitTarget;
    public GameObject BeHitImage;
    
    //Get Money is Message
    private bool OnInvenTory_Equip = false;
    private bool OnInvenTory_Etc = false;
    private bool OnStat = false;
    

    #region Set

    public int SetHP
    {
        get { return PlayerManager.instance.HP; }
        set
        {
            PlayerManager.instance.HP = value;

            int hp = PlayerManager.instance.HP;
            int maxHP = PlayerManager.instance.MaxHP;
            
            if (hp >= maxHP)
                hp = PlayerManager.instance.HP = maxHP;
            
            HPTxt.text = hp.ToString();
            StatHP.text = hp.ToString();
            HP.rectTransform.sizeDelta = new Vector2(((float)hp / maxHP) * 350f, 40f);
        }
    }

    public int SetMAXHP
    {
        get { return PlayerManager.instance.MaxHP; }
        set
        {
            PlayerManager.instance.MaxHP = value;
            int maxHP = PlayerManager.instance.MaxHP;
            StatMaxHP.text = maxHP.ToString();
        }
    }
    public int SetMP
    {
        get { return PlayerManager.instance.MP; }
        set
        {
            PlayerManager.instance.MP = value;

            int mp = PlayerManager.instance.MP;
            int maxMP = PlayerManager.instance.MaxMP;
            if (mp >= maxMP)
                mp = PlayerManager.instance.MP = maxMP;
            // StatMP.text = mp.ToString();
            MP.rectTransform.sizeDelta = new Vector2(((float)mp / maxMP) * 350f, 40f);

        }
    }
    public int SetMAXMP
    {
        get { return PlayerManager.instance.MaxMP; }
        set
        {
            PlayerManager.instance.MaxMP = value;
            int maxMP = PlayerManager.instance.MaxMP;
            StatMaxMP.text = maxMP.ToString();
        }
    }
    public int SetEXP
    {
        get { return PlayerManager.instance.Exp; }
        set
        {
            PlayerManager.instance.Exp = value;

            int exp = PlayerManager.instance.Exp;
            int maxexp = PlayerManager.instance.MaxExp;
            if (exp >= maxexp)
                exp = PlayerManager.instance.Exp = maxexp;
            ExpTxt.text = exp.ToString();
            StatEXP.text = exp.ToString();
            Exp.rectTransform.sizeDelta = new Vector2(((float)exp / maxexp) * 350f, 40f);
        }
    }
    public int SetMAXEXP
    {
        get { return PlayerManager.instance.MaxExp; }
        set
        {
            PlayerManager.instance.MaxExp = value;
            int maxExp = PlayerManager.instance.MaxExp;
            StatMaxHP.text = maxExp.ToString();
        }
    }
    public int SetGold
    {
        get
        {
            return PlayerManager.instance.Gold;
        }
        set
        {
            PlayerManager.instance.Gold = value;
            int gold = PlayerManager.instance.Gold;
            GoldTxt.text = gold.ToString();

        }
    }

    public int SetLevel
    {
        get
        {
            return PlayerManager.instance.Level;
        }
        set
        {
            PlayerManager.instance.Level = value;
            int level = PlayerManager.instance.Level;
            StatLV.text = level.ToString();
        }
    }

    public int SetAtt
    {
        get
        {
            return PlayerManager.instance.Att;
        }
        set
        {
            PlayerManager.instance.Att = value;
            int att = PlayerManager.instance.Att;
            StatATT.text = att.ToString();
        }
    }

    public int SetDef
    {
        get
        {
            return PlayerManager.instance.Def;
        }
        set
        {
            PlayerManager.instance.Def = value;
            int def = PlayerManager.instance.Def;
            StatDEF.text = def.ToString();
        }
    }
    #endregion
    
    

    private void Start()
    {
        // StatUI(false);
        ShopDialog.SetActive(false);
        SkillDialog.SetActive(false);
        Damege.gameObject.SetActive(false);
        BeHitImage.SetActive(false);
        InvenToryUi_Etc.SetActive(false);
        InvenToryUi_Equip.SetActive(false);
        EtcBtn.SetActive(false);
        EquipBtn.SetActive(false);
        StatInfoDialog.SetActive(false);
        SkillInfoDialog_archers.SetActive(false);
        EquipDialog.SetActive(false);
        StopButtonImg = STOPButton.GetComponent<Image>();
    }

    #region Update

    private void Update()
    {
        //Damege
        if (OnDamege)
        {
            DamegePosition += Time.deltaTime;
            Damege.transform.position = DamegeInitTarget.transform.position;
            OnDamege = false;
            Invoke("PlayerBeHitDamegeTime",1);
        }
        
        // ETC_Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnInvenTory_Etc = !OnInvenTory_Etc;
            InvenToryUi_Etc.SetActive(OnInvenTory_Etc);
        }
        
        // Equip_Inventory
        if (Input.GetKeyDown(KeyCode.G))
        {
            OnInvenTory_Equip = !OnInvenTory_Equip;
            InvenToryUi_Equip.SetActive(OnInvenTory_Equip);
        }
        
        //Stat
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     OnStat = !OnStat;
        //     StatUi.SetActive(OnStat);
        // }
    }

    #endregion
    

    public void CallShopUI(bool isShow)
    {
        ShopDialog.SetActive(isShow); 
    }

    public void CallSkillUI(bool isShow)
    {
        SkillDialog.SetActive(isShow);
    }

    public void CallInventory_EtcUI(bool isShow)
    {
        InvenToryUi_Etc.SetActive(isShow);
        EquipBtn.SetActive(true);
        EtcBtn.SetActive(true);
        if (!isShow)
        {
            EquipBtn.SetActive(false);
            EtcBtn.SetActive(false);
        }
    }
    
    public void CallInventory_EquipUI(bool isShow)
    {
        InvenToryUi_Equip.SetActive(isShow);
        if (!isShow)
        {
            EquipBtn.SetActive(false);
            EtcBtn.SetActive(false);
        }
    }

    public void CallQuestUI(bool isShow)
    {
        QuestDialog.SetActive(isShow);
    }

    public void CallHomeUI(bool isShow)
    {
        HomeDialog.SetActive(isShow);
    }

    public void OpenEquip()
    {
        InvenToryUi_Equip.SetActive(true);
        OnInvenTory_Equip = true;
        InvenToryUi_Etc.SetActive(false);
        OnInvenTory_Etc = false;
    }

    public void OpenEtc()
    {
        InvenToryUi_Etc.SetActive(true);
        OnInvenTory_Etc = true;
        InvenToryUi_Equip.SetActive(false);
        OnInvenTory_Equip = false;
    }

    public void CallStatInfoUI(bool isShow)
    {
        StatInfoDialog.SetActive(isShow);
    }

    public void CallStatInfo_PresentUI(bool isShow)
    {
        StatInfo_Present.SetActive(isShow);
    }

    public void SkillInfoDialog_archersUI(bool isShow)
    {
        SkillInfoDialog_archers.SetActive(isShow);
    }

    public void CallEquipUI(bool isShow)
    {
        EquipDialog.SetActive(isShow);
    }
    public void PlayerBeHitDamege(int damage)
    {
        BeHitImage.SetActive(true);
        Invoke("BeHitTime",1f);
        this.Damege.text = damage.ToString();
        Damege.transform.DOMoveY(1, 1).SetRelative();
        Damege.gameObject.SetActive(true);
        OnDamege = true;
    }

    public void GameTimeControl()
    {
        // 재생중이면 트루
        // 정지중이면 펄스
        if (!GamePlay)
        {
            GamePlay = true;
            Time.timeScale = 0;
            StopButtonImg.sprite = StartImg;
            
        }
        else
        {
            GamePlay = false;
            Time.timeScale = 1;
            StopButtonImg.sprite = StopImg;
        }
    }
    public void PlayerBeHitDamegeTime()
    {
        Damege.gameObject.SetActive(false);
        //Damege.transform.position = DamegeInitTarget.transform.position;
    }
    
    public void BeHitTime()
    {
        BeHitImage.SetActive(false);
    }
}
// UI는 업데이트가 필요없음
// 그 시점에 발동이 되어야한다. 인스턴스로 계속 하지말고
// 돌아갈 필요가 없는거를 확인
// SetStat
// Open쪽은 bool 사용을 하자.