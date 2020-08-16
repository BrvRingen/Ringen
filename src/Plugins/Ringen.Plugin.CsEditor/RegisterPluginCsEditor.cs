using Ringen.Core.PluginSystem;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Core.Messaging;
using Ringen.Core.UI;
using Ringen.Core.TranslationManager;
using System.Reflection;

namespace Ringen.Plugin.CsEditor
{
    public sealed class RegisterPlugin : PluggableBase
    {
        #region properties
        public static Type Type { get; private set; }
        public override string Name { get; } = "Editor";
        public override string StartPageKey { get; } = nameof(View);

        public override bool CanLoad => true;
        #endregion properties

        #region constructors
        public RegisterPlugin()
        {
            Type = GetType();
            TransManager.Instance.AddTranslationResource(ResourcesEnum.Ringen_Plugin_CsEditor_DictPluginMain, "Ringen.Plugin.CsEditor.Resources.LanguageFiles.DictPluginMain", Assembly.GetExecutingAssembly());
        }
        #endregion constructors

        #region methods
        public override void OnRegister()
        {

        }

        public override void OnHostLoaded()
        {
            Messenger.Default.Register(this, (OpenWithDoubleClickMessage obj) => {
                obj.Callback((RingenTabItem: View, Open: true));
            });
        }

        private IRingenTabItem view;

        public IRingenTabItem View
        {
            get {
                if (view == null)
                {
                    view = new View();
                }
                
                return view;
            }
        }




        #endregion methods
    }
}

