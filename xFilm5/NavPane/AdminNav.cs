#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

#endregion

namespace xFilm5.NavPane
{
    public partial class AdminNav : UserControl
    {
        public AdminNav()
        {
            InitializeComponent();

            NavPane.NavMenu.FillNavTree("admin", this.navAdmin.Nodes);
        }

        private void navMemberMgmt_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = navAdmin.SelectedNode.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)navAdmin.SelectedNode.Tag);
                ShowAppToolStrip((string)navAdmin.SelectedNode.Tag);
            }
        }

        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                switch (Tag.ToLower())
                {
                    case "cu_address":
                        xFilm5.Customer.Address.AddressList cuAddress = new xFilm5.Customer.Address.AddressList();
                        cuAddress.DockPadding.All = 6;
                        cuAddress.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(cuAddress);
                        break;
                    case "cu_user":
                        xFilm5.Customer.Staff.StaffList cuStaff = new xFilm5.Customer.Staff.StaffList();
                        cuStaff.DockPadding.All = 6;
                        cuStaff.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(cuStaff);
                        break;
                    case "cu_outstanding":
                        xFilm5.Customer.Billing.Aging cuAging = new xFilm5.Customer.Billing.Aging();
                        cuAging.DockPadding.All = 6;
                        cuAging.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(cuAging);
                        break;
                    case "cu_invoicelist":
                        xFilm5.Customer.Billing.InvoiceList cuInvoiceList = new xFilm5.Customer.Billing.InvoiceList();
                        cuInvoiceList.DockPadding.All = 6;
                        cuInvoiceList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(cuInvoiceList);
                        break;
                    case "cu_orderlist":
                        xFilm5.Customer.Billing.OrderList cuOrderList = new xFilm5.Customer.Billing.OrderList();
                        cuOrderList.DockPadding.All = 6;
                        cuOrderList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(cuOrderList);
                        break;
                    case "cu_dnlist":
                        xFilm5.Customer.Billing.DNList_v5 cuDNList = new xFilm5.Customer.Billing.DNList_v5();
                        cuDNList.DockPadding.All = 6;
                        cuDNList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(cuDNList);
                        break;
                    case "admin_staff":
                        xFilm5.Admin.Staff.StaffList oStaffList = new xFilm5.Admin.Staff.StaffList();
                        oStaffList.DockPadding.All = 6;
                        oStaffList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oStaffList);
                        break;
                    case "admin_fcm":
                        Admin.FCMConsole oFCM = new Admin.FCMConsole();
                        oFCM.DockPadding.All = 6;
                        oFCM.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oFCM);
                        break;
                }
            }
        }

        #region Show private AppToolStrip
        private void ShowAppToolStrip(string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);
                if (controls.Length > 0)
                {
                    Panel atsPane = (Panel)controls[0];

                    switch (Tag.ToLower())
                    {
                        case "admin_staff":
                            xFilm5.Admin.Staff.StaffListAts stafflistAts = new xFilm5.Admin.Staff.StaffListAts();
                            stafflistAts.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(stafflistAts);
                            break;
                        case "cu_address":
                            // 取消獨立的 ATS
                            //xFilm5.Customer.Address.AddressListAts addresstAts = new xFilm5.Customer.Address.AddressListAts();
                            //addresstAts.Dock = DockStyle.Fill;
                            //atsPane.Controls.Clear();
                            //atsPane.Controls.Add(addresstAts);
                            break;
                    }
                }
            }
        }
        #endregion
    }
}