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
            NfcDataBridge nfcDataBride
        )
            : base(messages)
        {
            _testView = new TestViewAdapter(messages);
            _userInputView = new UserInputView(messages);
            _nfcDataBridge = nfcDataBride;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "RequestData", OnRequestData },
                { "WriteCardData", OnWriteCardData },
                { "CardInserted", OnCardInserted },
                { "CardEjected", OnCardEjected },
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
            _testView.ShowData(cardData);
        }

        private void OnWriteCardData(object value)
        {
            Debug.Assert(value is CardData);

            _nfcDataBridge.CardData = (CardData)value;
        }

        private void OnCardInserted(object value)
        {
            var cardData = _nfcDataBridge.CardData;
            _testView.ShowData(cardData);
        }

        private void OnCardEjected(object value)
        {
            _testView.ShowData(null);
        }
    }
}
