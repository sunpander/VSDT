using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using auth.UI;

namespace auth
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public interface IMdiChild
        {
            DevExpress.XtraBars.Bar childToolBar {get;}
            void QuickSearch(string str);
        }
        public frmMain()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try{
            FormResources user = new FormResources();
            user.Show();
               }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }   
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                string formName = "EPES2";
                int userID = 6;
                //先根据画面名称和用户名,查询画面,按钮注册信息
                DataTable dt = Services.DbUserRes.QueryAccess(userID, formName, Services.CConstString.ConnectName);
               
                //Server.DbUserRes.QueryFormAuth(
                //表列为 name ,                 description,     type,                         num
                //-----名称(画面或者按钮名)        描述           类型 (FORM 或者BUTTON)     可访问数量(0以上为可访问)
                //为空,则没有相关注册信息
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("没有该画面的注册信息.");
                    return;
                }
                //遍历找出,可访问的画面,以及不可访问的按钮列表
                List<string> formEnable = new List<string>();
                List<string> buttonUnable = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string type = dt.Rows[i]["type"].ToString();
                    string name = dt.Rows[i]["name"].ToString();
                    string descript = dt.Rows[i]["description"].ToString();
                    int num = Convert.ToInt32(dt.Rows[i]["num"]);
                    if (type == "FORM" && num > 0)
                    {
                        formEnable.Add(name);
                    }
                    if (type == "BUTTON" && num == 0)
                    {
                        buttonUnable.Add(name);
                    }
                }
                //如果画面没有权限,则提示没权限并 退出
                if (formEnable.Count == 0)
                {
                    MessageBox.Show("没有权限访问该画面.");
                }
                //如果画面有权限,则构造...
                FormUsers user = new FormUsers();
                //只对注册的按钮,进行权限判断
                //如果有权限,则不作任何操作,如果没权限又存在,则把(Enable=false 或者 visible=false)
 
                //1。 先遍历工具条
                int count = user.ChildBar.Manager.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    if (buttonUnable.Contains(user.ChildBar.Manager.Items[i].Name))
                    {
                        user.ChildBar.Manager.Items[i].Enabled = false;
                        //user.ChildBar.Manager.Items[i].Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                }
                //2。 再遍历画面内可检测到的按钮
                if(buttonUnable.Count>0)
                {
                    SetControlUnable(user, buttonUnable);
                }
                user.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }   
        }
        private void SetControlUnable(Control ctrl, List<string> unableCtrlNames)
        {
            if(unableCtrlNames.Contains(ctrl.Name))
            {
                ctrl.Enabled = false;
                //ctrl.Visible = false;
            }
            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                SetControlUnable(ctrl.Controls[i], unableCtrlNames);
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {

                FormUserRes user = new FormUserRes();

                
                user.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }      
         
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                string formName = "FormTEST";
                int userID = 6;
                //先根据画面名称和用户名,查询画面,按钮注册信息
                DataTable dt = Services.DbUserRes.QueryAccess(userID, formName, Services.CConstString.ConnectName);
                //表列为 name ,                 description,     type,                         num
                //-----名称(画面或者按钮名)        描述           类型 (FORM 或者BUTTON)     可访问数量(0以上为可访问)
                //为空,则没有相关注册信息
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("没有该画面的注册信息.");
                    return;
                }
                //遍历找出,可访问的画面,以及不可访问的按钮列表
                List<string> formEnable = new List<string>();
                List<string> buttonUnable = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string type = dt.Rows[i]["type"].ToString();
                    string name = dt.Rows[i]["name"].ToString();
                    string descript = dt.Rows[i]["description"].ToString();
                    int num = Convert.ToInt32(dt.Rows[i]["num"]);
                    if (type == "FORM" && num > 0)
                    {
                        formEnable.Add(name);
                    }
                    if (type == "BUTTON" && num == 0)
                    {
                        buttonUnable.Add(name);
                    }
                }
                //如果画面没有权限,则提示没权限并 退出
                if (formEnable.Count == 0)
                {
                    MessageBox.Show("没有权限访问该画面.");
                    return;
                }
                //如果画面有权限,则构造...
                FormTEST user = new FormTEST();
                //只对注册的按钮,进行权限判断
                //如果有权限,则不作任何操作,如果没权限又存在,则把(Enable=false 或者 visible=false)

                //1。 先遍历工具条
                int count = user.ChildBar.Manager.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    if (buttonUnable.Contains(user.ChildBar.Manager.Items[i].Name))
                    {
                        user.ChildBar.Manager.Items[i].Enabled = false;
                        //user.ChildBar.Manager.Items[i].Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                }
                //2。 再遍历画面内可检测到的按钮
                if (buttonUnable.Count > 0)
                {
                    SetControlUnable(user, buttonUnable);
                }
                user.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }   
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            XtraForm1 frm = new XtraForm1();
            frm.Show();
        }
    }
}