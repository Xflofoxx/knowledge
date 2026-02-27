using NUnit.Framework;
using UnityEngine;
using System;

namespace Knowledge.Tests.Core
{
    [TestFixture]
    public class EventSystemTests
    {
        private EventSystem eventSystem;
        private int eventReceivedCount;
        private object receivedData;

        [SetUp]
        public void Setup()
        {
            var gameObject = new GameObject("EventSystem");
            eventSystem = gameObject.AddComponent<EventSystem>();
            eventReceivedCount = 0;
            receivedData = null;
        }

        [TearDown]
        public void Teardown()
        {
            eventSystem.ClearAllEvents();
            Object.DestroyImmediate(eventSystem.gameObject);
        }

        [Test]
        public void Subscribe_ValidEvent_AddsListener()
        {
            eventSystem.Subscribe("TestEvent", OnTestEvent);
            eventSystem.Publish("TestEvent");
            Assert.AreEqual(1, eventReceivedCount);
        }

        [Test]
        public void Unsubscribe_RemovesListener()
        {
            eventSystem.Subscribe("TestEvent", OnTestEvent);
            eventSystem.Unsubscribe("TestEvent", OnTestEvent);
            eventSystem.Publish("TestEvent");
            Assert.AreEqual(0, eventReceivedCount);
        }

        [Test]
        public void Publish_WithData_PassesDataToListener()
        {
            eventSystem.Subscribe("TestEvent", OnTestEventWithData);
            eventSystem.Publish("TestEvent", "TestData");
            Assert.AreEqual("TestData", receivedData);
        }

        [Test]
        public void MultipleSubscribers_AllReceiveEvent()
        {
            int count1 = 0, count2 = 0;
            eventSystem.Subscribe("TestEvent", () => count1++);
            eventSystem.Subscribe("TestEvent", () => count2++);
            eventSystem.Publish("TestEvent");
            Assert.AreEqual(1, count1);
            Assert.AreEqual(1, count2);
        }

        [Test]
        public void Publish_NoSubscribers_NoError()
        {
            Assert.DoesNotThrow(() => eventSystem.Publish("NonExistentEvent"));
        }

        [Test]
        public void ClearAllEvents_RemovesAllListeners()
        {
            eventSystem.Subscribe("Event1", OnTestEvent);
            eventSystem.Subscribe("Event2", OnTestEvent);
            eventSystem.ClearAllEvents();
            eventSystem.Publish("Event1");
            eventSystem.Publish("Event2");
            Assert.AreEqual(0, eventReceivedCount);
        }

        private void OnTestEvent()
        {
            eventReceivedCount++;
        }

        private void OnTestEventWithData(object data)
        {
            receivedData = data;
        }
    }
}
