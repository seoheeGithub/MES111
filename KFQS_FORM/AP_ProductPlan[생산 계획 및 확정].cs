﻿#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : AP_ProductPlan
//   Form Name    : 자재 재고관리 
//   Name Space   : KFQS_Form
//   Created Date : 2020/08
//   Made By      : DSH
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region < USING AREA >
using System;
using System.Data;
using DC_POPUP;

using DC00_assm;
using DC00_WinForm;

using Infragistics.Win.UltraWinGrid;
#endregion

namespace KFQS_Form
{
    public partial class AP_ProductPlan : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp = new DataTable(); // 
        UltraGridUtil _GridUtil = new UltraGridUtil();  //그리드 객체 생성
        Common _Common = new Common();
        string plantCode = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public AP_ProductPlan()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void AP_ProductPlan_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANNO", "계획번호", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE", "생산품목", true, GridColDataType_emu.VarChar, 300, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANQTY", "계획수량", true, GridColDataType_emu.Double, 100, 120, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE", "단위", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장", true, GridColDataType_emu.VarChar, 200, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK", "확정", true, GridColDataType_emu.CheckBox, 100, 120, Infragistics.Win.HAlign.Center, true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO", "작업지시번호", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERDATE", "확정일시", true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERWORKER", "확정자", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERCLOSEFLAG", "지시종료여부", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER", "등록자", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE", "등록일시", true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Center, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITOR", "수정자", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE", "수정일시", true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Center, true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion


            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("UNITCODE");     //단위
            UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("YESNO");     // 지시 종료 여부
            UltraGridUtil.SetComboUltraGrid(this.grid1, "ORDERCLOSEFLAG", rtnDtTemp, "CODE_ID", "CODE_NAME");
            Common.FillComboboxMaster(this.cboOrderCloseFlag, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            rtnDtTemp = _Common.GET_Workcenter_Code();     //작업장
            Common.FillComboboxMaster(this.cboWorkcenterCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            // 품목코드 
            //FP  : 완제품
            //OM  : 외주가공품
            //R/M : 원자재
            //S/M : 부자재(H / W)
            //SFP : 반제품
            rtnDtTemp = _Common.GET_ItemCodeFERT_Code("FERT");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            #endregion


            #region ▶ ENTER-MOVE ◀
            cboPlantCode.Value = plantCode;
            #endregion
        }
        #endregion


        #region < TOOL BAR AREA >
        public override void DoInquire()
        {
            DoFind();
        }
        private void DoFind()
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                base.DoInquire();
                _GridUtil.Grid_Clear(grid1);
                string sPlantcode = Convert.ToString(cboPlantCode.Value);
                string sWorkcenterCode = Convert.ToString(cboWorkcenterCode_H.Value);
                string sOrderno = Convert.ToString(txtOrderNo.Text);
                string sOrderCloseFlag = Convert.ToString(cboOrderCloseFlag.Value);

                rtnDtTemp = helper.FillTable("00AP_ProductPlan_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantcode, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("WORKCENTERCODE", sWorkcenterCode, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ORDERNO", sOrderno, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ORDERCLOSEFLAG", sOrderCloseFlag, DbType.String, ParameterDirection.Input)
                                    );

                this.ClosePrgForm();
                this.grid1.DataSource = rtnDtTemp;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(), DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }


      
        public override void DoNew()
        {
            base.DoNew();
            try
            {
                this.grid1.InsertRow();
                this.grid1.SetDefaultValue("PLANTCODE", plantCode);

                grid1.ActiveRow.Cells["PLANNO"].Activation      = Activation.NoEdit;
                grid1.ActiveRow.Cells["CHK"].Activation         = Activation.NoEdit;
                grid1.ActiveRow.Cells["ORDERNO"].Activation     = Activation.NoEdit;
                grid1.ActiveRow.Cells["ORDERWORKER"].Activation = Activation.NoEdit;

                grid1.ActiveRow.Cells["MAKER"].Activation      = Activation.NoEdit;
                grid1.ActiveRow.Cells["MAKEDATE"].Activation   = Activation.NoEdit;
                grid1.ActiveRow.Cells["EDITOR"].Activation     = Activation.NoEdit;
                grid1.ActiveRow.Cells["EDITDATE"].Activation   = Activation.NoEdit;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString());
            }
        }


        public override void DoDelete()
        {
            if (Convert.ToString(grid1.ActiveRow.Cells["CHK"].Value) == "1")
            {
                ShowDialog("작업지시 확정 내역을 취소 후 삭제 하세요.",
                 DialogForm.DialogType.OK);
                return;
            }
        }

        public override void DoSave()
        {
            // 그리드에 표현된 내용을 소스 바인딩에 포함한다.
            this.grid1.UpdateData();
            DataTable dtTemp = new DataTable();
            dtTemp = grid1.chkChange();

            if (dtTemp == null) return;

            DBHelper helper = new DBHelper("", true);
            try
            {
                this.Focus();

                if (this.ShowDialog("등록한 데이터를 저장 하시겠습니까?") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }

                base.DoSave();

                foreach (DataRow drRow in dtTemp.Rows)
                {
                    switch (drRow.RowState)
                    {
                        case DataRowState.Deleted:
                            #region 삭제
                            drRow.RejectChanges();
                            helper.ExecuteNoneQuery("12AP_ProductPlan_D1", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE", plantCode, DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("PLANNO", drRow["PLANNO"], DbType.String, ParameterDirection.Input)
                                                                    );

                            #endregion
                            break;
                        case DataRowState.Added:
                            #region 추가
                            string sErrorMsg = string.Empty;
                            if (Convert.ToString(drRow["ITEMCODE"]) == "")
                            {
                                sErrorMsg += "품목 ";
                            }
                            if (Convert.ToString(drRow["PLANQTY"]) == "")
                            {
                                sErrorMsg += "수량 ";
                            }
                            if (Convert.ToString(drRow["WORKCENTERCODE"]) == "")
                            {
                                sErrorMsg += "작업장 ";
                            }
                            if (sErrorMsg != "")
                            {
                                this.ClosePrgForm();
                                ShowDialog(sErrorMsg + "을 입력하지 않았습니다", DialogForm.DialogType.OK);
                                return;
                            }
                            helper.ExecuteNoneQuery("12AP_ProductPlan_I1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE", drRow["PLANTCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ITEMCODE", drRow["ITEMCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("PLANQTY", Convert.ToString(drRow["PLANQTY"]).Replace(",", ""), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("UNITCODE", drRow["UNITCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("WORKCENTERCODE", drRow["WORKCENTERCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("MAKER", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                  );

                            #endregion
                            break;
                        case DataRowState.Modified:
                            #region 수정
                            string sOrderFalg = string.Empty;
                            if (Convert.ToString(drRow["CHK"]).ToUpper() == "1") sOrderFalg = "Y";
                            else sOrderFalg = "N";

                            helper.ExecuteNoneQuery("12AP_ProductPlan_U1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE", drRow["PLANTCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("PLANNO", drRow["PLANNO"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ORDERFLAG", sOrderFalg, DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("EDITOR", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                  );


                            #endregion
                            break;
                    }
                }
                if (helper.RSCODE != "S")
                {
                    this.ClosePrgForm();
                    helper.Rollback();
                    this.ShowDialog(helper.RSMSG, DialogForm.DialogType.OK);
                    return;
                }
                helper.Commit();
                this.ClosePrgForm();
                this.ShowDialog("R00002", DialogForm.DialogType.OK);    // 데이터가 저장 되었습니다.
                DoInquire();
            }
            catch (Exception ex)
            {
                CancelProcess = true;
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
        #endregion

    }
}


            