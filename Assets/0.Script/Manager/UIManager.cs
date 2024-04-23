using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditorInternal;
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
    

    #region 새로운 코드
    [System.Serializable]
    public class PlayerInfo
    {
        public Image HP;
        public Text HPTxt;
        public Image MP;
        public Text MPTxt;
        public Image Exp;
        public Text ExpTxt;
        public Text GoldTxt;
    }
    public PlayerInfo pInfo;

    public int SetHP
    {
        get { return PlayerManager.instance.PlayerStatInfo.HP; }
        set
        {
            PlayerManager.instance.PlayerStatInfo.HP = value;

            int hp = PlayerManager.instance.PlayerStatInfo.HP;
            int maxHP = PlayerManager.instance.PlayerStatInfo.MaxHP;
            
            if (hp >= maxHP)
                hp = PlayerManager.instance.PlayerStatInfo.HP = maxHP;
            
            pInfo.HPTxt.text = hp.ToString();
            pInfo.HP.rectTransform.sizeDelta = new Vector2(((float)hp / maxHP) * 350f, 40f);
        }
    }
    #endregion

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


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Insert))
        {
            SetHP -= 10;
        }
        
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
    }

    public void ShopUI(bool isShow)
    {
        ShopUi.SetActive(isShow); 
    }

    public void StatUI(bool isShow)
    {
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