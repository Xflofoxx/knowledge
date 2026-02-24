using UnityEngine;

namespace Knowledge.Game
{
    public enum CharacterSex
    {
        Male,
        Female,
        NonBinary
    }

    public enum CharacterBackground
    {
        None,
        Warrior,
        Scholar,
        Merchant,
        Farmer,
        Hunter,
        Noble,
        Outcast
    }

    public enum BodyType
    {
        Slim,
        Average,
        Athletic,
        Heavy,
        Muscular
    }

    public enum PersonalityTrait
    {
        Brave,
        Cautious,
        Curious,
        Greedy,
        Kind,
        Aggressive,
        Peaceful,
        Ambitious
    }

    [System.Serializable]
    public class CharacterData
    {
        [Header("Basic Info")]
        public string characterName = "New Character";
        public CharacterSex sex = CharacterSex.Male;
        public CharacterBackground background = CharacterBackground.None;
        
        [Header("Age")]
        [Range(18, 80)] public int startingAge = 25;
        
        [Header("Body")]
        public BodyType bodyType = BodyType.Average;
        [Range(0.5f, 2f)] public float heightScale = 1f;
        [Range(0.5f, 2f)] public float widthScale = 1f;
        [Range(0.5f, 2f)] public float massScale = 1f;
        
        [Header("Personality")]
        public PersonalityTrait primaryTrait = PersonalityTrait.Curious;
        public PersonalityTrait secondaryTrait = PersonalityTrait.Brave;
        
        [Header("Appearance")]
        public Color hairColor = new Color(0.3f, 0.2f, 0.1f);
        public Color skinColor = new Color(0.9f, 0.8f, 0.7f);
        public Color eyeColor = new Color(0.2f, 0.4f, 0.8f);

        public float GetDisplayHeight()
        {
            float baseHeight = sex == CharacterSex.Male ? 1.75f : 1.60f;
            float bodyModifier = GetBodyTypeHeightModifier();
            return (baseHeight * heightScale) + bodyModifier;
        }

        public float GetDisplayWidth()
        {
            float baseWidth = sex == CharacterSex.Male ? 0.5f : 0.45f;
            float bodyModifier = GetBodyTypeWidthModifier();
            return (baseWidth * widthScale) + bodyModifier;
        }

        public float GetDisplayMass()
        {
            float baseMass = sex == CharacterSex.Male ? 70f : 60f;
            float bodyModifier = GetBodyTypeMassModifier();
            return (baseMass * massScale) + bodyModifier;
        }

        private float GetBodyTypeHeightModifier()
        {
            return bodyType switch
            {
                BodyType.Slim => -0.05f,
                BodyType.Average => 0f,
                BodyType.Athletic => 0.03f,
                BodyType.Heavy => 0.02f,
                BodyType.Muscular => 0.05f,
                _ => 0f
            };
        }

        private float GetBodyTypeWidthModifier()
        {
            return bodyType switch
            {
                BodyType.Slim => -0.08f,
                BodyType.Average => 0f,
                BodyType.Athletic => 0.03f,
                BodyType.Heavy => 0.15f,
                BodyType.Muscular => 0.08f,
                _ => 0f
            };
        }

        private float GetBodyTypeMassModifier()
        {
            return bodyType switch
            {
                BodyType.Slim => -10f,
                BodyType.Average => 0f,
                BodyType.Athletic => 5f,
                BodyType.Heavy => 25f,
                BodyType.Muscular => 15f,
                _ => 0f
            };
        }

        public float GetStartingStatBonus()
        {
            return background switch
            {
                CharacterBackground.Warrior => 1.2f,
                CharacterBackground.Scholar => 1.1f,
                CharacterBackground.Merchant => 1.0f,
                CharacterBackground.Farmer => 1.1f,
                CharacterBackground.Hunter => 1.15f,
                CharacterBackground.Noble => 1.05f,
                CharacterBackground.Outcast => 0.9f,
                _ => 1.0f
            };
        }

        public CharacterData Clone()
        {
            return new CharacterData
            {
                characterName = characterName,
                sex = sex,
                background = background,
                startingAge = startingAge,
                bodyType = bodyType,
                heightScale = heightScale,
                widthScale = widthScale,
                massScale = massScale,
                primaryTrait = primaryTrait,
                secondaryTrait = secondaryTrait,
                hairColor = hairColor,
                skinColor = skinColor,
                eyeColor = eyeColor
            };
        }
    }
}
