using System;
namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    interface IMotherForm
    {
        void AddEntity(FISCA.Presentation.INCPanel newEntity);
        void AddEntity(FISCA.Presentation.IBlankPanel newEntity);
        FISCA.Presentation.IPreferenceProvider PreferenceProvider { get; set; }
        event EventHandler PreferenceProviderChanged;
        FISCA.Presentation.RibbonBarItemManager RibbonBarItems { get; }
        void SetStatusBarMessage(string labelMessage, int progress);
        void SetStatusBarMessage(string labelMessage);
        FISCA.Presentation.MenuButton StartMenu { get; }
    }
}
