using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Factories.FactoryEnums;

namespace Server.Factories
{
    class NameFactory
    {

        public static String getNameOfCreature(CreatureEnum creatureEnum, GenderEnum genderEnum)
        {
            switch (creatureEnum)
            {
                case CreatureEnum.Archer:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Bogenschütze";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Bogenschützin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Captain:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Kapitän";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Kapitän";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Chieftain:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Häuptling";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Weise";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Commandant:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Kommandant";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Kommandantin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Farmer:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Bauer";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Bäuerin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Guard:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Wächter";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Wächterin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Hunter:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Jäger";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Jägerin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Mage:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Magier";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Magierin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Peasant:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Bürger";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Bürgerin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Priest:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Priester";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Priesterin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Slavehunter:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Sklavenjäger";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Sklavenjägerin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Soldier:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Soldat";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Soldatin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Sorcerer:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Beschwörer";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Beschwörerin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Spearman:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Speerwerfer";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Speerwerferin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Warlock:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Hexenmeister";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Hexenmeisterin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Warlord:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Kriegsmeister";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Kriegsmeisterin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                case CreatureEnum.Wizard:
                    {
                        switch (genderEnum)
                        {
                            case GenderEnum.Male:
                                {
                                    return "Zauberer";
                                }
                            case GenderEnum.Female:
                                {
                                    return "Zauberin";
                                }
                            default:
                                {
                                    return "Unbekannt";
                                }
                        }
                    }
                default:
                    {
                        return "Unbekannt";
                    }
            }
        }
    }
}
