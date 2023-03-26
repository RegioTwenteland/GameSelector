using GameSelector.Model;
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
        private NfcDataBridge _nfcDataBridge;

        public TestController(
            BlockingCollection<Tuple<string, object>> messages,
            TestViewAdapter testView,
            UserInputView userInputView,
            NfcDataBridge nfcDataBride
        )
            : base(messages)
        {
            _testView = testView;
            _userInputView = userInputView;
            _nfcDataBridge = nfcDataBride;

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
            var cardData = _nfcDataBridge.CardData;
            _testView.ShowData(cardData.ToString());
        }

        private void OnSendData(object value)
        {
            Debug.Assert(value is DateTime);

            var cardData = _nfcDataBridge.CardData;
            cardData.StartTime = (DateTime)value;

            _nfcDataBridge.CardData = cardData;
        }
    }
}
