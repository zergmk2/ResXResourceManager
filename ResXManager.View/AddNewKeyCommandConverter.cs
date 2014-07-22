﻿namespace tomenglertde.ResXManager.View
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using tomenglertde.ResXManager.Model;

    public class AddNewKeyCommandConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var resourceManager = value as ResourceManager;

            return resourceManager == null ? NullCommand.Default : new AddNewKeyCommand(resourceManager);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private class AddNewKeyCommand : DelegateCommand
        {
            private readonly ResourceManager _resourceManager;

            public AddNewKeyCommand(ResourceManager resourceManager)
            {
                Contract.Requires(resourceManager != null);

                _resourceManager = resourceManager;

                CanExecuteCallback = CanExecute;
                ExecuteCallback = Execute;
            }

            private bool CanExecute()
            {
                return _resourceManager.SelectedEntities.Count == 1;
            }

            private void Execute()
            {
                if (_resourceManager.SelectedEntities.Count != 1)
                    return;

                var resourceFile = _resourceManager.SelectedEntities.Single();

                if (!resourceFile.CanEdit(null))
                    return;

                var inputBox = new InputBox
                {
                    Title = Properties.Resources.Title,
                    Prompt = Properties.Resources.NewKeyPrompt,
                    Owner = Window.GetWindow(ResourceView.Instance),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                inputBox.TextChanged += (_, args) =>
                    inputBox.IsInputValid = !resourceFile.Entries.Any(entry => entry.Key.Equals(args.Text, StringComparison.OrdinalIgnoreCase));

                if (inputBox.ShowDialog() != true)
                    return;

                try
                {
                    _resourceManager.AddNewKey(resourceFile, inputBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), Properties.Resources.Title);
                }
            }

            [ContractInvariantMethod]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
            private void ObjectInvariant()
            {
                Contract.Invariant(_resourceManager != null);
            }
        }
    }
}