using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EF
{
    /// <summary>
    /// EF的信息提示
    /// </summary>
    public  static class EFMessageBox
    {
        const string DefaultCaption = "";
        const IWin32Window DefaultOwner = null;
        const MessageBoxButtons DefaultButtons = MessageBoxButtons.OK;
        const MessageBoxIcon DefaultIcon = MessageBoxIcon.None;
        const MessageBoxDefaultButton DefaultDefButton = MessageBoxDefaultButton.Button1;
        public static DialogResult Show(string text) { return Show(DefaultOwner, text, DefaultCaption, DefaultButtons, DefaultIcon, DefaultDefButton); }
        public static DialogResult Show(IWin32Window owner, string text) { return Show(owner, text, DefaultCaption, DefaultButtons, DefaultIcon, DefaultDefButton); }
        public static DialogResult Show(string text, string caption) { return Show(DefaultOwner, text, caption, DefaultButtons, DefaultIcon, DefaultDefButton); }
        public static DialogResult Show(IWin32Window owner, string text, string caption) { return Show(owner, text, caption, DefaultButtons, DefaultIcon, DefaultDefButton); }
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons) { return Show(DefaultOwner, text, caption, buttons, DefaultIcon, DefaultDefButton); }
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons) { return Show(owner, text, caption, buttons, DefaultIcon, DefaultDefButton); }
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) { return Show(DefaultOwner, text, caption, buttons, icon, DefaultDefButton); }
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) { return Show(owner, text, caption, buttons, icon, DefaultDefButton); }
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton) { return Show(DefaultOwner, text, caption, buttons, icon, defaultButton); }
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(owner, text, caption, buttons,icon,defaultButton);
        }
        //带皮肤的
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, string text) { return Show(lookAndFeel, DefaultOwner, text, DefaultCaption, DefaultButtons, DefaultIcon, DefaultDefButton); }
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, IWin32Window owner, string text) { return Show(lookAndFeel, owner, text, DefaultCaption, DefaultButtons, DefaultIcon, DefaultDefButton); }
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, string text, string caption) { return Show(lookAndFeel, DefaultOwner, text, caption, DefaultButtons, DefaultIcon, DefaultDefButton); }
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, IWin32Window owner, string text, string caption) { return Show(lookAndFeel, owner, text, caption, DefaultButtons, DefaultIcon, DefaultDefButton); }
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, string text, string caption, MessageBoxButtons buttons) { return Show(lookAndFeel, DefaultOwner, text, caption, buttons, DefaultIcon, DefaultDefButton); }
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, IWin32Window owner, string text, string caption, MessageBoxButtons buttons) { return Show(lookAndFeel, owner, text, caption, buttons, DefaultIcon, DefaultDefButton); }
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) { return Show(lookAndFeel, DefaultOwner, text, caption, buttons, icon, DefaultDefButton); }
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) { return Show(lookAndFeel, owner, text, caption, buttons, icon, DefaultDefButton); }
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton) { return Show(lookAndFeel, DefaultOwner, text, caption, buttons, icon, defaultButton); }
        //public static DialogResult Show(UserLookAndFeel lookAndFeel, IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        //{
        //    return DevExpress.XtraEditors.XtraMessageBox.Show(lookAndFeel, owner, text, caption,buttons, icon, defaultButton);
        //}
    }
}
