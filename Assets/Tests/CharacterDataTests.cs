using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class CharacterDataTests
    {
        private CharacterData _characterData;

        [SetUp]
        public void Setup()
        {
            _characterData = new CharacterData();
        }

        [Test]
        public void DefaultValues_AreCorrect()
        {
            Assert.AreEqual("New Character", _characterData.characterName);
            Assert.AreEqual(CharacterSex.Male, _characterData.sex);
            Assert.AreEqual(CharacterBackground.None, _characterData.background);
            Assert.AreEqual(25, _characterData.startingAge);
            Assert.AreEqual(BodyType.Average, _characterData.bodyType);
            Assert.AreEqual(1f, _characterData.heightScale);
            Assert.AreEqual(1f, _characterData.widthScale);
            Assert.AreEqual(1f, _characterData.massScale);
        }

        [Test]
        public void GetDisplayHeight_Male_ReturnsCorrectHeight()
        {
            _characterData.sex = CharacterSex.Male;
            _characterData.heightScale = 1f;
            _characterData.bodyType = BodyType.Average;

            float height = _characterData.GetDisplayHeight();

            Assert.AreEqual(1.75f, height, 0.01f);
        }

        [Test]
        public void GetDisplayHeight_Female_ReturnsCorrectHeight()
        {
            _characterData.sex = CharacterSex.Female;
            _characterData.heightScale = 1f;
            _characterData.bodyType = BodyType.Average;

            float height = _characterData.GetDisplayHeight();

            Assert.AreEqual(1.60f, height, 0.01f);
        }

        [Test]
        public void GetDisplayHeight_WithScale_AppliesScale()
        {
            _characterData.sex = CharacterSex.Male;
            _characterData.heightScale = 1.5f;
            _characterData.bodyType = BodyType.Average;

            float height = _characterData.GetDisplayHeight();

            Assert.AreEqual(2.625f, height, 0.01f);
        }

        [Test]
        public void GetDisplayHeight_DifferentBodyTypes_AppliesModifiers()
        {
            _characterData.sex = CharacterSex.Male;
            _characterData.heightScale = 1f;

            _characterData.bodyType = BodyType.Slim;
            float slimHeight = _characterData.GetDisplayHeight();

            _characterData.bodyType = BodyType.Muscular;
            float muscularHeight = _characterData.GetDisplayHeight();

            Assert.Less(slimHeight, muscularHeight);
        }

        [Test]
        public void GetDisplayWidth_Male_ReturnsCorrectWidth()
        {
            _characterData.sex = CharacterSex.Male;
            _characterData.widthScale = 1f;
            _characterData.bodyType = BodyType.Average;

            float width = _characterData.GetDisplayWidth();

            Assert.AreEqual(0.5f, width, 0.01f);
        }

        [Test]
        public void GetDisplayWidth_Female_ReturnsCorrectWidth()
        {
            _characterData.sex = CharacterSex.Female;
            _characterData.widthScale = 1f;
            _characterData.bodyType = BodyType.Average;

            float width = _characterData.GetDisplayWidth();

            Assert.AreEqual(0.45f, width, 0.01f);
        }

        [Test]
        public void GetDisplayMass_Male_ReturnsCorrectMass()
        {
            _characterData.sex = CharacterSex.Male;
            _characterData.massScale = 1f;
            _characterData.bodyType = BodyType.Average;

            float mass = _characterData.GetDisplayMass();

            Assert.AreEqual(70f, mass, 0.1f);
        }

        [Test]
        public void GetDisplayMass_Female_ReturnsCorrectMass()
        {
            _characterData.sex = CharacterSex.Female;
            _characterData.massScale = 1f;
            _characterData.bodyType = BodyType.Average;

            float mass = _characterData.GetDisplayMass();

            Assert.AreEqual(60f, mass, 0.1f);
        }

        [Test]
        public void GetDisplayMass_HeavyBodyType_IncreasesMass()
        {
            _characterData.sex = CharacterSex.Male;
            _characterData.massScale = 1f;
            _characterData.bodyType = BodyType.Heavy;

            float mass = _characterData.GetDisplayMass();

            Assert.Greater(mass, 70f);
        }

        [Test]
        public void GetDisplayMass_SlimBodyType_DecreasesMass()
        {
            _characterData.sex = CharacterSex.Male;
            _characterData.massScale = 1f;
            _characterData.bodyType = BodyType.Slim;

            float mass = _characterData.GetDisplayMass();

            Assert.Less(mass, 70f);
        }

        [Test]
        public void GetStartingStatBonus_Warrior_Returns20Percent()
        {
            _characterData.background = CharacterBackground.Warrior;

            float bonus = _characterData.GetStartingStatBonus();

            Assert.AreEqual(1.2f, bonus, 0.01f);
        }

        [Test]
        public void GetStartingStatBonus_Scholar_Returns10Percent()
        {
            _characterData.background = CharacterBackground.Scholar;

            float bonus = _characterData.GetStartingStatBonus();

            Assert.AreEqual(1.1f, bonus, 0.01f);
        }

        [Test]
        public void GetStartingStatBonus_Merchant_ReturnsNormal()
        {
            _characterData.background = CharacterBackground.Merchant;

            float bonus = _characterData.GetStartingStatBonus();

            Assert.AreEqual(1.0f, bonus, 0.01f);
        }

        [Test]
        public void GetStartingStatBonus_Outcast_ReturnsNegative()
        {
            _characterData.background = CharacterBackground.Outcast;

            float bonus = _characterData.GetStartingStatBonus();

            Assert.AreEqual(0.9f, bonus, 0.01f);
        }

        [Test]
        public void GetStartingStatBonus_None_ReturnsNormal()
        {
            _characterData.background = CharacterBackground.None;

            float bonus = _characterData.GetStartingStatBonus();

            Assert.AreEqual(1.0f, bonus, 0.01f);
        }

        [Test]
        public void Clone_CreatesSeparateInstance()
        {
            _characterData.characterName = "Test";
            _characterData.sex = CharacterSex.Female;
            _characterData.startingAge = 30;
            _characterData.background = CharacterBackground.Hunter;

            CharacterData cloned = _characterData.Clone();

            Assert.AreNotSame(_characterData, cloned);
            Assert.AreEqual(_characterData.characterName, cloned.characterName);
            Assert.AreEqual(_characterData.sex, cloned.sex);
            Assert.AreEqual(_characterData.startingAge, cloned.startingAge);
            Assert.AreEqual(_characterData.background, cloned.background);
        }

        [Test]
        public void Clone_ModifyingCloneDoesNotAffectOriginal()
        {
            _characterData.characterName = "Original";
            CharacterData cloned = _characterData.Clone();
            cloned.characterName = "Modified";

            Assert.AreNotEqual(_characterData.characterName, cloned.characterName);
        }

        [Test]
        public void StartingAge_DefaultIs25()
        {
            Assert.AreEqual(25, _characterData.startingAge);
        }

        [Test]
        public void HeightScale_DefaultIs1()
        {
            Assert.AreEqual(1f, _characterData.heightScale);
        }

        [Test]
        public void WidthScale_DefaultIs1()
        {
            Assert.AreEqual(1f, _characterData.widthScale);
        }

        [Test]
        public void MassScale_DefaultIs1()
        {
            Assert.AreEqual(1f, _characterData.massScale);
        }

        [Test]
        public void PrimaryTrait_DefaultIsCurious()
        {
            Assert.AreEqual(PersonalityTrait.Curious, _characterData.primaryTrait);
        }

        [Test]
        public void SecondaryTrait_DefaultIsBrave()
        {
            Assert.AreEqual(PersonalityTrait.Brave, _characterData.secondaryTrait);
        }

        [Test]
        public void ColorFields_HaveDefaultValues()
        {
            Assert.IsNotNull(_characterData.hairColor);
            Assert.IsNotNull(_characterData.skinColor);
            Assert.IsNotNull(_characterData.eyeColor);
        }
    }
}
