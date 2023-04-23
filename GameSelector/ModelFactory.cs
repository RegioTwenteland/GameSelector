using GameSelector.Model;
using GameSelector.Model.Database;
using System;

namespace GameSelector
{
    internal static class ModelFactory
    {
        private static IModel _model = null;

        public static IModel GetModel()
        {
            if (_model == null)
            {
                switch (GlobalSettings.ModelType)
                {
                    case ModelType.Database:
                        _model = new DbModel();
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return _model;
        }
    }
}
