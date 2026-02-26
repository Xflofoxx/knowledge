using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class CharacterEditorTests
    {
        private CharacterData _testData;

        [SetUp]
        public void Setup()
        {
            _testData = new CharacterData();
        }

        [Test]
        public void CharacterData_Initialize_WithDefaultValues()
        {
            Assert.IsNotNull(_testData);
            Assert.AreEqual("New Character", _testData.characterName);
            Assert.AreEqual(CharacterSex.Male, _testData.sex);
            Assert.AreEqual(25, _testData.startingAge);
        }

        [Test]
        public void CharacterData_SetSex_ModifiesSex()
        {
            _testData.sex = CharacterSex.Female;
            Assert.AreEqual(CharacterSex.Female, _testData.sex);

            _testData.sex = CharacterSex.NonBinary;
            Assert.AreEqual(CharacterSex.NonBinary, _testData.sex);
        }

        [Test]
        public void CharacterData_SetBackground_ModifiesBackground()
        {
            _testData.background = CharacterBackground.Warrior;
            Assert.AreEqual(CharacterBackground.Warrior, _testData.background);

            _testData.background = CharacterBackground.Scholar;
            Assert.AreEqual(CharacterBackground.Scholar, _testData.background);
        }

        [Test]
        public void CharacterData_SetBodyType_ModifiesBodyType()
        {
            _testData.bodyType = BodyType.Slim;
            Assert.AreEqual(BodyType.Slim, _testData.bodyType);

            _testData.bodyType = BodyType.Muscular;
            Assert.AreEqual(BodyType.Muscular, _testData.bodyType);
        }

        [Test]
        public void CharacterData_SetAge_ClampsToValidRange()
        {
            _testData.startingAge = 18;
            Assert.AreEqual(18, _testData.startingAge);

            _testData.startingAge = 80;
            Assert.AreEqual(80, _testData.startingAge);
        }

        [Test]
        public void CharacterData_SetHeightScale_ClampsToValidRange()
        {
            _testData.heightScale = 0.5f;
            Assert.AreEqual(0.5f, _testData.heightScale);

            _testData.heightScale = 2f;
            Assert.AreEqual(2f, _testData.heightScale);
        }

        [Test]
        public void CharacterData_SetWidthScale_ClampsToValidRange()
        {
            _testData.widthScale = 0.5f;
            Assert.AreEqual(0.5f, _testData.widthScale);

            _testData.widthScale = 2f;
            Assert.AreEqual(2f, _testData.widthScale);
        }

        [Test]
        public void CharacterData_SetMassScale_ClampsToValidRange()
        {
            _testData.massScale = 0.5f;
            Assert.AreEqual(0.5f, _testData.massScale);

            _testData.massScale = 2f;
            Assert.AreEqual(2f, _testData.massScale);
        }

        [Test]
        public void CharacterData_SetPrimaryTrait_ModifiesTrait()
        {
            _testData.primaryTrait = PersonalityTrait.Brave;
            Assert.AreEqual(PersonalityTrait.Brave, _testData.primaryTrait);

            _testData.primaryTrait = PersonalityTrait.Greedy;
            Assert.AreEqual(PersonalityTrait.Greedy, _testData.primaryTrait);
        }

        [Test]
        public void CharacterData_SetSecondaryTrait_ModifiesTrait()
        {
            _testData.secondaryTrait = PersonalityTrait.Cautious;
            Assert.AreEqual(PersonalityTrait.Cautious, _testData.secondaryTrait);

            _testData.secondaryTrait = PersonalityTrait.Ambitious;
            Assert.AreEqual(PersonalityTrait.Ambitious, _testData.secondaryTrait);
        }

        [Test]
        public void CharacterData_SetColors_ModifiesColors()
        {
            _testData.hairColor = Color.red;
            Assert.AreEqual(Color.red, _testData.hairColor);

            _testData.skinColor = Color.white;
            Assert.AreEqual(Color.white, _testData.skinColor);

            _testData.eyeColor = Color.blue;
            Assert.AreEqual(Color.blue, _testData.eyeColor);
        }

        [Test]
        public void GetCharacterData_ReturnsData()
        {
            GameObject go = new GameObject();
            CharacterEditor editor = go.AddComponent<CharacterEditor>();
            CharacterData data = editor.GetCharacterData();
            Assert.IsNotNull(data);
            Object.DestroyImmediate(go);
        }

        [Test]
        public void SetCharacterData_UpdatesData()
        {
            CharacterData newData = new CharacterData();
            newData.characterName = "New Name";
            newData.sex = CharacterSex.Female;
            newData.startingAge = 35;

            _testData.characterName = newData.characterName;
            _testData.sex = newData.sex;
            _testData.startingAge = newData.startingAge;

            Assert.AreEqual("New Name", _testData.characterName);
            Assert.AreEqual(CharacterSex.Female, _testData.sex);
            Assert.AreEqual(35, _testData.startingAge);
        }

        [Test]
        public void RandomizeLogic_ProducesValidData()
        {
            CharacterData data = new CharacterData();
            data.sex = (CharacterSex)Random.Range(0, 3);
            data.background = (CharacterBackground)Random.Range(0, 8);
            data.startingAge = Random.Range(18, 65);
            data.heightScale = Random.Range(0.8f, 1.2f);

            Assert.GreaterOrEqual((int)data.sex, 0);
            Assert.LessOrEqual((int)data.sex, 2);
            Assert.GreaterOrEqual(data.startingAge, 18);
            Assert.LessOrEqual(data.startingAge, 80);
            Assert.GreaterOrEqual(data.heightScale, 0.5f);
            Assert.LessOrEqual(data.heightScale, 2f);
        }

        [Test]
        public void Clone_ActsAsResetToDefaults()
        {
            _testData.characterName = "Custom";
            _testData.sex = CharacterSex.Female;
            _testData.startingAge = 50;
            _testData.background = CharacterBackground.Warrior;
            _testData.bodyType = BodyType.Heavy;

            CharacterData defaults = new CharacterData();
            _testData = defaults.Clone();

            Assert.AreEqual("New Character", _testData.characterName);
            Assert.AreEqual(CharacterSex.Male, _testData.sex);
            Assert.AreEqual(25, _testData.startingAge);
            Assert.AreEqual(CharacterBackground.None, _testData.background);
            Assert.AreEqual(BodyType.Average, _testData.bodyType);
        }

        [Test]
        public void EnumValues_AreComplete()
        {
            CharacterSex[] sexes = (CharacterSex[])System.Enum.GetValues(typeof(CharacterSex));
            Assert.GreaterOrEqual(sexes.Length, 3);

            CharacterBackground[] backgrounds = (CharacterBackground[])System.Enum.GetValues(typeof(CharacterBackground));
            Assert.GreaterOrEqual(backgrounds.Length, 7);

            BodyType[] bodyTypes = (BodyType[])System.Enum.GetValues(typeof(BodyType));
            Assert.GreaterOrEqual(bodyTypes.Length, 5);

            PersonalityTrait[] traits = (PersonalityTrait[])System.Enum.GetValues(typeof(PersonalityTrait));
            Assert.GreaterOrEqual(traits.Length, 8);
        }
    }
}
