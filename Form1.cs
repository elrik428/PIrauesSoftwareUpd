using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Piraeus_UpdtSoftwareVC
{
    public partial class Form1 : Form
    {
        public static OleDbConnection VCConn = null;
        //public static string DBName = "vc30";//Production
        public static string DBName = "vc30";//Production
        public static string VCIP = "Provider=SQLOLEDB;Data Source=10.1.45.144;Connect Timeout=30;Initial Catalog=vc30";//Production

        public Form1()
        {
            connectVC();
            if (VCConn.State != ConnectionState.Open)
            {
                Console.WriteLine("ERROR: Unable to connect to VeriCentre.");
                return;
            }

            InitializeComponent();

            listView_TID.MultiSelect = true;
            listView_TID.Columns.Add("TID");
            listView_TID.Columns.Add("Version");
            listView_TID.Columns.Add("Type");

            listView_TID_noCtls.Columns.Add("TID");
                        
        }


        static void connectVC()
        {

            /************CONNECT**************/
            for (int i = 0; i < 3; i++)
            {
                try
                {
                   VCConn = new OleDbConnection(VCIP + ";User Id=vc30;Password=VcEn!@#20130627");//Production
                  // VCConn = new OleDbConnection(VCIP + ";User Id=VeriCentre;Password=VcLe!@#20130423");//Test
                    VCConn.Open();

                    if (VCConn.State == ConnectionState.Open)
                        i = 3;
                }
                catch (Exception exc)
                {


                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            //int z = 0;
            OleDbCommand listView_Tids = VCConn.CreateCommand();
            if (textBox_MultTid.Text.Length > 0 && !listView_TID.Items.Cast<ListViewItem>().Any(item => item.Text == textBox_MultTid.Text))
            {
                char[] separator = { '\n', '\r', ' ' };
                String[] word = textBox_MultTid.Text.Split(separator);
                for (i = 0; i < word.Length; i++)
                {
                    if (!String.IsNullOrEmpty(word[i]) && !listView_TID.Items.Cast<ListViewItem>().Any(item => item.Text == word[i]))
                    {
                        string[] hostid_Descr = new string[5];
                        //z = 0;
                        listView_Tids.CommandText =
                            "select distinct a.CLUSTERID,a.TERMID, a.APPNM, a.FAMNM   from      vc30.relation a where" +
                            " a.TERMID = ?                                                                                     " +
                            //" in (		select distinct b.TERMID from      vc30.relation  b                                " +
                            //"		where                                                                                       " +
                            //"		b.TERMID = ?                                                                                 " +
                            //"		AND b.ACCCNT = -1                                                                           " +
                            //"		and b.APPNM like ('CTLS%')                                                                  " +
                            //")                                                                                              " +
                            " AND substring(a.APPNM,1,4) = ('PIRA')                                                          " +
                            " and substring(a.APPNM,9,1) = ('P')                                                            " +
                            " order by a.FAMNM,a.APPNM                                                                       ";

                        //listView_Tids.CommandText =
                        //"select distinct termid, appnm, famnm  " +
                        //                            "from       vc30.relation " +
                        //                            "where " +
                        //                            "TERMID = ? " +
                        //                            "and substring(appnm,1,4) = ('PIRA') " +
                        //                            "and substring(appnm,9,1) = ('P') " +
                        //                            "AND acccnt = -1 " +
                        //                            "order by famnm,appnm ";
                        listView_Tids.Parameters.Add("@TERMID", OleDbType.VarChar, 16).Value = word[i];
                        listView_Tids.Connection = VCConn;
                        OleDbDataReader reader11 = listView_Tids.ExecuteReader();
                        while (reader11.Read())
                        {
                            listView_TID.Items.Add(new ListViewItem(new string[] { word[i], reader11.GetString(2), reader11.GetString(3) }));
                        }

                        listView_TID.Sort();
                        listView_TID.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        reader11.Close();
                        listView_Tids.Dispose();
                                                                        
                    }
                }

            }

            OleDbCommand listView_Tids_noCTLS = VCConn.CreateCommand();
            if (textBox_MultTid.Text.Length > 0 && !listView_TID_noCtls.Items.Cast<ListViewItem>().Any(item => item.Text == textBox_MultTid.Text))
            {
                char[] separator = { '\n', '\r', ' ' };
                String[] word = textBox_MultTid.Text.Split(separator);
                for (i = 0; i < word.Length; i++)
                {
                    if (!String.IsNullOrEmpty(word[i]) && !listView_TID_noCtls.Items.Cast<ListViewItem>().Any(item => item.Text == word[i]))
                    {
                        string[] hostid_Descr = new string[5];
                        //z = 0;
                        listView_Tids_noCTLS.CommandText =
                           "select a.TERMID from      vc30.RELATION a											    " +
                           "where not exists (                                                                             " +
                           "select distinct b.CLUSTERID,b.TERMID, b.APPNM, b.FAMNM   from      vc30.relation  b     " +
                           "where                                                                                          " +
                           "b.TERMID = ?                                                                                    " +
                           "AND b.ACCCNT = -1                                                                              " +
                           "and b.APPNM like ('CTLS%') and a.TERMID = b.TERMID)                                            " +
                           "and a.TERMID = ? " +
                           "group by TERMID                                                                                " +
                           "order by TERMID                                                                                ";

                        listView_Tids_noCTLS.Parameters.Add("@TERMID", OleDbType.VarChar, 16).Value = word[i];
                        listView_Tids_noCTLS.Parameters.Add("@TERMID", OleDbType.VarChar, 16).Value = word[i];
                        listView_Tids_noCTLS.Connection = VCConn;
                        OleDbDataReader reader011 = listView_Tids_noCTLS.ExecuteReader();
                        while (reader011.Read())
                        {
                            listView_TID_noCtls.Items.Add(new ListViewItem(new string[] { reader011.GetString(0)}));
                        }

                        listView_TID_noCtls.Sort();
                        listView_TID_noCtls.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        reader011.Close();
                        listView_Tids_noCTLS.Dispose();

                    }
                }

            }


        }

        private void listView_TID_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            this.listView_TID.ListViewItemSorter = new  ListViewItemComparer(e.Column);
            // Call the sort method to manually sort.
            listView_TID.Sort();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem eachItem in listView_TID.SelectedItems)
            {
                listView_TID.Items.Remove(eachItem);
            }

            listView_TID_noCtls.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string frModel= "";
            string templateModel = "";
            string From_Appnm = "";
            string From_Appnm1 = "";
            string From_CLA = "";
            string From_OS = "";
            string From_EOS = "";
            string From_CTLS = "";
            string To_Appnm = "";
            string To_Appnm1 = "";
            string To_CLA = "";
            string To_OS = "";
            string To_EOS = "";
            string To_CTLS = "";

            string[] frAppnmArray_temp = new string[10];
            string[] frAppnmArray_02 = new string[9];
            string[] toAppnmArray = new string[10];
            string[] toAppnmArray_Substr = new string[8];
            string[] getFrAppnm_str = new string[2];
            string[] getToAppnm_str = new string[2];
            
            
     
            foreach (ListViewItem itemTID in listView_TID.Items)
            {
                int z = 0;
                int y = 0;
                int o = 0;
                int n = 0;
                int countCLTS = 0;
                int countparmLock = 0;

                string versionCheck = "";
                OleDbCommand version02Check = VCConn.CreateCommand();
                version02Check.CommandText =
                                        "select distinct appnm  " +
                                        "from       vc30.relation " +
                                        "where " +
                                        "TERMID = ? " +
                                        "and substring(appnm,1,4) = ('PIRA') " +
                                        "and substring(appnm,9,1) = ('P') " +
                                        "AND acccnt = -1 "; ;
                version02Check.Parameters.Add("@TERMID", OleDbType.VarChar, 10).Value = itemTID.SubItems[0].Text.ToString();
                version02Check.CommandTimeout = 60;
                OleDbDataReader versionReader = version02Check.ExecuteReader();
                while (versionReader.Read())
                {
                    versionCheck = versionReader.GetString(0);
                }

                version02Check.Dispose();
                versionReader.Close();

                string tempTID = itemTID.SubItems[0].Text.ToString();

                //OleDbCommand ctlsChoose = VCConn.CreateCommand();
                //ctlsChoose.CommandText = "select count(*) from       vc30.relation where TERMID =  ? ";
                //ctlsChoose.Parameters.Add("@TERMID", OleDbType.VarChar, 8).Value = itemTID.SubItems[0].Text.ToString();
                //countCLTS = Convert.ToInt32(ctlsChoose.ExecuteScalar());
                

                    OleDbCommand versionSelectFr = VCConn.CreateCommand();
                    versionSelectFr.CommandText = "select famnm,appnm from       vc30.relation where " +
                                                "TERMID =  ? " +
                                                "group by CLUSTERID,famnm,appnm " +
                                                "ORDER BY CLUSTERID,famnm,appnm ";                
                        versionSelectFr.Parameters.Add("@TERMID", OleDbType.VarChar, 10).Value = itemTID.SubItems[0].Text.ToString();
                        versionSelectFr.CommandTimeout = 60;
                        OleDbDataReader readerFr = versionSelectFr.ExecuteReader();
                        while (readerFr.Read())
                        {
                            frModel = readerFr.GetString(0);
                            frAppnmArray_temp[z] = readerFr.GetString(1);
                            z++;
                        }

                        versionSelectFr.Dispose();
                        readerFr.Close();

                        string[] frAppnmArray = (from item in frAppnmArray_temp.AsQueryable() where item != null select item).ToArray();

                        switch (frModel)
                        {
                            case "Vx-520":
                                templateModel = "520GCTLSSALES";
                                break;
                            case "Vx-675":
                                templateModel = "675CTLSSAL";
                                break;
                            case "Vx-675WiFi":
                                templateModel = "675WIFISAL";
                                break;
                            //case "Vx-690":
                            //    templateModel = "690Templ";
                            //    break;
                        }

                        OleDbCommand versionSelectTo = VCConn.CreateCommand();
                        versionSelectTo.CommandText = "select famnm,appnm from       vc30.relation where " +
                                                    "TERMID =  ? " +
                                                    "group by CLUSTERID,famnm,appnm " +
                                                    "ORDER BY CLUSTERID,famnm,appnm ";
                        versionSelectTo.Parameters.Add("@TERMID", OleDbType.VarChar, 13).Value = templateModel.ToString();
                        versionSelectTo.CommandTimeout = 60;
                        OleDbDataReader readerTo = versionSelectTo.ExecuteReader();

                        while (readerTo.Read())
                        {
                            toAppnmArray[y] = readerTo.GetString(1);
                            y++;
                        }

                        versionSelectTo.Dispose();
                        readerTo.Close();
                        
                                           
                        //if (countCLTS == 7)
                        //{
                        //    From_Appnm = frAppnmArray[4];
                        //    From_Appnm1 = frAppnmArray[3];
                        //    From_CLA = frAppnmArray[1];
                        //    From_EOS = frAppnmArray[2];
                        //    From_OS = frAppnmArray[5];
                            
                        //}
                        //else if (countCLTS == 8)
                        //{

                        //    From_CLA = frAppnmArray[1];
                        //    From_CTLS = frAppnmArray[2];
                        //    From_EOS = frAppnmArray[3];
                        //    From_Appnm1 = frAppnmArray[4];
                        //    From_Appnm = frAppnmArray[5];
                        //    From_OS = frAppnmArray[6];
                        //}

                        To_OS = toAppnmArray[0];
                        To_CLA = toAppnmArray[2];
                        To_CTLS = toAppnmArray[4];
                        To_EOS = toAppnmArray[6];
                        To_Appnm1 = toAppnmArray[7];
                        To_Appnm = toAppnmArray[8];

                        toAppnmArray_Substr[0] = "QT";
                        toAppnmArray_Substr[1] = "ACT";
                        toAppnmArray_Substr[2] = "CLA";
                        toAppnmArray_Substr[3] = "CTL";
                        toAppnmArray_Substr[4] = "EMV";
                        toAppnmArray_Substr[5] = "EOS";
                        toAppnmArray_Substr[6] = "VMA";
                        toAppnmArray_Substr[7] = "CMA";

                     for (int d = 0; d<toAppnmArray_Substr.Length; d++)

                      {
                        int indexFr = Array.FindIndex(frAppnmArray, element => element.Contains(toAppnmArray_Substr[d]) && element != null);
                        int indexTo = Array.FindIndex(toAppnmArray, element => element.Contains(toAppnmArray_Substr[d]));

                        // library exists in From TID
                        if (indexFr >= 0)
                        {
                            // Insert +  Delete Library
                            if (frAppnmArray[indexFr] != toAppnmArray[indexTo] && frAppnmArray[indexFr] != " ")
                            {
                                OleDbCommand softwareUpd_InsDel = VCConn.CreateCommand();
                                softwareUpd_InsDel.CommandText =
                               " declare @FromModel varchar(20)                                                                                                                    " +
                               " declare @ToModel varchar(20)                                                                                                                      " +
                               " declare @From_Lib varchar(10)                                                                                                                      " +
                               " declare @To_Lib varchar(10)                                                                                                                        " +
                               " SET @FromModel='" + frModel + "' " +
                               " SET @ToModel='" + frModel + "' " +
                               " SET @From_Lib='" + frAppnmArray[indexFr] + "' " +
                               " SET @To_Lib='" + toAppnmArray[indexTo] + "' " +
                                " insert into       vc30.RELATION                                                                                                                         " +
                                " (FAMNM,APPNM,TERMID,CLUSTERID,ACCCNT,LASTFULL,LASTPAR,ACCCODE,VIOLATIONCOUNT,LOCKED,MODON,MODBY,LOCKTIMESTAMP,EPROMID,DESCRIPTION,DLD_STATUS,     " +
                                " ISAUTODOWNLOAD,LAST_ATTEMPTED_DLD_DATE,VERSION,LASTPARAM_DLD_DATE,LASTFILE_DLD_DATE,FORUSES,FORMVIEWTYPE,SERVERID,TERM_FILE_UPDATES,              " +
                                " FORCEFILEDLD,FORCEPARAMDLD,FORCETERMFILEDLD)                                                                                                      " +
                                " select @ToModel,@To_Lib,TERMID,CLUSTERID,ACCCNT,NULL,NULL,ACCCODE,0,LOCKED,getdate(),                                                              " +
                                " 'SCRIPT1',NULL,NULL,DESCRIPTION,NULL,'N',NULL,NULL,NULL,NULL,FORUSES,FORMVIEWTYPE,NULL,TERM_FILE_UPDATES,'D','D','D'                              " +
                                " from       vc30.relation                                                                                                                                " +
                                " where famnm = @FromModel and appnm = @From_Lib                                                                                                     " +
                                " and TERMID  = '" + tempTID + "' " +
                                " delete from       vc30.RELATION                                                                                                                         " +
                                " where famnm = @FromModel and appnm = @From_Lib                                                                                                     " +
                                " and TERMID = '" + tempTID + "' ";

                                softwareUpd_InsDel.CommandTimeout = 500;
                                OleDbDataReader insertReader_1 = null;
                                try
                                {
                                    insertReader_1 = softwareUpd_InsDel.ExecuteReader();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error for TID : " + tempTID + "\r\n" + ex);
                                }

                                insertReader_1.Close();
                                softwareUpd_InsDel.Dispose();

                            }
                        }

                       // Library doesn't exist in From TID
                       else if(indexFr < 0)
                        // Insert Library
                       //if (frAppnmArray[indexFr] != toAppnmArray[indexTo] && frAppnmArray[indexFr] == " ")
                       {
                           OleDbCommand softwareUpd_Ins = VCConn.CreateCommand();
                           softwareUpd_Ins.CommandText =
                           " declare @FromModel varchar(20)                                                                                                                    " +
                           " declare @ToModel varchar(20)                                                                                                                      " +
                           " declare @From_Lib varchar(10)                                                                                                                     " +
                           " declare @To_Lib varchar(10)                                                                                                                       " +
                           " SET @FromModel='" + frModel + "' " +
                           " SET @ToModel='" + frModel + "' " +
                           " SET @From_Lib='" + To_OS + "' " +
                           " SET @To_Lib='" + toAppnmArray[indexTo] + "' " +
                           " insert into       vc30.RELATION                                                                                                        " +
                           " (FAMNM,APPNM,TERMID,CLUSTERID,ACCCNT,LASTFULL,LASTPAR,ACCCODE,VIOLATIONCOUNT,LOCKED,MODON,MODBY,LOCKTIMESTAMP,EPROMID,DESCRIPTION,DLD_STATUS,     " +
                           " ISAUTODOWNLOAD,LAST_ATTEMPTED_DLD_DATE,VERSION,LASTPARAM_DLD_DATE,LASTFILE_DLD_DATE,FORUSES,FORMVIEWTYPE,SERVERID,TERM_FILE_UPDATES,              " +
                           " FORCEFILEDLD,FORCEPARAMDLD,FORCETERMFILEDLD)                                                                                                      " +
                           " select @ToModel,@To_Lib,TERMID,CLUSTERID,ACCCNT,NULL,NULL,ACCCODE,0,LOCKED,getdate(),                                                             " +
                           " 'SCRIPT1',NULL,NULL,DESCRIPTION,NULL,'N',NULL,NULL,NULL,NULL,FORUSES,FORMVIEWTYPE,NULL,TERM_FILE_UPDATES,'D','D','D'                              " +
                           " from       vc30.relation                                                                                                               " +
                           " where famnm = @FromModel    and appnm = @From_Lib                                                                                                                      " +
                           " and TERMID = '" + tempTID + "' " ;

                           softwareUpd_Ins.CommandTimeout = 500;
                           OleDbDataReader insertReader_2 = null;
                           try
                           {
                               insertReader_2 = softwareUpd_Ins.ExecuteReader();

                           }
                           catch (Exception ex)
                           {
                               MessageBox.Show("Error for TID : " + tempTID + "\r\n" + ex);
                           }

                           insertReader_2.Close();
                           softwareUpd_Ins.Dispose();

                       }


                    }


                     //int indexFr_Appnm = Array.FindIndex(frAppnmArray, element => element.Contains("PIRA") && element.Substring(9,1)== "P");
                     //int indexTo_Appnm = Array.FindIndex(toAppnmArray, element => element.Contains("PIRA") && element.Substring(9,1) == "P");


                    OleDbCommand getAppnmFr = VCConn.CreateCommand();
                    getAppnmFr.CommandText =
                                             "select distinct appnm  " +
                                             "from       vc30.relation " +
                                             "where " +
                                             "TERMID = ? " +
                                             "and substring(appnm,1,4) = ('PIRA') " +
                                             "AND acccnt = -1 ";
                     getAppnmFr.Parameters.Add("@TERMID", OleDbType.VarChar, 10).Value = itemTID.SubItems[0].Text.ToString();
                     getAppnmFr.CommandTimeout = 60;
                     OleDbDataReader appnmReaderFr = getAppnmFr.ExecuteReader();
                     while (appnmReaderFr.Read())
                     {
                         getFrAppnm_str[o] = appnmReaderFr.GetString(0);
                         o++;
                     }

                     getAppnmFr.Dispose();
                     appnmReaderFr.Close();



                     OleDbCommand getAppnmTo = VCConn.CreateCommand();
                     getAppnmTo.CommandText =
                                             "select distinct appnm  " +
                                             "from       vc30.relation " +
                                             "where " +
                                             "TERMID = ? " +
                                             "and substring(appnm,1,4) = ('PIRA') " +
                                             "AND acccnt = -1 ";
                     getAppnmTo.Parameters.Add("@TERMID", OleDbType.VarChar, 13).Value = templateModel.ToString();
                     getAppnmTo.CommandTimeout = 60;
                     OleDbDataReader appnmReaderTo = getAppnmTo.ExecuteReader();
                     while (appnmReaderTo.Read())
                     {
                         getToAppnm_str[n] = appnmReaderTo.GetString(0);
                         n++;
                     }

                     getAppnmTo.Dispose();
                     appnmReaderTo.Close();

                     if (getFrAppnm_str[1] != getToAppnm_str[1])
                     {
                         // - - - - - - - - - - - - - - - - -    APPNM - APPNM1
                         OleDbCommand softwareUpdCTLS_6 = VCConn.CreateCommand();
                         softwareUpdCTLS_6.CommandText =
    " declare @FromModel varchar(20)                                                                                                                    " +
    " declare @ToModel varchar(20)                                                                                                                      " +
    " declare @FromAppnm varchar(10)                                                                                                                    " +
    " declare @ToAppnm varchar(10)                                                                                                                      " +
    " declare @FromAppnm1 varchar(10)                                                                                                                   " +
    " declare @ToAppnm1 varchar(10)                                                                                                                     " +
    " SET @FromModel='" + frModel + "' " +
    " SET @ToModel='" + frModel + "' " +
    " SET @FromAppnm='" + getFrAppnm_str[1] + "' " +
    " SET @ToAppnm='" + getToAppnm_str[1] + "' " +
    " SET @FromAppnm1='" + getFrAppnm_str[0] + "' " +
    " SET @ToAppnm1='" + getToAppnm_str[0] + "' " +
   "insert into       vc30.RELATION                                                                                                                          " +
     "(FAMNM,APPNM,TERMID,CLUSTERID,ACCCNT,LASTFULL,LASTPAR,ACCCODE,VIOLATIONCOUNT,LOCKED,MODON,MODBY,LOCKTIMESTAMP,EPROMID,DESCRIPTION,DLD_STATUS,      " +
     "ISAUTODOWNLOAD,LAST_ATTEMPTED_DLD_DATE,VERSION,LASTPARAM_DLD_DATE,LASTFILE_DLD_DATE,FORUSES,FORMVIEWTYPE,SERVERID,TERM_FILE_UPDATES,               " +
     "FORCEFILEDLD,FORCEPARAMDLD,FORCETERMFILEDLD)                                                                                                       " +
     "select @ToModel,@ToAppnm,TERMID,CLUSTERID,ACCCNT,NULL,NULL,ACCCODE,0,LOCKED,getdate(),                                                             " +
     "'SCRIPT1',NULL,NULL,DESCRIPTION,NULL,'N',NULL,NULL,NULL,NULL,FORUSES,FORMVIEWTYPE,NULL,TERM_FILE_UPDATES,'D','D','D'                               " +
     "from       vc30.relation                                                                                                                                 " +
     "where famnm = @FromModel and appnm = @FromAppnm                                                                                                    " +
     "and TERMID =  '" + tempTID + "' " +
     "insert into       vc30.RELATION                                                                                                                          " +
     "(FAMNM,APPNM,TERMID,CLUSTERID,ACCCNT,LASTFULL,LASTPAR,ACCCODE,VIOLATIONCOUNT,LOCKED,MODON,MODBY,LOCKTIMESTAMP,EPROMID,DESCRIPTION,DLD_STATUS,      " +
     "ISAUTODOWNLOAD,LAST_ATTEMPTED_DLD_DATE,VERSION,LASTPARAM_DLD_DATE,LASTFILE_DLD_DATE,FORUSES,FORMVIEWTYPE,SERVERID,TERM_FILE_UPDATES,               " +
     "FORCEFILEDLD,FORCEPARAMDLD,FORCETERMFILEDLD)                                                                                                       " +
     "select @ToModel,@ToAppnm1,TERMID,CLUSTERID,ACCCNT,NULL,NULL,ACCCODE,0,LOCKED,getdate(),                                                            " +
     "'SCRIPT1',NULL,NULL,DESCRIPTION,NULL,'N',NULL,NULL,NULL,NULL,FORUSES,FORMVIEWTYPE,NULL,TERM_FILE_UPDATES,'D','D','D'                               " +
     "from       vc30.relation                                                                                                                                 " +
     "where famnm = @FromModel and appnm = @FromAppnm1                                                                                                   " +
     "and TERMID =  '" + tempTID + "' " +
     "insert into       vc30.PARAMETER (FAMNM, APPNM, PARTID, PARNAMELOC, SEQINFO, DLDTYPE,PARINFO, VALUE,PSIZE,FLAG)                                          " +
     "select @ToModel model,@ToAppnm, PARTID, PARNAMELOC, SEQINFO, DLDTYPE,PARINFO, VALUE,PSIZE,FLAG                                                     " +
     "from       vc30.parameter                                                                                                                                " +
     "where famnm = @FromModel and appnm in (@FromAppnm, @FromAppnm1)                                                                                    " +
     "and PARTID =  '" + tempTID + "' " +
     "update       vc30.term_dld_files                                                                                                                         " +
     "set appnm = @ToAppnm                                                                                                                               " +
     "where famnm = @FromModel and appnm = @FromAppnm                                                                                                    " +
     "AND TERMID = '" + tempTID + "' " +
     "delete from       vc30.RELATION                                                                                                                          " +
     "where famnm = @FromModel and appnm = @FromAppnm                                                                                                    " +
     "and TERMID =  '" + tempTID + "' " +
     "delete from       vc30.RELATION                                                                                                                          " +
     "where famnm = @FromModel and appnm = @FromAppnm1                                                                                                   " +
     "and TERMID =  '" + tempTID + "' ";
                         //+"GO ";
                         softwareUpdCTLS_6.CommandTimeout = 500;
                         OleDbDataReader insertReader_6 = null;
                         try
                         {
                             insertReader_6 = softwareUpdCTLS_6.ExecuteReader();
                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show("Error for TID : " + tempTID + "\r\n" + ex);
                         }

                         softwareUpdCTLS_6.Dispose();
                         insertReader_6.Close();


                         // - - - - - - - - - - - - - - - - -    USES - *UNZIP
                         OleDbCommand softwareUpdCTLS_7 = VCConn.CreateCommand();
                         softwareUpdCTLS_7.CommandText =
 "update       vc30.PARAMETER set value = 'T" + getToAppnm_str[1] + "' where parnameloc = 'USES' and partid =  '" + tempTID + "' " +
 " delete from       vc30.PARAMETER where parnameloc = '*unzip' and partid =  '" + tempTID + "' ";

                         softwareUpdCTLS_7.CommandTimeout = 500;
                         OleDbDataReader insertReader_7 = null;
                         try
                         {
                             insertReader_7 = softwareUpdCTLS_7.ExecuteReader();
                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show("Error for TID : " + tempTID + "\r\n" + ex);
                         }

                         softwareUpdCTLS_7.Dispose();
                         insertReader_7.Close();


                         // Check if param exist so to insert 

                         OleDbCommand paramLockCheck = VCConn.CreateCommand();
                         paramLockCheck.CommandText =
                                                 "select count(*)  " +
                                                 "from  vc30.PARAMETER " +
                                                 "where " +
                                                 "PARTID = ? " +
                                                 "and PARNAMELOC = 'LOCK_TRX_TIME' "  ;
                         paramLockCheck.Parameters.Add("@TERMID", OleDbType.VarChar, 10).Value = itemTID.SubItems[0].Text.ToString();
                         countparmLock = Convert.ToInt32(paramLockCheck.ExecuteScalar());

                         if(countparmLock == 0)
                         {

                             // - - - - - - - - - - - - - - - - -    Parm Insert for Lock timer value
                             OleDbCommand insParmLock = VCConn.CreateCommand();
                             insParmLock.CommandText =
"  insert into vc30.PARAMETER				" +
"  (FAMNM, APPNM, PARTID, PARNAMELOC, SEQINFO, DLDTYPE,PARINFO, VALUE,PSIZE,FLAG)                                                           " +
"  (                                                                                                                                        " +
"		select '" + frModel + "' , '" + toAppnmArray[8] + "' , '" + tempTID + "' , PARNAMELOC, (select  max(seqinfo) + 1 from vc30.PARAMETER where PARTID = '" + tempTID + "'), DLDTYPE,PARINFO,   " +
"			(select                                                                                                                               " +
"			case                                                                                                                                  " +
"			when PARNAMELOC = 'MCC' and [value] = 'RESTAURANT' then '06:00'                                                                       " +
"			else '00:01'                                                                                                                          " +
"			end as new_Value                                                                                                                      " +
"			from vc30.PARAMETER where PARTID = '" + tempTID + "' and PARNAMELOC = 'MCC'),                                                                      " +
"			PSIZE,FLAG                                                                                                                            " +
"			from vc30.parameter where PARTID = 'T" + toAppnmArray[8] + "' and PARNAMELOC = 'LOCK_TRX_TIME' and                                                  " +
"			famnm = '" + frModel + "') ";
                             insParmLock.CommandTimeout = 500;
                             OleDbDataReader insParmLockRdr = null;
                             try
                             {
                                 insParmLockRdr = insParmLock.ExecuteReader();
                             }
                             catch (Exception ex)
                             {
                                 MessageBox.Show("Error for TID : " + tempTID + "\r\n" + ex);
                             }

                         }


                         // - - - - - - - - - - - - - - - - -    Parm Insert for Logo
                         OleDbCommand insParmLogo = VCConn.CreateCommand();
                         insParmLogo.CommandText = 
                             "update vc30.PARAMETER " +
                             "set [value] = 'F:KATASTASHA_D.VFT' " +
                             "where PARNAMELOC = 'LOGO_DISP_EN' and PARTID  = '" + tempTID + "'";
                            
                         insParmLogo.CommandTimeout = 500;
                         OleDbDataReader insParmLogoRdr = null;
                         try
                         {
                             insParmLogoRdr = insParmLogo.ExecuteReader();
                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show("Error for TID : " + tempTID + "\r\n" + ex);
                         }

                         Array.Clear(frAppnmArray_temp, 0, 10);
                         Array.Clear(toAppnmArray, 0, 10);
                         Array.Clear(getFrAppnm_str, 0, 2);
                         Array.Clear(getToAppnm_str, 0, 2);

                     }
                }
            MessageBox.Show("Software update was successful.");
            //listView_TID.Clear();
            Cursor.Current = Cursors.Default;
            }
        }
 }

