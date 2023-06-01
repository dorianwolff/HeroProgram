namespace HeroProgramm;

public class Monster
{
    public string monsterName;
    public string monsterType; //normal, boss, demon king
    public int luck=0;
    public int monsterATK=0;
    public int monsterHP=0;
    public bool alive=true;
    public int giveEXP;

}

public static class allMonsters
{
    public static List<Monster> monsters;

    public static void initializeMonsterList()
    {
        //All level 1 stats
        
        //0
        monsters = new List<Monster>(); 
        Monster w1 = new Monster();
        w1.monsterName = "Copycat";
        w1.monsterType = "mimic";
        //w1.monsterATK = 1; = player's atk
        //w1.monsterHP = 1; = player's hp
        w1.alive = true;
        w1.giveEXP = 15;
        monsters.Add(w1);
        
        //1
        Monster w2 = new Monster();
        w2.monsterName = "Goblin Warrior";
        w2.monsterType = "goblin";
        w2.luck = 1;
        w2.monsterATK = 3;
        w2.monsterHP = 10;
        w2.alive = true;
        w2.giveEXP = 5;
        monsters.Add(w2);
        
        //2
        Monster w3 = new Monster();
        w3.monsterName = "Succubus";
        w3.monsterType = "demon";
        w2.luck = 6;
        w3.monsterATK = 10;
        w3.monsterHP = 20;
        w3.alive = true;
        w3.giveEXP = 30;
        monsters.Add(w3);
        
        //3
        Monster w4 = new Monster();
        w4.monsterName = "Goblin General";
        w4.monsterType = "goblin";
        w4.luck = 1;
        w4.monsterATK = 7;
        w4.monsterHP = 16;
        w4.alive = true;
        w4.giveEXP = 15;
        monsters.Add(w4);
        
        //4
        Monster w5 = new Monster();
        w5.monsterName = "Doom Bringer";
        w5.monsterType = "demon";
        w5.luck = 3;
        w5.monsterATK = 10;
        w5.monsterHP = 25;
        w5.alive = true;
        w5.giveEXP = 35;
        monsters.Add(w5);
        
        //5
        Monster w6 = new Monster();
        w6.monsterName = "Demon Lord";
        w6.monsterType = "demon";
        w6.luck = 15;
        w6.monsterATK = 22;
        w6.monsterHP = 55;
        w6.alive = true;
        w6.giveEXP = 75;
        monsters.Add(w6);
    }

    public static void LevelUpMonsters()
    {
        Random rd = new Random();
        int statBoost = rd.Next(0, 3);
        foreach (var monster in monsters)
        {
            if (statBoost == 0)
            {
                monster.luck += 2;
                monster.monsterHP += 2;
                monster.monsterATK += 1;
            }
            else if (statBoost == 1)
            {
                monster.luck += 0;
                monster.monsterHP += 5;
                monster.monsterATK += 1;
            }
            else
            {
                monster.luck += 1;
                monster.monsterHP += 2;
                monster.monsterATK += 3;
            }
        }
    }
}
