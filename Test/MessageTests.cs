using WashLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Test
{
    [TestClass]
    public class MessageTests
    {
        [DataContract]
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        private class MyCommandData
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        {
            [DataMember]
            public string SomeString { get; set; }

            [DataMember]
            public DateTime SomeDateTime { get; set; }

            public override bool Equals(object obj)
            {
                Debug.Assert(obj != null && obj.GetType() == typeof(MyCommandData));

                var other = obj as MyCommandData;
                return SomeString.Equals(other.SomeString) && SomeDateTime.Equals(other.SomeDateTime);
            }
        }

        [TestMethod]
        public void TestMessageSerializion()
        {
            var controllerId1 = new ControllerId("sender");
            var controllerId2 = new ControllerId("recipient");

            var testString = "Hello world";
            var testDateTime = new DateTime(1870, 10, 6);

            var testData = new MyCommandData
            {
                SomeString = testString,
                SomeDateTime = testDateTime,
            };


            var message = new Message(controllerId1, controllerId2, "MyCommand", testData);

            Assert.IsTrue(message.Data.Equals(testData));
        }
    }
}
