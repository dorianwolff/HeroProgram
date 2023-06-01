using System.Reflection.Metadata;

namespace HeroProgramm;

public static class Scenarios
{
    
    //Kingdom
    public static void EnteredSlums(Hero Player) 
    {
        Console.WriteLine("- You have entered the slums. You smell piss, vomit and dried firewood.\n"+
                          "  You venture within the slums and see nothing but misery.\n" +
                          "  Perhaps if you had the right amount of help and luck, you could\n" +
                          "  manage to change this miserable place...");
        Console.WriteLine("- Do you wish to abandon some luck?\n" +
                          "  Type the number of luck you want to get rid or press enter");
        string value = Console.ReadLine();
        int number;
        bool success = int.TryParse(value, out number);
        if (success && number > 0 && number <= Player.luck)
        {
            Player.luck -= number;
        }
        if (Player.luck<5&&!Player.lostGold)
        {
            if (Player.gold < 100)
            {
                Console.WriteLine("- You are unlucky, you lost "+Player.gold+" gold!");
                Player.gold = 0;
            }
            else
            {
                Player.gold -= 100;
                Console.WriteLine("- You are unlucky, you lost 100 gold!");
            }

            Player.lostGold = true;
        }

        if (Player.luck == 7&&!Player.foundPeoplesSword)
        {
            Console.WriteLine("- The whole slums begin to tremble. \n"+
                              "  and bellow your feet appears a... stick? \n" +
                              "  Do you wish to pick up the stick?\n"+
                              "  yes | no");
            string action = Console.ReadLine();
            switch (action)
            {
                case "2":
                    Console.WriteLine("- Pussy cat...");
                    break;
                //yes
                default:
                    Console.WriteLine("- You have obtained a legendary-grade weapon! \n" +
                                      "  [the people's will] has been added to your inventory.");
                    obtainAWeapon(Player,4);
                    Player.foundPeoplesSword = true;
                    
                    break;
            }

        }

        if (!Player.foughtCopyCat)
        {
            Console.WriteLine("- As you continue your journey in the slums, night falls. You hear the wind brushing\n" +
                              "  past you and a sudden chill runs down your spine as you see a shadow leap out from the\n" +
                              "  ground in front of you.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("- You see it's bright white teeth even from within the darkness and its blue gloomy eyes,\n"+
                              "  staring right through you. It then proceeds to meld, twist... change.\n"+
                              "  You have encountered a copycat.\n" +
                              "- Do you wish to fight?\n" +
                              "  yes | no");
            string action2 = Console.ReadLine();
            Console.Clear();
            switch (action2)
            {
                case "2":
                    Console.WriteLine("- Continue to run... You can't even face yourself.");
                    break;
                //yes
                default:
                    Console.WriteLine("- You have decided to fight! ");
                    if (allMonsters.monsters[0].alive)
                        CombatMechanics.Fight(Player, allMonsters.monsters[0]);
                    else
                    {
                        allMonsters.monsters[0].alive = true;
                        CombatMechanics.Fight(Player, allMonsters.monsters[0]);
                    }
                    Player.foughtCopyCat = true;
                    break;
            }
            Console.WriteLine("- After this series of events, you decide to change locations.\n" +
                              "  Do you wish to visit :\n" +
                              "  The Adventurer's Guild | The local Inn");
            string firstAction = Console.ReadLine();
            switch (firstAction)
            {
                case "2":
                    EnteredInn(Player);
                    break;
                //case 1
                default:
                    EnteredGuild(Player);
                    break;
            }
        }
        else
        {
            Console.WriteLine("- After this series of events, you decide to change locations.\n" +
                              "  Do you wish to visit :\n" +
                              "  The Adventurer's Guild | The local Inn");
            string action3 = Console.ReadLine();
            switch (action3)
            {
                case "2":
                    EnteredInn(Player);
                    break;
                //case 1
                default:
                    EnteredGuild(Player);
                    break;
            }
        }
        
        

    } 
    
    public static void EnteredGuild(Hero Player)
    {
        Console.WriteLine("- You have arrived at the Guild.\n" +
                          "  It is crowded. You smell smoke, hear laughs and cries. You let out\n" +
                          "  a quick smirk as you find your way to a table, as you think of how .\n" +
                          "  cozy and agreeable this peaceful place is. \n" +
                          "  You calmly order and drink booze, then take out your fortune in order to pay.\n" +
                          "  Tip?! The price will go up by 10 gold every time! Beware if you can't pay!");
        if (Player.gold < Player.freeGuildStats)
        {
            Console.WriteLine("- You don't have enough money!");
            Death("the bar tender because you stole booze", Player);
        }
        else
        {
            Random rd = new Random();
            int randStat = rd.Next(1, 4);
            Console.WriteLine("- You have bought booze for "+Player.freeGuildStats+" and have "+Player.gold+" remaining.");
            Player.gold -= Player.freeGuildStats;
            Player.freeGuildStats += 10;
            
            
            Console.WriteLine("- You feel very satisfied with your drink and food. You have gained "+randStat+"hp.");
            Player.hp += randStat;
            Console.WriteLine("Enter \"status\" to view your status or press enter to continue...");
            
            string ac = Console.ReadLine();
            Console.WriteLine("Type \"status\" to view your status or press enter to resume...");
            if (ac == "status")
            {
                GameLogic.Status(Player);
            }
            Console.Clear();
            Console.WriteLine("- Do you wish to :\n" +
                              "  Go to the Inn | Go to the Forest | Head to the slums");
            string action2 = Console.ReadLine();
            Console.Clear();
            switch (action2)
            {
                //Forest
                case "2":
                    Console.WriteLine("- You have decided to head out to the Forest.");
                    GameLogic.enteredForest("Forest", Player);
                    break;
                case "3":
                    Console.WriteLine("- You have decided to go to the slums.");
                    EnteredSlums(Player);
                    break;
                //Inn
                default:
                    Console.WriteLine("- You decide to go to a nearby Inn to find some well deserved rest.");
                    EnteredInn(Player);
                    break;
            }
            
        }
    }
    
    public static void EnteredInn(Hero Player)
    {
        if (allMonsters.monsters[2].alive)
        {
            Console.WriteLine("- You have entered the local Inn.\n" +
                          "  It is dark. You make out a young and slightly pale figure standing behind\n" +
                          "  the front desk. You walk up to her and ask :\n" +
                          "  \"May I have a room for the night?\" | \"I'll take a room, but please bring me a glass of wine first.\"");
        string action = Console.ReadLine();
        Console.Clear();
        if (action == "status")
        {
            GameLogic.Status(Player);
            EnteredGuild(Player);
        }
        switch (action)
        {
            case "2":
                Console.WriteLine("- She looks at you closely and you begin to distinguish her attractive figure...\n" +
                                  "  Then she serves you a glass of red wine and joins you at the bar.\n" );
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("- She smiles. Then serves you another glass. You want to refuse but can't.\n" +
                                  "  She proceeds to gently reach out for your mouth and kisses you right on it.\n" +
                                  "  A sensation like you've never felt before fluctuates throughout your body.\n" +
                                  "  You feel warm, happy, loved? ...");
                Console.WriteLine("Press enter to resume...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("- Cold?");
                Death("sex-driven attractive succubus", Player);
                break;
            default:
                Console.WriteLine("- She reaches out under the desk and pulls out a set of keys. You thank her\n" +
                                  "  and ask for the price. She smiles and tells you the first night is free if\n" +
                                  "  you intend to stay for more than 3 days, also your room is on the first floor.\n" +
                                  "  Even though suspicious you gratefully accept, as you decided to stay for that long.");
                Console.WriteLine("Press enter to resume...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("- You thank her once more and head upstairs to your room.\n" +
                                  "  In the corner of your eye, as you are heading upstairs, you see her smile in a vey\n" +
                                  "  odd smile, so odd that it doesn't look human.\n" +
                                  "  You open the door to your room and realise there is no lock.\n" +
                                  "  As these series of events seem overly suspicious, you decide to stand guard and\n" +
                                  "  grab your weapon as you sit on the bed.\n");
                Console.WriteLine("Press enter to resume...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("- You dose off after an hour and fall asleep.");
                Console.WriteLine("Press enter to resume...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("- Suddenly, you hear the door squeak open. And you see her, naked... With jet black wings?\n" +
                                  "  You have encountered a succubus and have no other choice but to fight.");
                Console.WriteLine("Press enter to resume...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("- You have not fallen pray to her charms and gain 2 random stat points!");
                StatPoint(Player, 2);
                if (allMonsters.monsters[2].alive)
                    CombatMechanics.Fight(Player, allMonsters.monsters[2]);
                else
                {
                    allMonsters.monsters[2].alive = true;
                    CombatMechanics.Fight(Player, allMonsters.monsters[2]);
                }

                if (!allMonsters.monsters[2].alive)
                {
                    Console.WriteLine("- Congrats! You have earned 2 extra luck and a legendary weapon, void&oblivion!");
                    Player.luck += 2;
                    obtainAWeapon(Player,5);
                    Console.WriteLine("- You have defeated the deadly succubus and decide to get some sleep.");
                    Console.WriteLine("Press enter to resume...");
                    Console.ReadLine();
                    Console.Clear();
                }
                
                break;
        }
        }
        
        Console.WriteLine("- You got a good night's sleep.");
        Console.WriteLine("- You decide to head to the guild.");
        Console.WriteLine("Press enter to resume...");
        Console.ReadLine();
        Console.Clear();
        EnteredGuild(Player);
        
    }
    
    
    //Dungeon
    
    public static void EscapesDungeon(Hero Player)
    {
        Console.WriteLine("- You have chose to escape the dungeon. You are brave, but not foolish!");
        if (Player.prestige > 0)
        {
            Player.prestige -= 1;
            Console.WriteLine("  However, you have lost 1 prestige for your cowardliness.");
        }
        GameLogic.enteredForest("Forest",Player);
    }
    
    public static void ConfrontsDungeon(Hero Player)
    {
        if (!Player.savedElfWarrior)
        {
            bool fled = false;
        Console.Clear();
        Console.WriteLine("- As you head toward the inner dept of the dungeon, you hear cries of anger followed\n" +
                          "  by cries of anguish. You pick up your pace and increase your senses of your surroundings,\n" +
                          "  eager to discover the creatures letting out such cries.\n" +
                          "  The scene you witness is cold and unsettling. A goblin warrior is pinning a wounded \n" +
                          "  warrior. Suddenly, the nameless warrior's head is hit with the goblin's massive axe.\n" +
                          "  They can be saved, you thought.\n" +
                          "  Do you want to :\n" +
                          "  Fight | Flee");
        string action2 = Console.ReadLine();
        switch (action2)
        {
            case "2":
                Console.WriteLine("- Continue to run... You can't even face a measly goblin.");
                GameLogic.enteredForest("Forest", Player);
                fled = true;
                break;
            case "status":
                GameLogic.Status(Player);
                ConfrontsDungeon(Player);
                break;
            //yes
            default:
                Console.WriteLine("- You have decided to fight! ");
                if (allMonsters.monsters[1].alive)
                    CombatMechanics.Fight(Player, allMonsters.monsters[1]);
                else
                {
                    allMonsters.monsters[1].alive = true;
                    CombatMechanics.Fight(Player, allMonsters.monsters[1]);
                }
                Console.WriteLine("- The nameless warrior is alive! \n" +
                                  "  Do you want to :\n" +
                                  "  Leave the Dungeon with him | Continue without him");
                string action3 = Console.ReadLine();
                switch (action3)
                {
                    case "2":
                        Console.WriteLine("- As cruel as it may seem, your journey is about yourself, not others.");
                        Console.ReadLine();
                        EndDungeon(Player);
                        break;
                    //yes
                    default:
                        Player.savedElfWarrior = true;
                        Console.WriteLine("- You have decided to help him. May the world reward your generosity.\n" +
                                          "  You head out of the dungeon with his half alive motionless body.\n" +
                                          "  Your body needs some rest from the previous fight. Your consciousness \n" +
                                          "  is fading...");
                        EnteredElfVillage(Player,true);
                        
                        break;
                }
                break;
        }

        if (fled)
        {
            GameLogic.enteredForest("Forest", Player);
        }
        }
        else
        {
            int monsterIndex = 3;
            if (allMonsters.monsters[3].alive == false)
                monsterIndex = 4;
            if (allMonsters.monsters[4].alive == false)
            {
                Console.WriteLine("- You continue to roam the dungeon, however, no monsters can be found.");
                EndDungeon(Player);
            }
            Console.Clear();
            Console.WriteLine("- You continue to roam the dungeon, eager to seek power and riches.\n" +
                              "  You stumble upon a " + allMonsters.monsters[monsterIndex].monsterName +
                              " of the "+allMonsters.monsters[monsterIndex].monsterType + " tribe\n"+
                              "  Do you want to :\n" +
                              "  Fight | Flee");
            string action2 = Console.ReadLine();
            switch (action2)
            {
                case "2":
                    Console.WriteLine("- You wish to flee, however the "+ allMonsters.monsters[monsterIndex].monsterName+
                                      " manages to lightly wound you. You lost 2 hp.");
                    Player.hp -= 2;
                    CombatMechanics.CheckDeath(Player, "a stupid light cut");
                    Console.ReadLine();
                    GameLogic.enteredForest("Forest", Player);
                    break;
                //fight goblin lord or demon
                default:
                    Console.WriteLine("- You have decided to fight.");
                    if (allMonsters.monsters[monsterIndex].alive)
                        CombatMechanics.Fight(Player, allMonsters.monsters[monsterIndex]);
                    Console.Clear();
                    Console.WriteLine("- You went through a meaningless struggle, but you find a note on the \n" +
                                      "  "+ allMonsters.monsters[monsterIndex].monsterName+"'s corps with written :\n" +
                                      "  \"Humans mustn't assemble the 7, destroy the slums.\"");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("- What could this information mean? You thought to your self. \n" +
                                      "  Do you want to :\n" +
                                      "  Leave the Dungeon | Continue");
                    string action3 = Console.ReadLine();
                    switch (action3)
                    {
                        case "2":
                            Console.WriteLine("- You are on a kill streak, why not continue?");
                            Console.ReadLine();
                            EndDungeon(Player);
                            break;
                        //yes
                        default:
                            Console.WriteLine("- You might indeed need to rest.");
                            GameLogic.enteredForest("Forest", Player);
                        
                            break;
                    }
                    break;
            }
        }
        
    }
    
    public static void EndDungeon(Hero Player)
    {
        Console.Clear();
        Console.WriteLine("- You continue to venture within the depths of the dungeon...\n" +
                          "  You distinguish in the darkness what seems a massive door blocking your path.\n" +
                          "  Many ancient figures decorate it in a frighteningly cleaver way\n" +
                          "  It can only be one thing... The Boss's room.\n" +
                          "  Do you wish to : \n" +
                          "  Tip?! Beware. No option is ever safe.\n"+
                          "  Continue | Leave");
        string firstAction = Console.ReadLine();
        switch (firstAction)
        {
            case "2": //leave
                Console.WriteLine("- You think you are not prepared. As you turn around and head for the exit, \n" +
                                  "  Your mind begins to spins as you get absorbed through the door...");
                bossRoom(Player);
                break;
            //Continue
            default:
                Console.WriteLine("- You try pushing the door wide open but it does not budge...\n"+
                                  "  You think about giving up. Then, suddenly, a flash of light.\n" +
                                  "  Your mind begins to spins as you get absorbed through the door...");
                bossRoom(Player);
                break;
        }
    }
    
    //Magic tower
    
    public static void ClickedOnTheBook(Hero Player)
    {
        Console.WriteLine("- Congrats! You are admitted to the hall of fame as you currently have \n" +
                          "  " + Player.prestige + " prestige\n");
        Console.WriteLine("Press enter to resume...");
        Console.ReadLine();
        Console.WriteLine("- You are awarded the [staff of demise] and 5 prestige for your feats.\n" +
                          "  However, you will be denied access to this area in the future");
        Player.prestige += 5;
        obtainAWeapon(Player, 7);
        GameLogic.Status(Player);
        GameLogic.enteredMagicTower("Magic Tower", Player);
        Console.WriteLine("Press enter to resume...");
        Console.ReadLine();
        Console.Clear();
    }
    
    public static void Pervert(Hero Player)
    {
        if (!Player.tookOldMansChallenge)
        {
            Player.tookOldMansChallenge = true;
            Console.WriteLine("- As you touch the letters, you feel as if you are getting absorbed. The letters are\n" +
                              "  dancing and reforming into a new word, which you unconsciously laugh at when you\n" +
                              "  figure it out. You suddenly phase out only to open your eyes in an immense room \n" +
                              "  where you make out a wide arena with an old man standing in the center.\n" +
                              "  Even from afar, you can tell he meets the criteria of the word.\n" +
                              "  He then proceeds to speak softly but you can hear his words, as if the distance\n" +
                              "  had little to no signification.\n" +
                              "  \"Do you wish to pass my trial young one?\"\n" +
                              "  Tip?! remember what was said in as you enter the magic tower...\n"+
                              "  yes | no");
            string firstAction = Console.ReadLine();
            Console.Clear();
            switch (firstAction)
            {
                case "2": //dont to wish pass the trial
                    Console.WriteLine("- How admirable! This was a test! Please take this spare strength I have\n" +
                                      "  and defeat what and who you want with it!");
                    StatPoint(Player,10);
                    Console.WriteLine("Press enter to resume...");
                    Console.ReadLine();
                    Console.Clear();
                    GameLogic.enteredMagicTower("Magic Tower", Player);
                    break;
                case "status":
                    GameLogic.Status(Player);
                    Console.Clear();
                    Pervert(Player);
                    break;
                //wish to pass the trial
                default:
                    Console.WriteLine("- How unfortunate for you... Well have a nice journey greedy young man...\n" +
                                      "  [You have been forcefully teleported to a specific location]");
                    Console.WriteLine("Press enter to resume...");
                    Console.ReadLine();
                    Console.Clear();
                    GameLogic.enteredMagicTower("Magic Tower", Player);
                    break;
            }
        }
        else
        {
            Console.WriteLine("- The old man is denying you access to this area");
            GameLogic.enteredMagicTower("Magic Tower", Player);
        }
        
        
        
    }
    
    public static void Baka(Hero Player)
    {
        Console.WriteLine("- As you touch the word, the letters reconfigure...\n"+
                          "  One word : \"Baka\"."+
                          "  Weather you know or not, the will of the tower rejects you\n"+
                          "  You will randomly be transported at a location.");
        Random rd = new Random();
        int randomPlace = rd.Next(0, 11);
        switch (randomPlace)
        {
            case 0:
                bossRoom(Player);
                break;
            case 10:
                Console.Clear();
                Console.WriteLine("  Nothing happened lmao!");
                GameLogic.enteredMagicTower("Magic Tower", Player);
                break;
            case 1:
                GameLogic.enteredDungeon("Dungeon", Player);
                break;
            case 5:
                GameLogic.enteredKingdom("Kingdom", Player);
                break;
            default:
                GameLogic.enteredForest("Forest",Player);
                break;
        }

    }
    
    //Forest
    
    public static void forestNorth(Hero Player)
    {
        Console.WriteLine("- You have chosen to head North.");
        GameLogic.enteredKingdom("Kingdom", Player);
    }
    
    public static void forestSouth(Hero Player)
    {
        Console.WriteLine("- You have chosen to head South, and arrive in an elf village.");
        EnteredElfVillage(Player, false);
    }
    
    public static void forestWest(Hero Player)
    {
        Console.WriteLine("- You have chosen to head West.");
        GameLogic.enteredMagicTower("Magic Tower", Player);
    }
    
    public static void forestEast(Hero Player)
    {
        Console.WriteLine("- You have chosen to head East.");
        GameLogic.enteredDungeon("Dungeon", Player);
    }
    
    //Boss Room
    public static void bossRoom(Hero Player)
    {
        Console.WriteLine("- You have ventured within the demon lord's sacred and cursed realms.");
        if (Player.level < 4 || Player.prestige < 5)
        {
            Console.WriteLine("  A huge silhoue...");
            Console.WriteLine("  Do better next Time!");
            Death("demon", Player);
        }
        else
        {
            Console.WriteLine("- You have encountered the Demon Lord and have no choice but to fight.");
            if (allMonsters.monsters[5].alive)
                CombatMechanics.Fight(Player, allMonsters.monsters[5]);
            else
            {
                allMonsters.monsters[5].alive = true;
                CombatMechanics.Fight(Player, allMonsters.monsters[5]);
            }
            
            Victory(Player);
        }
        
    }
    
    //Elf Village

    public static void EnteredElfVillage(Hero Player, bool rescuedWarrior)
    {
        if (!rescuedWarrior)
        {
            Console.WriteLine("Ara Ara...");
            Console.ReadLine();
            Death("salty elf assassin stabbing you from behind", Player);
        }
        else
        {
            Player.savedElfWarrior = true;
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("- Your eyes feel heavy. You hear noises and start to distinguish silhouettes.");
            Console.ReadLine();
            Console.WriteLine("- You wake up, suddenly... ");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("- You are nose to nose with a crowd of elves, intensely staring at you.\n" +
                              "  You see fright, fear, discuss in their eyes. As well as...curiousness?\n" +
                              "  Recognition? You just connected the dots. The warrior you saved was an elf.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("- A man speaks, you shouldn't be able to understand his language, but you just do.\n" +
                              "  Do you wish to follow his tutoring?\n" +
                              "  yes | no");
            string firstAction = Console.ReadLine();
            switch (firstAction)
            {
                case "2": 
                    Console.WriteLine("- A miss... or nor?\n" +
                                      "  You are awarded a potions and a weapon for your chivalrous deeds.");
                    Player.hp +=3;
                    Player.atkDamage +=2;
                    Player.luck += 1;
                    Player.prestige += 1;
                    obtainAWeapon(Player,6);//bow of multiple arrows
                    GameLogic.Status(Player);
                    Console.WriteLine("- However, even though grateful, the elves warn you to not set foot in this\n"+
                                      "  land again.");
                    Console.ReadLine();
                    Console.Clear();
                    GameLogic.enteredForest("Forest ", Player);
                    break;
                    
                //Continue
                default:
                    Console.WriteLine("- A wise choice. You opted for a class change. You have become a magical archer");
                    Player.experience = 0;
                    Player.hp = 20;
                    Player.atkDamage = 7;
                    Player.luck += 2;
                    Player.prestige += 1;
                    Player.heroClass = "Magical Archer";
                    GameLogic.Status(Player);
                    Console.WriteLine("- However, even though grateful, the elves warn you to not set foot in this\n"+
                                      "  land again.");
                    Console.ReadLine();
                    Console.Clear();
                    GameLogic.enteredForest("Forest ", Player);
                    break;
            }
            
        }
    }
    
    //Stat points up
    public static void StatPoint(Hero Player, int numberOfStatPoints)
    {
        Random rd = new Random();
        for (int i = 0; i < numberOfStatPoints; i++)
        {
            string stat;
            int rdNum = rd.Next(0, 3);
            if (rdNum == 0)
            { 
                Player.hp += 1;
                stat = "hp";
            }
            
            else if (rdNum == 1)
            {
                Player.atkDamage += 1;
                stat = "atk";
            }
            else
            {
                Player.luck += 1;
                stat = "luck";
            }
            Console.WriteLine("  Congratulations! You have obtained 1 "+stat);
        }
    }
    
    //Obtain weapon stats

    public static void obtainAWeapon(Hero Player, int index)
    {
        Player.weapons.Add(obtainableWeapons.allWeapons[index]);
        Player.hp += obtainableWeapons.allWeapons[index].hp;
        Player.atkDamage+=obtainableWeapons.allWeapons[index].atk;
        Player.luck+=obtainableWeapons.allWeapons[index].luck;
        Player.prestige+=obtainableWeapons.allWeapons[index].prestige;
    }
    
    //Death

    public static void Death(string deathCause, Hero Player)
    {
        Console.WriteLine("  You were killed by a "+deathCause+"!");
        float totalPoints = Player.gold/10 + 5*Player.hp + 5*Player.level + 2*Player.luck + 10*Player.prestige + 3*Player.atkDamage +
                            4*Player.experience;
        if (Player.hallOfFame)
            totalPoints += 150;
        HighScoreSet(Player, (int)totalPoints);
        GameLogic.initializePlayer(Player);
    }
    
    //VICTORY

    public static void Victory(Hero Player)
    {
        Console.WriteLine("- Congratulations! You have defeated the demon lord which ruled over the world!\n");
        Console.ReadLine();
        Console.Clear();
        int defeatedDemonLord = 1000;
        float totalPoints = defeatedDemonLord+Player.gold/10 + 5*Player.hp + 5*Player.level + 2*Player.luck + 10*Player.prestige + 3*Player.atkDamage +
                          4*Player.experience;
        if (Player.hallOfFame)
            totalPoints += 150;
        HighScoreSet(Player, (int)totalPoints);
        Console.ReadLine();
        Console.Clear();
        GameLogic.initializePlayer(new Hero());
    }
    
    //Set high Scores
    public static void HighScoreSet(Hero Player, int score)
    {
        if (Player.heroClass == "Rogue")
        {
            if (score > highScore.rogueScore)
            {
                Console.WriteLine("              ** Congratulations! **\n" +
                                  "  You have obtained "+score+" points\n"+
                                  "You have obtained a new highscore with the rogue,\n" +
                                  "beating your previous score of "+highScore.rogueScore);
                highScore.rogueScoreName = Player.name;
                highScore.rogueScore = score;
            }
            else
            {
                Console.WriteLine("  You have scored " + score + " points, but have not beaten \n" +
                                  "  the previous "+Player.heroClass+" highscore of " + highScore.rogueScore);
            }
        }
        else if (Player.heroClass == "Archer")
        {
            if (score > highScore.archerScore)
            {
                Console.WriteLine("              ** Congratulations! **\n" +
                                  "  You have obtained "+score+" points\n"+
                                  "You have obtained a new highscore with the archer,\n" +
                                  "beating your previous score of "+highScore.archerScore);
                highScore.archerScore = score;
                highScore.archerScoreName = Player.name;
            }
            else
            {
                Console.WriteLine("  You have scored " + score + " points, but have not beaten \n" +
                                  "  the previous "+Player.heroClass+" highscore of " + highScore.archerScore);
            }
        }
        else if (Player.heroClass == "Mage")
        {
            if (score > highScore.mageScore)
            {
                Console.WriteLine("              ** Congratulations! **\n" +
                                  "  You have obtained "+score+" points\n"+
                                  "You have obtained a new highscore with the mage,\n" +
                                  "beating your previous score of "+highScore.mageScore);
                highScore.mageScore = score;
                highScore.mageScoreName = Player.name;
            }
            else
            {
                Console.WriteLine("  You have scored " + score + " points, but have not beaten \n" +
                                  "  the previous "+Player.heroClass+" highscore of " + highScore.mageScore);
            }
        }
        else if (Player.heroClass == "Warrior")
        {
            if (score > highScore.warriorScore)
            {
                Console.WriteLine("              ** Congratulations! **\n" +
                                  "  You have obtained "+score+" points\n"+
                                  "You have obtained a new highscore with the warrior,\n" +
                                  "beating your previous score of "+highScore.warriorScore);
                highScore.warriorScore = score;
                highScore.warriorScoreName = Player.name;
            }
            else
            {
                Console.WriteLine("  You have scored " + score + " points, but have not beaten \n" +
                                  "  the previous "+Player.heroClass+" highscore of " + highScore.warriorScore);
            }
        }
        else
        {
            if (score > highScore.magicalArcherScore)
            {
                Console.WriteLine("              ** Congratulations! **\n" +
                                  "  You have obtained "+score+" points\n"+
                                  "You have obtained a new highscore with the archer,\n" +
                                  "beating your previous score of "+highScore.magicalArcherScore);
                highScore.magicalArcherScore = score;
                highScore.magicalArcherScoreName = Player.name;
            }
            else
            {
                Console.WriteLine("  You have scored " + score + " points, but have not beaten \n" +
                                  "  the previous Magical Archer highscore of " + highScore.magicalArcherScore);
            }
        }
    }
    



}