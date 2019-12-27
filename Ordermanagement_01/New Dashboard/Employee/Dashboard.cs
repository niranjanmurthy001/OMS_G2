﻿using System;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraCharts;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.Skins;
using System.Threading.Tasks;
using System.Diagnostics;
using Ordermanagement_01.Models;
using Ordermanagement_01.Reports;
using System.Collections;
using DevExpress.XtraEditors.Controls;
using static Ordermanagement_01.New_Dashboard.New_Dashboard;
using System.Runtime.InteropServices;

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class Dashboard : XtraForm
    {
        private readonly int userId, userRoleId;
        private string productionDate;
        private readonly DataAccess dataaccess;
        private int Day, Hour, Current_Holiday, Previous_Holiday, Prv_day;
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        const int MF_BYCOMMAND = 0;
        const int MF_DISABLED = 2;
        const int SC_CLOSE = 0xF060;
        /// <summary>
        /// Employee Dashboard
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRoleId"></param>
        /// <param name="productionDate"></param>
        public Dashboard(int userId, int userRoleId)
        {
            DevExpress.UserSkins.BonusSkins.Register();
            this.userId = userId;
            this.userRoleId = userRoleId;
            dataaccess = new DataAccess();
            InitializeComponent();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                var close = GetSystemMenu(Handle, false);
                EnableMenuItem(close, SC_CLOSE, MF_BYCOMMAND | MF_DISABLED);
                labelControlToDay.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
                lblCopyRight.Text = "© " + DateTime.Now.Year.ToString();
                chartControlEfficiency.Visible = false;
                //chartControlTest.Visible = false;
                chartControlAccuracy.Visible = false;
                gridControlAttendance.Visible = false;
                gridControlTimings.Visible = false;
                ActiveControl = buttonToday;
                BindSkins();
                Populate_Production_Date();
                Daily_User_Login();
                Task t = Task.WhenAll(BindUserDetailsAsync(),
                BindTimingsAsync(productionDate, productionDate),
                BindAccuracyAsync(productionDate), BindErrorsCount(),
                BindEfficiencyAsync(productionDate), Load_Order_Count());
                t.Wait(1000);
                WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

        }

        private void Daily_User_Login()
        {
            Hashtable htget_Hour = new Hashtable();
            DataTable dtget_Hour = new DataTable();
            htget_Hour.Add("@Trans", "GET_HOUR");
            dtget_Hour = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Hour);
            if (dtget_Hour.Rows.Count > 0)
            {
                Hour = int.Parse(dtget_Hour.Rows[0]["Hour"].ToString());
            }
            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            int Check = 0;
            htcheck.Add("@Trans", "CHECK_USER_LOGIN_PRODUCTION_DATE_WISE");
            htcheck.Add("@User_Id", userId);
            htcheck.Add("@Production_Date", productionDate);
            dtcheck = dataaccess.ExecuteSP("SP_User_Login_Details", htcheck);
            if (dtcheck.Rows.Count > 0)
            {
                Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {
                Check = 0;
            }
            Hashtable ht_User = new Hashtable();
            DataTable dt_user = new System.Data.DataTable();
            if (Check == 0)
            {
                ht_User.Add("@Trans", "INSERT");
                ht_User.Add("@User_Id", userId);
                ht_User.Add("@Production_Date", productionDate);
                // This is For Night Shift
                if (Hour == 17 || Hour == 18 || Hour == 19 || Hour == 20 || Hour == 21 || Hour == 22 || Hour == 23 || Hour == 0 || Hour == 1 || Hour == 2 || Hour == 3 || Hour == 4 || Hour == 5 || Hour == 6)
                {
                    ht_User.Add("@Shift_Login_Id", 3);
                }
                else
                {
                    // This is For Day Shift
                    ht_User.Add("@Shift_Login_Id", 1);
                }
                dt_user = dataaccess.ExecuteSP("SP_User_Login_Details", ht_User);
            }
            else
            {
                ht_User.Add("@Trans", "UPDATE_LOGOUT");
                ht_User.Add("@User_Id", userId);
                ht_User.Add("@Production_Date", productionDate);
                dt_user = dataaccess.ExecuteSP("SP_User_Login_Details", ht_User);
            }
        }
        private async Task Load_Order_Count()
        {
            try
            {
                using (var Client = new HttpClient())
                {
                    Dictionary<string, object> list = new Dictionary<string, object>();
                    if (userRoleId == 1 || userRoleId == 5 || userRoleId == 6)
                    {
                        list.Add("@Trans", "COUNT_OF_ORDERS_WORK_TYPE_WISE_ALL_USER_WISE");
                    }
                    else if (userRoleId == 2 || userRoleId == 3 || userRoleId == 4)
                    {
                        list.Add("@Trans", "COUNT_OF_ORDERS_WORK_TYPE_WISE_USER_WISE");

                    }
                    list.Add("@User_Id", userId);
                    var serializedUser = JsonConvert.SerializeObject(list);
                    var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                    var result = await Client.PostAsync(Base_Url.Url + "/ProcessingOrders/Processing_Order_Count", content);

                    if (result.IsSuccessStatusCode)
                    {

                        var UserJsonString = await result.Content.ReadAsStringAsync();
                        Result_Data[] Res_daata = JsonConvert.DeserializeObject<Result_Data[]>(UserJsonString);
                        if (Res_daata != null)
                        {
                            foreach (Result_Data res in Res_daata)
                            {
                                link_Order_Count.Text = res.Live_Order_Count;
                                
                            }
                        }
                        else
                        {
                            link_Order_Count.Text = "00";
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.ToString());
                XtraMessageBox.Show("Error Occured Please Check With Administrator");
            }
        }
        private void BindSkins()
        {
            SkinContainerCollection skins = SkinManager.Default.Skins;
            DataTable dtSkins = new DataTable();
            dtSkins.Columns.AddRange(new DataColumn[]
            {
                     new DataColumn("Id",typeof(int)),
                     new DataColumn("Skin",typeof(string))
            });
            for (int i = 0, key = 0; i < skins.Count; i++)
            {
                DataRow row = dtSkins.NewRow();
                row["Id"] = ++key;
                row["Skin"] = skins[i].SkinName;
                dtSkins.Rows.Add(row);
            }
            DataRow row1 = dtSkins.NewRow();
            row1["Id"] = 0;
            row1["Skin"] = "SELECT";
            dtSkins.Rows.InsertAt(row1, 0);
            lookUpEditSkins.Properties.DataSource = dtSkins;
            lookUpEditSkins.Properties.ValueMember = "Id";
            lookUpEditSkins.Properties.DisplayMember = "Skin";
            lookUpEditSkins.Properties.Columns.Add(new LookUpColumnInfo("Skin"));
        }
        #region Efficiency
        /// <summary>
        /// Efficiency binding Day wise.
        /// </summary>
        /// <param name="date"></param>        
        private async Task BindEfficiencyAsync(string date)
        {
            try
            {
                circularGaugeEfficiency.Scales[0].Value = 0;
                labelComponentEfficiency.Text = circularGaugeEfficiency.Scales[0].Value.ToString();
                var dictEfficiency = new Dictionary<string, object>()
                {
                    { "@Trans","DAILY_USER_NEW_UPDATED_EFF" },
                    { "@Production_Date",date },
                    { "@User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictEfficiency), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Efficiency/EfficiencySummary", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            arcScaleRangeBarComponent1.Value = float.Parse(dt.Rows[0]["Effecinecy"].ToString());
                            labelComponentEfficiency.Text = arcScaleRangeBarComponent1.Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Binding grid for weekly efficiency
        /// </summary>        
        private async Task BindEfficiencyWeeklyAsync(string fromDate, string toDate)
        {
            try
            {
                chartControlEfficiency.DataSource = null;
                var dictAccuracy = new Dictionary<string, object>()
                {
                  { "@Trans","USER_EFF_DATE_RANGE" },
                  { "@From_Date",fromDate },
                  { "@ToDate",toDate },
                  { "@User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictAccuracy), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Efficiency/EfficiencySummary", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var dtResult = await response.Content.ReadAsStringAsync();
                            DataTable listEfficiency = JsonConvert.DeserializeObject<DataTable>(dtResult);
                            if (listEfficiency != null && listEfficiency.Rows.Count > 0)
                            {
                                chartControlEfficiency.DataSource = listEfficiency;
                                chartControlEfficiency.Series["Efficiency"].ArgumentDataMember = "Date";
                                chartControlEfficiency.Series["Efficiency"].ValueDataMembers.AddRange("Effecinecy");
                                chartControlEfficiency.Series["Efficiency"].ArgumentScaleType = ScaleType.Auto;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void groupControlEfficiency_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Application.OpenForms["Efficiency_Summary"] != null)
            {
                Application.OpenForms["Efficiency_Summary"].Focus();
            }
            else
            {
                Efficiency_Summary summary = new Efficiency_Summary(userId, userRoleId, productionDate.ToString());
                Invoke(new MethodInvoker(delegate { summary.Show(); }));
            }
        }
        private void chartControlEfficiency_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Efficiency_Summary"] != null)
            {
                Application.OpenForms["Efficiency_Summary"].Focus();
            }
            else
            {
                Efficiency_Summary summary = new Efficiency_Summary(userId, userRoleId, productionDate.ToString());
                Invoke(new MethodInvoker(delegate { summary.Show(); }));
            }
        }
        private void gaugeControlEfficiency_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Efficiency_Summary"] != null)
            {
                Application.OpenForms["Efficiency_Summary"].Focus();
            }
            else
            {
                Efficiency_Summary summary = new Efficiency_Summary(userId, userRoleId, productionDate.ToString());
                Invoke(new MethodInvoker(delegate { summary.Show(); }));
            }
        }
        #endregion
        #region Accuracy
        private async Task BindAccuracyAsync(string date)
        {
            try
            {
                circularGauge_Accuracy.Scales[0].Value = 0;
                arcScaleNeedleComponent2.Value = circularGauge_Accuracy.Scales[0].Value;
                var dictAccuracy = new Dictionary<string, object>()
                {
                    { "@Trans","CALCULATE_MONTHLY_ACCURACY_USER_WISE" },
                    { "@From_date",date },
                    { "@To_Date",date },
                    { "@Error_On_User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictAccuracy), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Accuracy/AccuracySummary", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var dtResult = await response.Content.ReadAsStringAsync();
                            var listAccuracy = JsonConvert.DeserializeObject<List<Models.Employee.AccuracySummary>>(dtResult);
                            if (listAccuracy != null && listAccuracy.Count > 0)
                            {
                                circularGauge_Accuracy.Scales[0].Value = (float)listAccuracy[0].Accuracy;
                                arcScaleNeedleComponent2.Value = circularGauge_Accuracy.Scales[0].Value;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task BindAccuracyWeeklyAsync(string fromDate, string toDate)
        {
            try
            {
                chartControlAccuracy.DataSource = null;
                var dictAccuracy = new Dictionary<string, object>()
                {
                  { "@Trans","CALCULATE_MONTHLY_ACCURACY_USER_WISE" },
                  { "@From_date",fromDate },
                  { "@To_Date",toDate },
                  { "@Error_On_User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictAccuracy), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Accuracy/AccuracySummary", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var dtResult = await response.Content.ReadAsStringAsync();
                            var listAccuracy = JsonConvert.DeserializeObject<List<Models.Employee.AccuracySummary>>(dtResult);
                            if (listAccuracy != null && listAccuracy.Count > 0)
                            {
                                chartControlAccuracy.DataSource = listAccuracy;
                                chartControlAccuracy.Series["Accuracy"].ArgumentDataMember = "Date";
                                chartControlAccuracy.Series["Accuracy"].ValueDataMembers.AddRange("Accuracy");
                                chartControlAccuracy.Series["Accuracy"].ArgumentScaleType = ScaleType.Auto;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void groupControlAccuracy_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Application.OpenForms["Accuracy_Summary"] != null)
            {
                Application.OpenForms["Accuracy_Summary"].Focus();
            }
            else
            {
                Accuracy_Summary summary = new Accuracy_Summary(userId, userRoleId, productionDate.ToString());
                Invoke(new MethodInvoker(delegate { summary.Show(); }));
            }
        }
        private void chartControlAccuracy_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Accuracy_Summary"] != null)
            {
                Application.OpenForms["Accuracy_Summary"].Focus();
            }
            else
            {
                Accuracy_Summary summary = new Accuracy_Summary(userId, userRoleId, productionDate.ToString());
                Invoke(new MethodInvoker(delegate { summary.Show(); }));
            }
        }
        private void gaugeControl_Accuracy_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Accuracy_Summary"] != null)
            {
                Application.OpenForms["Accuracy_Summary"].Focus();
            }
            else
            {
                Accuracy_Summary summary = new Accuracy_Summary(userId, userRoleId, productionDate.ToString());
                Invoke(new MethodInvoker(delegate { summary.Show(); }));
            }
        }
        #endregion
        #region Timings
        private async Task BindTimingsAsync(string fromDate, string toDate)
        {
            try
            {
                chartControlTimings.Series[0].DataSource = null;
                labelControlLoginTime.Text = string.Empty;
                labelControl2.Visible = false;
                labelControlLoginStatus.Text = string.Empty;
                labelControlLoginStatus.ForeColor = Color.Red;
                var dictionary = new Dictionary<string, object>()
                 {
                    { "@Trans", "SUMMARY" },
                    { "@From_Date",fromDate },
                    { "@To_Date", toDate },
                    { "@User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/User/Timings", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            DataRow row = dt.Rows[0];
                            if (!string.IsNullOrEmpty(row["Login_Time"].ToString()))
                            {
                                labelControl2.Visible = true;
                                labelControlLoginTime.Text = row["Login_Time"].ToString();
                                labelControlLoginStatus.Text = "P";
                                labelControlLoginStatus.ForeColor = Color.ForestGreen;
                            }
                            else
                            {
                                labelControlLoginStatus.Text = "A";
                            }
                            if (Convert.ToInt32(TimeSpan.Parse(row["Production Hours"].ToString()).TotalHours) > 0)
                            {
                                int productionTime = Convert.ToInt32(TimeSpan.Parse(row["Production Hours"].ToString()).TotalHours);
                                int breakTime = Convert.ToInt32(TimeSpan.Parse(row["Break Hours"].ToString()).TotalHours);
                                int idleTime = Convert.ToInt32(TimeSpan.Parse(row["Ideal Hours"].ToString()).TotalHours);
                                List<DataPoint> list = new List<DataPoint>()
                                {
                                    new DataPoint{ Argument="Production",Value=productionTime },
                                    new DataPoint{ Argument="Break",Value=breakTime           },
                                    new DataPoint{ Argument="Idle",Value=idleTime             }
                                };
                                chartControlTimings.Series[0].DataSource = list;
                                chartControlTimings.Series[0].ArgumentDataMember = "Argument";
                                chartControlTimings.Series[0].ValueDataMembers.AddRange("Value");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private async Task BindWeeklyTimingsAsync(string fromDate, string toDate)
        {
            try
            {
                gridControlTimings.DataSource = null;
                var dictionary = new Dictionary<string, object>()
                 {
                    { "@Trans", "SUMMARY" },
                    { "@From_Date",fromDate },
                    { "@To_Date", toDate },
                    { "@User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/User/Timings", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            gridControlTimings.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void chartControlTimings_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["BreakIdleReportsNew"] != null)
            {
                Application.OpenForms["BreakIdleReportsNew"].Focus();
            }
            else
            {
                BreakIdleReportsNew reports = new BreakIdleReportsNew(userId, userRoleId);
                Invoke(new MethodInvoker(delegate { reports.Show(); }));
            }

        }
        private void gridViewTimings_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["BreakIdleReportsNew"] != null)
            {
                Application.OpenForms["BreakIdleReportsNew"].Focus();
            }
            else
            {
                BreakIdleReportsNew reports = new BreakIdleReportsNew(userId, userRoleId);
                Invoke(new MethodInvoker(delegate { reports.Show(); }));
            }
        }
        private void groupControlTimings_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Application.OpenForms["BreakIdleReportsNew"] != null)
            {
                Application.OpenForms["BreakIdleReportsNew"].Focus();
            }
            else
            {
                BreakIdleReportsNew reports = new BreakIdleReportsNew(userId, userRoleId);
                Invoke(new MethodInvoker(delegate { reports.Show(); }));
            }
        }
        #endregion
        #region Attendance
        private async Task BindWeeklyAttendanceAsync(string fromDate, string toDate)
        {
            try
            {
                gridControlAttendance.DataSource = null;
                var dictionary = new Dictionary<string, object>()
                 {
                    { "@Trans", "SELECT_USER_WISE" },
                    { "@From_Date",fromDate },
                    { "@To_Date", toDate },
                    { "@User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/User/Attendance", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            gridControlAttendance.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void panelControlAttendance_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["User_Login_Details"] != null)
            {
                Application.OpenForms["User_Login_Details"].Focus();
            }
            else
            {
                User_Login_Details user = new User_Login_Details(userId, userRoleId);
                Invoke(new MethodInvoker(delegate { user.Show(); }));
            }
        }
        private void gridViewAttendance_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["User_Login_Details"] != null)
            {
                Application.OpenForms["User_Login_Details"].Focus();
            }
            else
            {
                User_Login_Details user = new User_Login_Details(userId, userRoleId);
                Invoke(new MethodInvoker(delegate { user.Show(); }));
            }
        }
        private void labelControlLoginStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Application.OpenForms["User_Login_Details"] != null)
            {
                Application.OpenForms["User_Login_Details"].Focus();
            }
            else
            {
                User_Login_Details user = new User_Login_Details(userId, userRoleId);
                Invoke(new MethodInvoker(delegate { user.Show(); }));
            }
        }
        private void groupControlAttendance_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Application.OpenForms["User_Login_Details"] != null)
            {
                Application.OpenForms["User_Login_Details"].Focus();
            }
            else
            {
                User_Login_Details user = new User_Login_Details(userId, userRoleId);
                Invoke(new MethodInvoker(delegate { user.Show(); }));
            }
        }
        #endregion
        #region Profile
        private async Task BindUserDetailsAsync()
        {
            try
            {
                labelControlName.Text = string.Empty;
                labelControlEmpCode.Text = string.Empty;
                labelControlBranch.Text = string.Empty;
                labelControlRole.Text = string.Empty;
                labelControlReporting.Text = string.Empty;
                labelControlShift.Text = string.Empty;
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync($"{Base_Url.Url}/User/GetUser/{userId}");
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            var user = JsonConvert.DeserializeAnonymousType(data, new
                            {
                                EmployeeName = string.Empty,
                                Code = string.Empty,
                                Branch = string.Empty,
                                Role = string.Empty,
                                Reporting = string.Empty,
                                Shift = string.Empty,
                                Theme = string.Empty
                            });
                            labelControlName.Text = user.EmployeeName ?? string.Empty;
                            labelControlEmpCode.Text = user.Code ?? string.Empty;
                            labelControlBranch.Text = user.Branch ?? string.Empty;
                            labelControlRole.Text = user.Role ?? string.Empty;
                            labelControlReporting.Text = user.Reporting ?? string.Empty;
                            labelControlShift.Text = user.Shift ?? string.Empty;
                            if (!string.IsNullOrEmpty(user.Theme))
                            {
                                lookUpEditSkins.EditValue = Convert.ToInt32(user.Theme);
                            }
                            else
                            {
                                lookUpEditSkins.EditValue = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private async void buttonEditProfile_Click(object sender, EventArgs e)
        {
            ChangePassword password = new ChangePassword();
            if (XtraDialog.Show(password, "Change Password", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                string newPassword = password.NewPassword.Text.Trim();
                string confirmPassword = password.ConfirmPassword.Text.Trim();
                if (string.IsNullOrEmpty(newPassword))
                {
                    XtraMessageBox.Show("Enter password");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPassword))
                {
                    XtraMessageBox.Show("Enter confirm password");
                    return;
                }
                if (newPassword != string.Empty && confirmPassword != string.Empty && string.Equals(newPassword, confirmPassword))
                {
                    try
                    {
                        SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                        var dictionary = new Dictionary<string, object>()
                         {
                            { "@Trans", "CHANGEPASSWORD" },
                            { "@User_Id",userId          },
                            { "@NewPassword",newPassword }
                         };

                        var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                        using (var httpClient = new HttpClient())
                        {
                            var response = await httpClient.PutAsync(Base_Url.Url + "/User/ChangePassword", data);
                            if (response.IsSuccessStatusCode)
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    SplashScreenManager.CloseForm(false);
                                    XtraMessageBox.Show("Password changed successfully");
                                }
                                else
                                {
                                    SplashScreenManager.CloseForm(false);
                                    XtraMessageBox.Show("Failed to change password.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Something went wrong");
                    }
                    finally
                    {
                        SplashScreenManager.CloseForm(false);
                    }
                }
                else
                {
                    XtraMessageBox.Show("New password and confirm password are not same.");
                }
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Sure want to logout?", "Logout", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }
        private void buttonToday_Click(object sender, EventArgs e)
        {
            BindToday();
        }
        private void BindToday()
        {
            gaugeControlEfficiency.Visible = true;
            // gaugeControlTest.Visible = true;
            gaugeControl_Accuracy.Visible = true;
            panelControlAttendance.Visible = true;
            chartControlTimings.Visible = true;
            chartControlEfficiency.Visible = false;
            // chartControlTest.Visible = false;
            chartControlAccuracy.Visible = false;
            gridControlAttendance.Visible = false;
            gridControlTimings.Visible = false;
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Task t = Task.WhenAll(BindTimingsAsync(productionDate, productionDate),
                 BindAccuracyAsync(productionDate), BindErrorsCount(),
                 BindEfficiencyAsync(productionDate), Load_Order_Count());
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void buttonYesterday_Click(object sender, EventArgs e)
        {
            gaugeControlEfficiency.Visible = true;
            // gaugeControlTest.Visible = true;
            gaugeControl_Accuracy.Visible = true;
            panelControlAttendance.Visible = true;
            chartControlTimings.Visible = true;

            chartControlEfficiency.Visible = false;
            //  chartControlTest.Visible = false;
            chartControlAccuracy.Visible = false;
            gridControlAttendance.Visible = false;
            gridControlTimings.Visible = false;

            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                string yesterday = Convert.ToDateTime(productionDate).AddDays(-1).ToShortDateString();
                Task t = Task.WhenAll(
                 BindTimingsAsync(yesterday, yesterday),
                 BindAccuracyAsync(yesterday),
                 BindEfficiencyAsync(yesterday));
                t.Wait(1000);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void buttonWeek_Click(object sender, EventArgs e)
        {
            gaugeControlEfficiency.Visible = false;
            //   gaugeControlTest.Visible = false;
            gaugeControl_Accuracy.Visible = false;
            panelControlAttendance.Visible = false;
            chartControlTimings.Visible = false;

            //    chartControlTest.Visible = true;
            chartControlEfficiency.Visible = true;
            chartControlAccuracy.Visible = true;
            gridControlAttendance.Visible = true;
            gridControlTimings.Visible = true;

            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Task t = Task.WhenAll(
                 BindWeeklyTimingsAsync(Convert.ToDateTime(productionDate).AddDays(-6).ToShortDateString(), productionDate),
                 BindWeeklyAttendanceAsync(Convert.ToDateTime(productionDate).AddDays(-6).ToShortDateString(), productionDate),
                 BindAccuracyWeeklyAsync(Convert.ToDateTime(productionDate).AddDays(-6).ToShortDateString(), productionDate),
                 BindEfficiencyWeeklyAsync(Convert.ToDateTime(productionDate).AddDays(-6).ToShortDateString(), productionDate)
                );
                t.Wait(1000);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }


        }
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            BindToday();
        }
        private async void buttonTheme_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditSkins.EditValue) < 1)
            {
                XtraMessageBox.Show("Select theme");
                lookUpEditSkins.Focus();
                return;
            }
            try
            {
                var dictionary = new Dictionary<string, object>()
                     {
                        { "@Trans", "UPDATE_SKIN" },
                        { "@Theme",lookUpEditSkins.EditValue },
                        { "@User_id",userId }
                    };

                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/User/Theme", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            XtraMessageBox.Show("Theme applied successfully");
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Failed to apply theme");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Failed to apply theme");
            }
        }
        private void lookUpEditSkins_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditSkins.EditValue) > 0)
            {
                defaultLookAndFeel1.LookAndFeel.SkinName = lookUpEditSkins.Text;
            }
        }
        private void linklabelDRN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://drnds.com/");
        }
        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Daily_User_Login();
            Application.Exit();
        }
        private void buttonBreak_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                Ordermanagement_01.Employee.Break_DetailsNew breakDetails = new Ordermanagement_01.Employee.Break_DetailsNew(userId, DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.ToString("MM/dd/yyyy"), productionDate);
                Invoke(new MethodInvoker(delegate { breakDetails.Show(); }));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("something went wrong");
            }
        }

        private void buttonIdle_Click(object sender, EventArgs e)
        {
            try
            {
                Ordermanagement_01.Dashboard.IdleTrack iTrack = new Ordermanagement_01.Dashboard.IdleTrack(userId, productionDate, true);
                Invoke(new MethodInvoker(delegate { iTrack.Show(); }));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("something went wrong");
            }
        }

        private void buttonErrors_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                if (Application.OpenForms["Error_Dashboard"] != null)
                {
                    Application.OpenForms["Error_Dashboard"].Focus();
                }
                else
                {
                    Ordermanagement_01.Employee.Error_Dashboard errors = new Ordermanagement_01.Employee.Error_Dashboard(userId, userRoleId, productionDate);
                    errors.Show();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Problem while Opening Please Check with Administrator");
                SplashScreenManager.CloseForm(false);
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

        }

        private void btn_notification_Click(object sender, EventArgs e)
        {

        }

        private void link_Order_Count_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                if (Application.OpenForms["New_Dashboard"] != null)
                {
                    Application.OpenForms["New_Dashboard"].Focus();
                }
                else
                {
                    New_Dashboard New_dash = new New_Dashboard(userId, userRoleId);
                    Invoke(new MethodInvoker(delegate { New_dash.Show(); }));
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex + "Problem while Opening Please Check with Administrator");
                SplashScreenManager.CloseForm(false);
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        #endregion
        #region Error
        private async Task BindErrorsCount()
        {
            try
            {
                linkLabelErrors.Text = "00";
                var dictErrors = new Dictionary<string, object>();
                if (userRoleId == 2)
                {
                    dictErrors.Add("@Trans", "NEW_ERRORS_COUNT_FOR_USER_WISE");
                }
                else
                {
                    dictErrors.Add("@Trans", "NEW_ERRORS_COUNT_FOR_ADMIN_WISE");
                }
                dictErrors.Add("@Error_On_User_Id", userId);
                var data = new StringContent(JsonConvert.SerializeObject(dictErrors), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/User/Errors", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var dtResult = await response.Content.ReadAsStringAsync();
                            DataTable dtErrors = JsonConvert.DeserializeObject<DataTable>(dtResult);
                            if (dtErrors.Rows.Count > 0)
                            {
                                linkLabelErrors.Text = dtErrors.Rows[0]["Error_Count"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void groupControlErrors_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                if (Application.OpenForms["Error_Dashboard"] != null)
                {
                    Application.OpenForms["Error_Dashboard"].Focus();
                }
                else
                {
                    Ordermanagement_01.Employee.Error_Dashboard errors = new Ordermanagement_01.Employee.Error_Dashboard(userId, userRoleId, productionDate);
                    errors.Show();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Problem while Opening Please Check with Administrator");
                SplashScreenManager.CloseForm(false);
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        #endregion
        private void Populate_Production_Date()
        {
            try
            {
                Hashtable htget_day = new Hashtable();
                DataTable dtget_Day = new DataTable();

                htget_day.Add("@Trans", "GET_WEEK_END_DAY");
                dtget_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_day);
                if (dtget_Day.Rows.Count > 0)
                {
                    Day = int.Parse(dtget_Day.Rows[0]["Day"].ToString());
                }

                Hashtable htget_Hour = new Hashtable();
                DataTable dtget_Hour = new DataTable();

                htget_Hour.Add("@Trans", "GET_HOUR");
                dtget_Hour = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Hour);
                if (dtget_Hour.Rows.Count > 0)
                {
                    Hour = int.Parse(dtget_Hour.Rows[0]["Hour"].ToString());
                }
                if (Day != null && Hour != null)
                {
                    //Check Day in Week days
                    //Tuesday To Friday For day Shift
                    if (Day == 3 || Day == 4 || Day == 5 || Day == 6)
                    {
                        //Check Hours
                        //For Day Shift
                        if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16)
                        {
                            //Check the Current Day is Holiday 
                            Hashtable htcheck_Holiday = new Hashtable();
                            DataTable dtcheck_Holiday = new DataTable();
                            htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_CURRENT_DATE");
                            dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);
                            Current_Holiday = 0;
                            if (dtcheck_Holiday.Rows.Count > 0)
                            {
                                Hashtable htget_Current_day = new Hashtable();
                                DataTable dtget_Current_Day = new DataTable();
                                htget_Current_day.Add("@Trans", "GET_CURRENT_DAY");
                                dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);
                                if (dtget_Current_Day.Rows.Count > 0)
                                {
                                    Current_Holiday = 1;
                                    productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                                }
                            }
                            else
                            {
                                // Check the previous Day is Holiday or not 
                                //Checking 

                                Previous_Holiday = 0;
                                Hashtable htget_Day_prod_date = new Hashtable();
                                DataTable dtget_Day_Prod_Date = new DataTable();

                                htget_Day_prod_date.Add("@Trans", "GET_DAY_SHIFT_PRV_DAY");
                                dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                if (dtget_Day_Prod_Date.Rows.Count > 0)
                                {
                                    // Production_Date = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                    htcheck_Holiday.Clear();
                                    htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_DATE");
                                    htcheck_Holiday.Add("@Date", dtget_Day_Prod_Date.Rows[0]["Production_Date"]);
                                    dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);

                                    if (dtcheck_Holiday.Rows.Count > 0)
                                    {
                                        //if the Previous Day is Holiday
                                        Previous_Holiday = 1;
                                        Hashtable htget_day1 = new Hashtable();
                                        DataTable dtget_Day1 = new DataTable();

                                        htget_day1.Add("@Trans", "GET_DAY_NO_BY_DATE");
                                        htget_day1.Add("@Date", dtcheck_Holiday.Rows[0]["Holiday_date"].ToString());
                                        dtget_Day1 = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_day1);
                                        if (dtget_Day1.Rows.Count > 0)
                                        {
                                            Prv_day = int.Parse(dtget_Day1.Rows[0]["Day"].ToString());
                                        }
                                        if (Prv_day == 3 || Prv_day == 4 || Prv_day == 5 || Prv_day == 6)
                                        {

                                            //If its Weekdays ====== Prod.date=Holiday.Date-1
                                            Hashtable htget_Day_prod_date1 = new Hashtable();
                                            DataTable dtget_Day_prod_date1 = new DataTable();

                                            htget_Day_prod_date1.Add("@Trans", "GET_DAY_SHIFT_PRV_DAY_BY_HOLIDAYDATE");
                                            htget_Day_prod_date1.Add("@Date", dtcheck_Holiday.Rows[0]["Holiday_date"].ToString());
                                            dtget_Day_prod_date1 = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date1);

                                            if (dtget_Day_prod_date1.Rows.Count > 0)
                                            {
                                                productionDate = dtget_Day_prod_date1.Rows[0]["Production_Date"].ToString();
                                            }
                                        }
                                        else if (Prv_day == 2)
                                        {
                                            //For Day Shift
                                            if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16)
                                            {
                                                //Gettting Friday Day if the day is monday
                                                htget_Day_prod_date.Clear();
                                                dtget_Day_Prod_Date.Clear();
                                                htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                                                dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                                if (dtget_Day_Prod_Date.Rows.Count > 0)
                                                {
                                                    //Check The Friday Is Holiday Or Not
                                                    htcheck_Holiday.Clear();
                                                    htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_DATE");
                                                    htcheck_Holiday.Add("@Date", dtget_Day_Prod_Date.Rows[0]["Production_Date"]);
                                                    dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);

                                                    if (dtcheck_Holiday.Rows.Count > 0)
                                                    {
                                                        htget_Day_prod_date.Clear();
                                                        dtget_Day_Prod_Date.Clear();
                                                        htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_LEAVE_ON_MONDAY");
                                                        dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                                        if (dtget_Day_Prod_Date.Rows.Count > 0)
                                                        {
                                                            productionDate = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        htget_Day_prod_date.Clear();
                                                        dtget_Day_Prod_Date.Clear();
                                                        htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                                                        dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);
                                                        if (dtget_Day_Prod_Date.Rows.Count > 0)
                                                        {
                                                            productionDate = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Previous_Holiday == 0 && Current_Holiday == 0)
                                        {
                                            //This IS Current Day is Not holiday and Previous day is not Holiday Then
                                            //This is from Tuesday-Friday
                                            if (Day == 3 || Day == 4 || Day == 5 || Day == 6)
                                            {
                                                //Check Hours
                                                //For Day Shift
                                                if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16)
                                                {
                                                    Hashtable htget_Day_prod_date1 = new Hashtable();
                                                    DataTable dtget_Day_prod_date1 = new DataTable();

                                                    htget_Day_prod_date1.Add("@Trans", "GET_DAY_SHIFT_PRV_DAY");
                                                    dtget_Day_prod_date1 = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date1);

                                                    if (dtget_Day_prod_date1.Rows.Count > 0)
                                                    {
                                                        productionDate = dtget_Day_prod_date1.Rows[0]["Production_Date"].ToString();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //If not Prvious Day Holiday Then Prd.date=Prv.Day
                                            if (Prv_day == 3 || Prv_day == 4 || Prv_day == 5 || Prv_day == 6)
                                            {
                                                Hashtable htget_Day_prod_date1 = new Hashtable();
                                                DataTable dtget_Day_prod_date1 = new DataTable();

                                                htget_Day_prod_date1.Add("@Trans", "GET_DAY_SHIFT_PRV_DAY");
                                                dtget_Day_prod_date1 = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date1);

                                                if (dtget_Day_prod_date1.Rows.Count > 0)
                                                {
                                                    productionDate = dtget_Day_prod_date1.Rows[0]["Production_Date"].ToString();
                                                }
                                            }
                                            else if (Prv_day == 2)
                                            {
                                                //For Day Shift
                                                if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16)
                                                {
                                                    //Gettting Friday Day if the day is monday
                                                    htget_Day_prod_date.Clear();
                                                    dtget_Day_Prod_Date.Clear();
                                                    htget_day.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                                                    dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                                    if (dtget_Day_Prod_Date.Rows.Count > 0)
                                                    {
                                                        productionDate = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        //This is For Night Shift
                        else if (Hour == 19 || Hour == 20 || Hour == 21 || Hour == 22 || Hour == 23 || Hour == 0 || Hour == 1 || Hour == 2 || Hour == 3 || Hour == 4 || Hour == 5 || Hour == 6)
                        {
                            Hashtable htcheck_Holiday = new Hashtable();
                            DataTable dtcheck_Holiday = new DataTable();
                            htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_CURRENT_DATE_FOR_NIGHT_SHIFT");
                            dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);
                            Current_Holiday = 0;
                            if (dtcheck_Holiday.Rows.Count > 0)
                            {
                                Hashtable htget_Current_day = new Hashtable();
                                DataTable dtget_Current_Day = new DataTable();
                                htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                                dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                                if (dtget_Current_Day.Rows.Count > 0)
                                {
                                    productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                                }
                            }
                            else
                            {
                                Hashtable htget_Current_day = new Hashtable();
                                DataTable dtget_Current_Day = new DataTable();
                                htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                                dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                                if (dtget_Current_Day.Rows.Count > 0)
                                {
                                    productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                                }
                            }
                        }

                        else if (Hour == 17 || Hour == 18)
                        {
                            // This is Day Shift Ending and Nightshfit begining

                            // Check the User Logged in this Production Date

                            Hashtable htcheck_Holiday = new Hashtable();
                            DataTable dtcheck_Holiday = new DataTable();
                            htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_CURRENT_DATE_FOR_NIGHT_SHIFT");
                            dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);
                            Current_Holiday = 0;
                            if (dtcheck_Holiday.Rows.Count > 0)
                            {
                                Hashtable htget_Current_day = new Hashtable();
                                DataTable dtget_Current_Day = new DataTable();
                                htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                                dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                                if (dtget_Current_Day.Rows.Count > 0)
                                {
                                    productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                                }
                            }
                            else
                            {
                                Hashtable htget_Current_day = new Hashtable();
                                DataTable dtget_Current_Day = new DataTable();
                                htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                                dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                                if (dtget_Current_Day.Rows.Count > 0)
                                {
                                    productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                                }
                            }
                        }
                    }
                    //For Monday Day Shift
                    else if (Day == 2)
                    {
                        //Check Hours
                        //For Day Shift
                        if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16)
                        {
                            Hashtable htget_Day_prod_date = new Hashtable();
                            DataTable dtget_Day_Prod_Date = new DataTable();

                            Hashtable htcheck_Holiday = new Hashtable();
                            DataTable dtcheck_Holiday = new DataTable();
                            //Gettting Friday Day if the day is monday

                            htget_Day_prod_date.Clear();
                            dtget_Day_Prod_Date.Clear();
                            htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                            dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                            if (dtget_Day_Prod_Date.Rows.Count > 0)
                            {
                                //Check The Friday Is Holiday Or Not

                                htcheck_Holiday.Clear();
                                htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_DATE");
                                htcheck_Holiday.Add("@Date", dtget_Day_Prod_Date.Rows[0]["Production_Date"]);
                                dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);

                                if (dtcheck_Holiday.Rows.Count > 0)
                                {
                                    htget_Day_prod_date.Clear();
                                    dtget_Day_Prod_Date.Clear();
                                    htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_LEAVE_ON_MONDAY");
                                    dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                    if (dtget_Day_Prod_Date.Rows.Count > 0)
                                    {
                                        productionDate = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                    }
                                }
                                else
                                {
                                    htget_Day_prod_date.Clear();
                                    dtget_Day_Prod_Date.Clear();
                                    htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                                    dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                    if (dtget_Day_Prod_Date.Rows.Count > 0)
                                    {
                                        productionDate = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                    }
                                }
                            }
                        }
                        //This is For Night Shift
                        else if (Hour == 19 || Hour == 20 || Hour == 21 || Hour == 22 || Hour == 23 || Hour == 0 || Hour == 1 || Hour == 2 || Hour == 3 || Hour == 4 || Hour == 5 || Hour == 6)
                        {
                            Hashtable htget_Current_day = new Hashtable();
                            DataTable dtget_Current_Day = new DataTable();
                            htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                            dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                            if (dtget_Current_Day.Rows.Count > 0)
                            {
                                productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                            }
                        }

                        else if (Hour == 17 || Hour == 18)
                        {
                            // This is Day Shift Ending and Nightshfit begining

                            // Check the User Logged in this Production Date

                            Hashtable htcheck_Holiday = new Hashtable();
                            DataTable dtcheck_Holiday = new DataTable();
                            htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_CURRENT_DATE_FOR_NIGHT_SHIFT");
                            dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);
                            Current_Holiday = 0;
                            if (dtcheck_Holiday.Rows.Count > 0)
                            {
                                Hashtable htget_Current_day = new Hashtable();
                                DataTable dtget_Current_Day = new DataTable();
                                htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                                dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                                if (dtget_Current_Day.Rows.Count > 0)
                                {
                                    productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                                }
                            }
                            else
                            {
                                Hashtable htget_Current_day = new Hashtable();
                                DataTable dtget_Current_Day = new DataTable();
                                htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                                dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                                if (dtget_Current_Day.Rows.Count > 0)
                                {
                                    productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                                }
                            }
                        }
                    }
                    //For Sat-Sunday Day Shift
                    else if (Day == 7 || Day == 1)
                    {
                        //Check Hours
                        //For Day Shift
                        if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16)
                        {
                            //Prod.Date=Current.Date
                            Hashtable htget_Current_day = new Hashtable();
                            DataTable dtget_Current_Day = new DataTable();
                            htget_Current_day.Add("@Trans", "GET_CURRENT_DAY");
                            dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                            if (dtget_Current_Day.Rows.Count > 0)
                            {
                                Current_Holiday = 1;
                                productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                            }
                        }
                        if (Hour == 19 || Hour == 20 || Hour == 21 || Hour == 22 || Hour == 23 || Hour == 0 || Hour == 1 || Hour == 2 || Hour == 3 || Hour == 4 || Hour == 5 || Hour == 6)
                        {
                            Hashtable htget_Current_day = new Hashtable();
                            DataTable dtget_Current_Day = new DataTable();
                            htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                            dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                            if (dtget_Current_Day.Rows.Count > 0)
                            {
                                productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                            }
                        }

                        else if (Hour == 17 || Hour == 18)
                        {
                            // This is Day Shift Ending and Nightshfit begining

                            // Check the User Logged in this Production Date

                            Hashtable htcheck_Holiday = new Hashtable();
                            DataTable dtcheck_Holiday = new DataTable();
                            htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_CURRENT_DATE_FOR_NIGHT_SHIFT");
                            dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);
                            Current_Holiday = 0;
                            if (dtcheck_Holiday.Rows.Count > 0)
                            {
                                Hashtable htget_Current_day = new Hashtable();
                                DataTable dtget_Current_Day = new DataTable();
                                htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                                dtget_Current_Day = dataaccess.ExecuteSP("Production_Date", htget_Current_day);

                                if (dtget_Current_Day.Rows.Count > 0)
                                {
                                    productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                                }
                            }
                            else
                            {
                                Hashtable htget_Current_day = new Hashtable();
                                DataTable dtget_Current_Day = new DataTable();
                                htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                                dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                                if (dtget_Current_Day.Rows.Count > 0)
                                {
                                    productionDate = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                                }
                            }
                        }
                    }
                    productionDate = Convert.ToDateTime(productionDate).ToString("MM/dd/yyyy");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private class DataPoint
        {
            public string Argument { get; set; }
            public double Value { get; set; }
        }
    }
}