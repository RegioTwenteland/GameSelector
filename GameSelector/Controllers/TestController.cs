using GameSelector.Views;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSelector.Controllers
{
    internal class TestController : AbstractController
    {
        private TestViewAdapter _testView;
        private UserInputView _userInputView;

        public TestController(BlockingCollection<Tuple<string, object>> messages, TestViewAdapter testView, UserInputView userInputView)
            : base(messages)
        {
            _testView = testView;
            _userInputView = userInputView;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "RequestData", OnRequestData },
                { "SendData", OnSendData }
            });
        }

        new public void Run()
        {
            _testView.Start(Stop);

            base.Run();
        }

        private void OnRequestData(object value)
        {
            var data = _userInputView.GetData();
            _testView.ShowData(data);
        }

        private void OnSendData(object value)
        {
            Debug.Assert(value is string);
            _userInputView.SetData((string)value);
        }
    }
}
