using System;

namespace GameSelector.Views
{
    internal interface IUserViewAdapter
    {
        event EventHandler Ready;

        void Start(Action<object> onClose);

        void ShowGameImmediate(GameDataView game, GroupDataView group);

        void ShowGame(GameDataView game, GroupDataView group);

        void ShowAlreadyPlaying(GameDataView game);

        void ShowNoGamesLeft();

        void ShowPaused();

        void ShowReady();

        void ShowReadyAfter(TimeSpan delay);

        void SetGameCodes(string[] names);
    }
}
