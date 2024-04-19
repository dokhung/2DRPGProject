public class Stat
{
   // 플레이어
   public struct PlayerStat
   {
      public int HP;
      public int MaxHP;
      public int MP;
      public int MaxMP;
      public int Att;
      public int Skill;
      public int ExpVal;
      public int MaxExpVal;
      public int Level;
      public int Money;
      public int Def;

      public PlayerStat(int hp,int mp, int att, int skill, int exp, int maxEXP, int level, int money, int DEF)
      {
         this.HP = hp;
         this.MaxHP = hp;
         this.MP = mp;
         this.MaxMP = mp;
         this.Att = att;
         this.Skill = skill;
         this.ExpVal = exp;
         this.MaxExpVal = maxEXP;
         this.Level = level;
         this.Money = money;
         this.Def = DEF;
      }
   }
}