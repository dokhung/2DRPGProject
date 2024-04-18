using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    
    
    
    [Header("StatUi")]
    public Text Stat_LV;
    public Text Stat_HP;
    public Text Stat_MaxHP;
    public Text Stat_MP;
    public Text Stat_MaxMp;
    public Text Stat_Att;
    public Text Stat_ExpVal;
    public Text Stat_MaxExpVal;
    public Text Stat_Skill;
    public Text Stat_Money;
    public Text Stat_DEF;
    public Slider HPbar;
    public Slider MPbar;
    public Slider EXPbar;
    public Text HPText;
    public Text MPText;
    public Text EXPText;
    
    [Header("PlayerDamegeSkin")]
    public TMP_Text Damege;

    public bool OnDamege = false;
    private float DamegePosition = 0;
    public Transform DamegeInitTarget;
    public GameObject BeHitImage;

    [Header("SHOP")] 
    public Text SHOPholdMoney;
    
    //Get Money is Message

    private bool OnInvenTory = false;
    private bool OnInvenTory_Equip = false;
    private bool OnInvenTory_Etc = false;

    public Stat.PlayerStat _plyaerInfo;

    public Stat.PlayerStat PlyaerInfo
    {
        get { return _plyaerInfo; }
        set
        {
            _plyaerInfo = value;
            UpdateUI();
        }
    }
    
    
    

    private void Start()
    {
        ShopUi.SetActive(false);
        StatUi.SetActive(false);
        EquipmentUi.SetActive(false);
        SkillUi.SetActive(false);
        MenuUi.SetActive(false);
        InvenToryUi.SetActive(false);
        Damege.gameObject.SetActive(false);
        BeHitImage.SetActive(false);
    }

    private void UpdateUI()
    {
        //능력치 뷰
        Stat_LV.text = PlayerManager.instance.PlayerStatInfo.Level.ToString();
        Stat_HP.text = PlayerManager.instance.PlayerStatInfo.HP.ToString();
        Stat_MaxHP.text = PlayerManager.instance.PlayerStatInfo.MaxHP.ToString();
        Stat_MP.text = PlayerManager.instance.PlayerStatInfo.MP.ToString();
        Stat_MaxMp.text = PlayerManager.instance.PlayerStatInfo.MaxMP.ToString();
        Stat_Att.text = PlayerManager.instance.PlayerStatInfo.Att.ToString();
        Stat_ExpVal.text = PlayerManager.instance.PlayerStatInfo.ExpVal.ToString();
        Stat_MaxExpVal.text = PlayerManager.instance.PlayerStatInfo.MaxExpVal.ToString();
        Stat_Money.text = PlayerManager.instance.PlayerStatInfo.Money.ToString();
        Stat_DEF.text = PlayerManager.instance.PlayerStatInfo.Def.ToString();
        // // 채력바 뷰의 숫자 수치
        HPText.text = PlayerManager.instance.PlayerStatInfo.HP.ToString();
        MPText.text = PlayerManager.instance.PlayerStatInfo.MP.ToString();
        EXPText.text = PlayerManager.instance.PlayerStatInfo.ExpVal.ToString();
        // SHOP MONEY
        SHOPholdMoney.text = PlayerManager.instance.PlayerStatInfo.Money.ToString();


        // // 채력바 뷰
        HPbar.maxValue = PlayerManager.instance.PlayerStatInfo.MaxHP;
        HPbar.value = PlayerManager.instance.PlayerStatInfo.HP;
        MPbar.maxValue = PlayerManager.instance.PlayerStatInfo.MaxMP;
        MPbar.value = PlayerManager.instance.PlayerStatInfo.MP;
        EXPbar.value = PlayerManager.instance.PlayerStatInfo.ExpVal;
        EXPbar.maxValue = PlayerManager.instance.PlayerStatInfo.MaxExpVal;
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
            if (!OnInvenTory)
            {
                InvenToryUi.SetActive(true);
                OnInvenTory = true; 
            }
            else if (OnInvenTory)
            {
                InvenToryUi.SetActive(false);
                OnInvenTory = false; 
            }
            
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!OnInvenTory_Equip)
            {
                InvenToryUi_Equip.SetActive(true);
                OnInvenTory_Equip = true;
            }
            else if (OnInvenTory_Equip)
            {
                InvenToryUi_Equip.SetActive(false);
                OnInvenTory_Equip = false;
            }
        }
        
    }


    public void OpenShop()
    {
        ShopUi.SetActive(true); 
    }

    public void ClosedShop()
    {
        ShopUi.SetActive(false);
    }

    public void OpenStatUi()
    {
        StatUi.SetActive(true);
    }

    public void CloseStatUi()
    {
        StatUi.SetActive(false);
    }

    public void OpenEquipmentUi()
    {
        EquipmentUi.SetActive(true);
    }

    public void CloseEquipmentUi()
    {
        EquipmentUi.SetActive(false);
    }

    public void OpenSkillUi()
    {
        SkillUi.SetActive(true);
    }

    public void CloseSkillUi()
    {
        SkillUi.SetActive(false);
    }

    public void OpenMenuUi()
    {
        MenuUi.SetActive(true);
    }

    public void CloseMenuUi()
    {
        MenuUi.SetActive(false);
        // Instantiate(슬롯프리팹, 인벤토리 슬롯의 부모의 transform) 설정
    }

    public void OpenInventory()
    {
        InvenToryUi.SetActive(true);
    }

    public void CloseInventory()
    {
        InvenToryUi.SetActive(false);
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
// SetStatㄷ
// Open쪽은 bool 사용을 하자.