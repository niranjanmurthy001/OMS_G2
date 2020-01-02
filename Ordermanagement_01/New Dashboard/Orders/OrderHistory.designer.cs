﻿namespace Ordermanagement_01
{
    partial class OrderHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderHistory));
            this.groupControlOrderHistory = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_Clientname = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_County = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_State = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_Subprocess = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlOrderHistory = new DevExpress.XtraGrid.GridControl();
            this.gridViewOrderHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Assignee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Assigner = new DevExpress.XtraGrid.Columns.GridColumn();
            this.orderTask = new DevExpress.XtraGrid.Columns.GridColumn();
            this.progressType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.workType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.stage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlOrderStatusHistory = new DevExpress.XtraGrid.GridControl();
            this.gridViewOrderStatusHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.task = new DevExpress.XtraGrid.Columns.GridColumn();
            this.status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.statusChangeby = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.changedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlOrderHistory)).BeginInit();
            this.groupControlOrderHistory.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrderHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrderHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrderStatusHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrderStatusHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlOrderHistory
            // 
            this.groupControlOrderHistory.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlOrderHistory.AppearanceCaption.Options.UseFont = true;
            this.groupControlOrderHistory.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControlOrderHistory.CaptionImageOptions.Image")));
            this.groupControlOrderHistory.Controls.Add(this.tableLayoutPanel1);
            this.groupControlOrderHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlOrderHistory.Location = new System.Drawing.Point(0, 0);
            this.groupControlOrderHistory.Name = "groupControlOrderHistory";
            this.groupControlOrderHistory.Size = new System.Drawing.Size(869, 533);
            this.groupControlOrderHistory.TabIndex = 0;
            this.groupControlOrderHistory.Text = "History List";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 33);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(865, 498);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lbl_Clientname);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbl_County);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lbl_State);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lbl_Subprocess);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(859, 47);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lbl_Clientname
            // 
            this.lbl_Clientname.AutoSize = true;
            this.lbl_Clientname.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Clientname.Location = new System.Drawing.Point(117, 15);
            this.lbl_Clientname.Name = "lbl_Clientname";
            this.lbl_Clientname.Size = new System.Drawing.Size(72, 16);
            this.lbl_Clientname.TabIndex = 12;
            this.lbl_Clientname.Text = "Clientname";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.label3.Location = new System.Drawing.Point(16, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "CLIENT NAME:";
            // 
            // lbl_County
            // 
            this.lbl_County.AutoSize = true;
            this.lbl_County.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_County.Location = new System.Drawing.Point(763, 12);
            this.lbl_County.Name = "lbl_County";
            this.lbl_County.Size = new System.Drawing.Size(49, 20);
            this.lbl_County.TabIndex = 15;
            this.lbl_County.Text = "County";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.label4.Location = new System.Drawing.Point(243, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "SUBPROCESS NAME:";
            // 
            // lbl_State
            // 
            this.lbl_State.AutoSize = true;
            this.lbl_State.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_State.Location = new System.Drawing.Point(586, 15);
            this.lbl_State.Name = "lbl_State";
            this.lbl_State.Size = new System.Drawing.Size(38, 16);
            this.lbl_State.TabIndex = 14;
            this.lbl_State.Text = "State";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.label5.Location = new System.Drawing.Point(528, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "STATE:";
            // 
            // lbl_Subprocess
            // 
            this.lbl_Subprocess.AutoSize = true;
            this.lbl_Subprocess.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Subprocess.Location = new System.Drawing.Point(383, 16);
            this.lbl_Subprocess.Name = "lbl_Subprocess";
            this.lbl_Subprocess.Size = new System.Drawing.Size(74, 16);
            this.lbl_Subprocess.TabIndex = 13;
            this.lbl_Subprocess.Text = "Subprocess";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.label6.Location = new System.Drawing.Point(695, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "COUNTY:";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.splitContainerControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 56);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(859, 439);
            this.panelControl1.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(859, 439);
            this.splitContainerControl1.SplitterPosition = 257;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.groupControl2.Appearance.Options.UseBackColor = true;
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl2.CaptionImageOptions.Image")));
            this.groupControl2.Controls.Add(this.gridControlOrderHistory);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(859, 257);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Order History";
            // 
            // gridControlOrderHistory
            // 
            this.gridControlOrderHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOrderHistory.Location = new System.Drawing.Point(2, 21);
            this.gridControlOrderHistory.MainView = this.gridViewOrderHistory;
            this.gridControlOrderHistory.Name = "gridControlOrderHistory";
            this.gridControlOrderHistory.Size = new System.Drawing.Size(855, 234);
            this.gridControlOrderHistory.TabIndex = 18;
            this.gridControlOrderHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOrderHistory});
            // 
            // gridViewOrderHistory
            // 
            this.gridViewOrderHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Assignee,
            this.Assigner,
            this.orderTask,
            this.progressType,
            this.workType,
            this.stage,
            this.date});
            this.gridViewOrderHistory.GridControl = this.gridControlOrderHistory;
            this.gridViewOrderHistory.IndicatorWidth = 30;
            this.gridViewOrderHistory.Name = "gridViewOrderHistory";
            this.gridViewOrderHistory.OptionsBehavior.Editable = false;
            this.gridViewOrderHistory.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewOrderHistory_CustomDrawRowIndicator);
            // 
            // Assignee
            // 
            this.Assignee.Caption = "Assignee";
            this.Assignee.FieldName = "User_Name";
            this.Assignee.Name = "Assignee";
            this.Assignee.Visible = true;
            this.Assignee.VisibleIndex = 0;
            // 
            // Assigner
            // 
            this.Assigner.Caption = "Assigner";
            this.Assigner.FieldName = "AssignedBy";
            this.Assigner.Name = "Assigner";
            this.Assigner.Visible = true;
            this.Assigner.VisibleIndex = 1;
            // 
            // orderTask
            // 
            this.orderTask.Caption = "Order Task";
            this.orderTask.FieldName = "Order_Status";
            this.orderTask.Name = "orderTask";
            this.orderTask.Visible = true;
            this.orderTask.VisibleIndex = 2;
            // 
            // progressType
            // 
            this.progressType.Caption = "Progress Type";
            this.progressType.FieldName = "Progress_Status";
            this.progressType.Name = "progressType";
            this.progressType.Visible = true;
            this.progressType.VisibleIndex = 3;
            // 
            // workType
            // 
            this.workType.Caption = "Order Type";
            this.workType.FieldName = "Work_Type";
            this.workType.Name = "workType";
            this.workType.Visible = true;
            this.workType.VisibleIndex = 4;
            // 
            // stage
            // 
            this.stage.Caption = "Stage";
            this.stage.FieldName = "Modification_Type";
            this.stage.Name = "stage";
            this.stage.Visible = true;
            this.stage.VisibleIndex = 5;
            // 
            // date
            // 
            this.date.Caption = "Date";
            this.date.DisplayFormat.FormatString = "G";
            this.date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.date.FieldName = "Inserted_Date";
            this.date.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.date.Name = "date";
            this.date.Visible = true;
            this.date.VisibleIndex = 6;
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl3.CaptionImageOptions.Image")));
            this.groupControl3.Controls.Add(this.gridControlOrderStatusHistory);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(859, 170);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "Order Status History";
            // 
            // gridControlOrderStatusHistory
            // 
            this.gridControlOrderStatusHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOrderStatusHistory.Location = new System.Drawing.Point(2, 31);
            this.gridControlOrderStatusHistory.MainView = this.gridViewOrderStatusHistory;
            this.gridControlOrderStatusHistory.Name = "gridControlOrderStatusHistory";
            this.gridControlOrderStatusHistory.Size = new System.Drawing.Size(855, 137);
            this.gridControlOrderStatusHistory.TabIndex = 18;
            this.gridControlOrderStatusHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOrderStatusHistory});
            // 
            // gridViewOrderStatusHistory
            // 
            this.gridViewOrderStatusHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.task,
            this.status,
            this.statusChangeby,
            this.reason,
            this.changedDate});
            this.gridViewOrderStatusHistory.GridControl = this.gridControlOrderStatusHistory;
            this.gridViewOrderStatusHistory.IndicatorWidth = 30;
            this.gridViewOrderStatusHistory.Name = "gridViewOrderStatusHistory";
            this.gridViewOrderStatusHistory.OptionsBehavior.Editable = false;
            this.gridViewOrderStatusHistory.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewOrderStatusHistory_CustomDrawRowIndicator);
            // 
            // task
            // 
            this.task.Caption = "Task";
            this.task.FieldName = "Order_Status";
            this.task.Name = "task";
            this.task.Visible = true;
            this.task.VisibleIndex = 0;
            // 
            // status
            // 
            this.status.Caption = "Status";
            this.status.FieldName = "Progress_Status";
            this.status.Name = "status";
            this.status.Visible = true;
            this.status.VisibleIndex = 1;
            // 
            // statusChangeby
            // 
            this.statusChangeby.Caption = "Status Changed By";
            this.statusChangeby.FieldName = "Employee_Name";
            this.statusChangeby.Name = "statusChangeby";
            this.statusChangeby.Visible = true;
            this.statusChangeby.VisibleIndex = 2;
            // 
            // reason
            // 
            this.reason.Caption = "Reason";
            this.reason.FieldName = "Reason";
            this.reason.Name = "reason";
            this.reason.Visible = true;
            this.reason.VisibleIndex = 3;
            // 
            // changedDate
            // 
            this.changedDate.Caption = "Status Changed On";
            this.changedDate.DisplayFormat.FormatString = "G";
            this.changedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.changedDate.FieldName = "Date";
            this.changedDate.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.changedDate.Name = "changedDate";
            this.changedDate.Visible = true;
            this.changedDate.VisibleIndex = 4;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            // 
            // OrderHistory
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 533);
            this.Controls.Add(this.groupControlOrderHistory);
            this.LookAndFeel.SkinName = "Office 2010 Blue";
            this.MinimumSize = new System.Drawing.Size(885, 571);
            this.Name = "OrderHistory";
            this.Text = "Order History";
            this.Load += new System.EventHandler(this.OrderHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlOrderHistory)).EndInit();
            this.groupControlOrderHistory.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrderHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrderHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOrderStatusHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOrderStatusHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlOrderHistory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Clientname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_County;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_State;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_Subprocess;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControlOrderHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrderHistory;
        private DevExpress.XtraGrid.Columns.GridColumn Assignee;
        private DevExpress.XtraGrid.Columns.GridColumn Assigner;
        private DevExpress.XtraGrid.Columns.GridColumn orderTask;
        private DevExpress.XtraGrid.Columns.GridColumn progressType;
        private DevExpress.XtraGrid.Columns.GridColumn workType;
        private DevExpress.XtraGrid.Columns.GridColumn stage;
        private DevExpress.XtraGrid.Columns.GridColumn date;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraGrid.GridControl gridControlOrderStatusHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOrderStatusHistory;
        private DevExpress.XtraGrid.Columns.GridColumn task;
        private DevExpress.XtraGrid.Columns.GridColumn status;
        private DevExpress.XtraGrid.Columns.GridColumn statusChangeby;
        private DevExpress.XtraGrid.Columns.GridColumn reason;
        private DevExpress.XtraGrid.Columns.GridColumn changedDate;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}