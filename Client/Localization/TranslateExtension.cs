namespace Client.Localization
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    /// <summary>
    /// The Translate Markup extension returns a binding to a TranslationData
    /// that provides a translated resource of the specified key
    /// </summary>
    [MarkupExtensionReturnType(typeof(object))]
    //[Localizability(LocalizationCategory.Text)]
    public class TranslateExtension : MarkupExtension
    {
        private readonly static DependencyObject DependencyObject = new DependencyObject();
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateExtension"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public TranslateExtension(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (DesignerProperties.GetIsInDesignMode(DependencyObject))
            {
                if (TranslationManager.Instance.TranslationProvider == null)
                    throw new Exception("TranslationManager.Instance.TranslationProvider==null");
                if (TranslationManager.Instance.TranslationProvider.Languages.Any())
                {
                    var missing = TranslationManager.Instance.Languages
                                                                .Where(x => !TranslationManager.Instance.HasKey(key,x))
                                                                .ToArray();
                    if (missing.Any())
                    {
                        var ls = string.Join(", ", missing.Select(x=>x.TwoLetterISOLanguageName));
                        throw new Exception(string.Format("Translation for key {0} is missing in {{{1}}}", key, ls));
                    }
                }

            }
            Key = key;
        }

        [ConstructorArgument("key")]
        public string Key { get; set; }

        /// <summary>
        /// See <see cref="MarkupExtension.ProvideValue" />
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding("Value")
            {
                Source = new TranslationData(Key)
            };
            return binding.ProvideValue(serviceProvider);
        }
    }
}
