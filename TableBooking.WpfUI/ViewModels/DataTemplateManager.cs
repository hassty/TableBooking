using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace WpfUI.ViewModels
{
    public class DataTemplateManager
    {
        private DataTemplate CreateTemplate(Type viewModelType, Type viewType)
        {
            const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type viewmodels:{0}}}\"><views:{1} /></DataTemplate>";
            var xaml = String.Format(xamlTemplate, viewModelType.Name, viewType.Name, viewModelType.Namespace, viewType.Namespace);

            var context = new ParserContext();

            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            context.XamlTypeMapper.AddMappingProcessingInstruction("viewmodels", viewModelType.Namespace, viewModelType.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction("views", viewType.Namespace, viewType.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("viewmodels", "viewmodels");
            context.XmlnsDictionary.Add("views", "views");

            return (DataTemplate)XamlReader.Parse(xaml, context);
        }

        public void RegisterDataTemplate<TViewModel, TView>()
                    where TViewModel : ViewModelBase where TView : FrameworkElement
        {
            RegisterDataTemplate(typeof(TViewModel), typeof(TView));
        }

        public void RegisterDataTemplate(Type viewModelType, Type viewType)
        {
            var template = CreateTemplate(viewModelType, viewType);
            var key = template.DataTemplateKey;
            Application.Current.Resources.Add(key, template);
        }
    }
}