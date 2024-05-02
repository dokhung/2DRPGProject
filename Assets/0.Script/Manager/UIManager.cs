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
    public GameObject ShopUi;
    public GameObject StatUi;
    public GameObject EquipmentUi;
    public GameObject SkillUi;
    public GameObject MenuUi;
    public GameObject InvenToryUi;
    public GameObject InvenToryUi_Equip;
    public GameObject InvenToryUi_Etc;
    
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

    private bool OnInvenTory = false;
    private bool OnInvenTory_Equip = false;
    private bool OnInvenTory_Etc = false;
    private bool OnStat = false;
    

    #region Set

    public int SetHP
    {
        get { return PlayerManager.instance.playerStat.HP; }
        set
        {
            PlayerManager.instance.playerStat.HP = value;

            int hp = PlayerManager.instance.playerStat.HP;
            int maxHP = PlayerManager.instance.playerStat.MaxHP;
            
            if (hp >= maxHP)
                hp = PlayerManager.instance.playerStat.HP = maxHP;
            
            HPTxt.text = hp.ToString();
            HP.rectTransform.sizeDelta = new Vector2(((float)hp / maxHP) * 350f, 40f);
        }
    }

    public int SetMAXHP
    {
        get { return PlayerManager.instance.playerStat.MaxHP; }
        set
        {
            PlayerManager.instance.playerStat.MaxHP = value;
            int maxHP = PlayerManager.instance.playerStat.MaxHP;
            StatMaxHP.text = maxHP.ToString();
        }
    }
    public int SetMP
    {
        get { return PlayerManager.instance.playerStat.MP; }
        set
        {
            PlayerManager.instance.playerStat.MP = value;

            int mp = PlayerManager.instance.playerStat.MP;
            int maxMP = PlayerManager.instance.playerStat.MaxMP;
            if (mp >= maxMP)
                mp = PlayerManager.instance.playerStat.MP = maxMP;
            MPTxt.text = mp.ToString();
            MP.rectTransform.sizeDelta = new Vector2(((float)mp / maxMP) * 350f, 40f);

        }
    }
    public int SetMAXMP
    {
        get { return PlayerManager.instance.playerStat.MaxMP; }
        set
        {
            PlayerManager.instance.playerStat.MaxMP = value;
            int maxMP = PlayerManager.instance.playerStat.MaxMP;
            StatMaxMP.text = maxMP.ToString();
        }
    }
    public int SetEXP
    {
        get { return PlayerManager.instance.playerStat.Exp; }
        set
        {
            PlayerManager.instance.playerStat.Exp = value;

            int exp = PlayerManager.instance.playerStat.Exp;
            int maxexp = PlayerManager.instance.playerStat.MaxExp;
            if (exp >= maxexp)
                exp = PlayerManager.instance.playerStat.Exp = maxexp;
            ExpTxt.text = exp.ToString();
            Exp.rectTransform.sizeDelta = new Vector2(((float)exp / maxexp) * 350f, 40f);
        }
    }
    public int SetMAXEXP
    {
        get { return PlayerManager.instance.playerStat.MaxExp; }
        set
        {
            PlayerManager.instance.playerStat.MaxExp = value;
            int maxExp = PlayerManager.instance.playerStat.MaxExp;
            StatMaxHP.text = maxExp.ToString();
        }
    }
    public int SetGold
    {
        get
        {
            return PlayerManager.instance.playerStat.Gold;
        }
        set
        {
            PlayerManager.instance.playerStat.Gold = value;
            int gold = PlayerManager.instance.playerStat.Gold;
            GoldTxt.text = gold.ToString();

        }
    }

    public int SetLevel
    {
        get
        {
            return PlayerManager.instance.playerStat.Level;
        }
        set
        {
            PlayerManager.instance.playerStat.Level = value;
            int level = PlayerManager.instance.playerStat.Level;
            StatLV.text = level.ToString();
        }
    }

    public int SetAtt
    {
        get
        {
            return PlayerManager.instance.playerStat.Att;
        }
        set
        {
            PlayerManager.instance.playerStat.Att = value;
            int att = PlayerManager.instance.playerStat.Att;
            StatATT.text = att.ToString();
        }
    }

    public int SetDef
    {
        get
        {
            return PlayerManager.instance.playerStat.Def;
        }
        set
        {
            PlayerManager.instance.playerStat.Def = value;
            int def = PlayerManager.instance.playerStat.Def;
            StatDEF.text = def.ToString();
        }
    }
    #endregion
    
    

    private void Start()
    {
        StatUI(false);
        ShopUI(false);
        EquipmentUI(false);
        SkillUI(false);
        MenuUI(false);
        Damege.gameObject.SetActive(false);
        BeHitImage.SetActive(false);
        OpenInventory(false);
        
        // 시작때 1번만 적용
        // HPTxt.text = PlayerManager.instance.playerStat.HP.ToString();
        // MPTxt.text = PlayerManager.instance.playerStat.MP.ToString();
        // ExpTxt.text = PlayerManager.instance.playerStat.Exp.ToString();
        // GoldTxt.text = PlayerManager.instance.playerStat.Gold.ToString();
        // StatLV.text = PlayerManager.instance.playerStat.Level.ToString();
        // StatHP.text = PlayerManager.instance.playerStat.HP.ToString();
        // StatMaxHP.text = PlayerManager.instance.playerStat.MaxHP.ToString();
        // StatMP.text = PlayerManager.instance.playerStat.MP.ToString();
        // StatMaxMP.text = PlayerManager.instance.playerStat.MaxMP.ToString();
        // StatEXP.text = PlayerManager.instance.playerStat.Exp.ToString();
        // StatMaxEXP.text = PlayerManager.instance.playerStat.MaxExp.ToString();
        // StatATT.text = PlayerManager.instance.playerStat.Att.ToString();
        // StatDEF.text = PlayerManager.instance.playerStat.Def.ToString();
        // HPTxt.text = SetHP.ToString();
        // StatHP.text = SetHP.ToString();

    }


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

        if (Input.GetKeyDown(KeyCode.I))
        {
            OnInvenTory = !OnInvenTory;
            InvenToryUi.SetActive(OnInvenTory);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            OnInvenTory_Equip = !OnInvenTory_Equip;
            InvenToryUi_Equip.SetActive(OnInvenTory_Equip);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OnStat = !OnStat;
            StatUi.SetActive(OnStat);
        }
    }

    public void ShopUI(bool isShow)
    {
        ShopUi.SetActive(isShow); 
    }

    public void StatUI(bool isShow)
    {
        Debug.Log(isShow);
        StatUi.SetActive(isShow);
    }

    public void EquipmentUI(bool isShow)
    {
        EquipmentUi.SetActive(isShow);
    }

    public void SkillUI(bool isShow)
    {
        SkillUi.SetActive(isShow);
    }

    public void MenuUI(bool isShow)
    {
        MenuUi.SetActive(isShow);
    }

    public void OpenInventory(bool isShow)
    {
        InvenToryUi.SetActive(isShow);
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

    public void PlayerBeHitDamege(int damage)
    {
        BeHitImage.SetActive(true);
        Invoke("BeHitTime",1f);
        this.Damege.text = damage.ToString();
        Damege.transform.DOMoveY(1, 1).SetRelative();
        Damege.gameObject.SetActive(true);
        OnDamege = true;
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