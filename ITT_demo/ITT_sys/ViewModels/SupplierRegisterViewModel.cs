using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using ITT_sys.Models;
using ITT_sys.Views;
using ITT_sys.ViewModels.DB_Connection;
using System.Windows;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using ITT_sys.ViewModels.Commands;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Input;


namespace ITT_sys.ViewModels
{
    public class SupplierRegisterViewModel : Conductor<object>, INotifyPropertyChanged
    {

       

        #region propties initializing


        static public string SupName;
        static public int supID ;
       static int sID;


        
        public string Status = "active";
        
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;

                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);

            }
        }



        private string _supplierCode;

        public string SupplierCode
        {
            get { return _supplierCode; }
            set
            {
                _supplierCode = value;
                NotifyOfPropertyChange(() => SupplierCode);
            }
        }


     
    private string _supplierName;

        public string SupplierName
        {
            get { return _supplierName; }
            set
            {
                _supplierName = value;
                NotifyOfPropertyChange(() => SupplierName);
            }
        }

        private string _contactPerson
;

        public string ContactPerson

        {
            get { return _contactPerson; }
            set
            {
                _contactPerson = value;
                NotifyOfPropertyChange(() => ContactPerson
);
            }
        }

        private string _contactNo;

        public string ContactNo
        {
            get { return _contactNo; }
            set
            {
                _contactNo = value;
                NotifyOfPropertyChange(() => ContactNo);
            }
        }

        private string _faxNo;

        public string FaxNo
        {
            get { return _faxNo; }
            set
            {
                _faxNo = value;
                NotifyOfPropertyChange(() => FaxNo);
            }
        }

        private string _addrLine1;

        public string AddrLine1
        {
            get { return _addrLine1; }
            set
            {
                _addrLine1 = value;
                NotifyOfPropertyChange(() => AddrLine1);
            }
        }

        private string _addrLine2;

        public string AddrLine2
        {
            get { return _addrLine2; }
            set
            {
                _addrLine2 = value;
                NotifyOfPropertyChange(() => AddrLine2);
            }
        }

        private string _addrLine3;

        public string AddrLine3
        {
            get { return _addrLine3; }
            set
            {
                _addrLine3 = value;
                NotifyOfPropertyChange(() => AddrLine3);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        private string _bankCode;

        public string BankCode
        {
            get { return _bankCode; }
            set
            {
                _bankCode = value;
                NotifyOfPropertyChange(() => BankCode);
            }
        }

        private string _branchCode;

        public string BranchCode
        {
            get { return _branchCode; }
            set
            {
                _branchCode = value;
                NotifyOfPropertyChange(() => BranchCode);
            }
        }

        private string _chequeName;

        public string ChequeName
        {
            get { return _chequeName; }
            set
            {
                _chequeName = value;
                NotifyOfPropertyChange(() => ChequeName);
            }
        }


        private string _accountNo;

        public string AccountNo
        {
            get { return _accountNo; }
            set
            {
                _accountNo = value;
                NotifyOfPropertyChange(() => AccountNo);
            }
        }


        private string _truckId;

        public string TruckId
        {
            get { return _truckId; }
            set
            {
                _truckId = value;
                NotifyOfPropertyChange(() => TruckId);
            }
        }

        private string _truckType;

        public string TruckType
        {
            get { return _truckType; }
            set
            {
                _truckType = value;
                NotifyOfPropertyChange(() => TruckType);
            }
        }


        private string _truckSize;

        public string TruckSize
        {
            get { return _truckSize; }
            set
            {
                _truckSize = value;
                NotifyOfPropertyChange(() => TruckSize);
            }
        }

        private string  _registerdate ;

        public string registerDate
        {
            get { return _registerdate; }

            set { _registerdate = value; }
        }

        private DateTime _joinDate;

        public DateTime JoinDate
        {
            get { return _joinDate; }
            set { _joinDate = value; }
        }

        
        DBCon DB_Con = new DBCon();

       
        #endregion

        #region btton_click

        public ICommand SaveCommand { get; private set; }

        public bool CanSave
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region register_supplier

        public ObservableCollection<TruckerModel> GetAlltruckers()
        {
            ObservableCollection<TruckerModel> TruckCol = new ObservableCollection<TruckerModel>();
            DB_Con.OpenCon();
            DB_Con.com = new SqlCommand();
            DB_Con.com.Connection = DB_Con.con;
            DB_Con.com.CommandText = "Select TruckId,Truck_size,Truck_Type,Join_date,Status from Trucker_Details where sup_code='" + supID + "'";

            
            SqlDataReader Reader = DB_Con.com.ExecuteReader();
 
            while (Reader.Read())
            {
                TruckCol.Add(new TruckerModel()
                {
                    TruckId = (Reader["TruckId"]).ToString(),
                    TruckSize = (Reader["Truck_size"]).ToString(),
                    TruckType = (Reader["Truck_Type"]).ToString(),
                    registerDate = (Reader["Truck_Type"]).ToString(),
                    Status = (Reader["Status"]).ToString()
                });
            }

            DB_Con.CloseCon();
            return TruckCol;
        }

      

        public bool CanRegister(string supplierName, string branchCode, string bankCode)
        {
            return !String.IsNullOrWhiteSpace(supplierName) || !String.IsNullOrWhiteSpace(branchCode) || !String.IsNullOrWhiteSpace(bankCode);
        }

        public void Register(string supplierName, string branchCode, string bankCode)
        {

            try
            {

                string query = "INSERT INTO Supplier_details1  (Supplier_name,Mobile_Number,RecepientName,Address1,Address2,Address3,Registered_date,BranchCode,BankCode,account_no,status,Fax_Number,eTans,cheque_Name,Email) VALUES('"
                + this.SupplierName + "','"
                + this.ContactNo + "','"
                + this.ContactPerson + "','"
                + this.AddrLine1 + "','"
                + this.AddrLine2 + "','"
                + this.AddrLine3 + "','"
                + this._registerdate + "','"
                + this.BranchCode + "','"
                + this.BankCode + "','"
                + this.AccountNo + "','"
                + this.Status + "','"
                + this.FaxNo + "','"
                + this.IsSelected + "','"
                + this.ChequeName + "','"
                + this.Email +
                "');";
                
                int noline = DB_Con.insert(query);

                if (noline > 0)
                {
                    MessageBox.Show("Data inserted Successfully");
                    SupName = this.SupplierName;

                }
                else
                {
                    MessageBox.Show("Try again!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
               
        }

        public void InsertEmployee(TruckerModel objEmp)
        {
            Console.WriteLine("registe date=" + objEmp.registerDate);

            string query2 = "select TOP 1 sID from Supplier_details1 where Supplier_name ='"
               + SupName + "' order by Registered_date desc";
            supID = DB_Con.getSupID(query2);

            DB_Con.OpenCon();
            DB_Con.com = new SqlCommand();
            DB_Con.com.Connection = DB_Con.con;
            DB_Con.com.CommandText = "Insert into Trucker_Details(TruckId,Truck_size,Truck_Type,Join_date,Status,sup_code) Values(@TruckId,@TruckSize,@TruckType,@StartDate,@Status,@sup_code)";
            DB_Con.com.Parameters.AddWithValue("@TruckId", objEmp.TruckId);
            DB_Con.com.Parameters.AddWithValue("@TruckSize", objEmp.TruckSize);
            DB_Con.com.Parameters.AddWithValue("@TruckType", objEmp.TruckType);
            DB_Con.com.Parameters.AddWithValue("@StartDate", objEmp.registerDate);
            DB_Con.com.Parameters.AddWithValue("@Status", objEmp.Status);
            DB_Con.com.Parameters.AddWithValue("@sup_code", supID);
            DB_Con.com.ExecuteNonQuery();
            DB_Con.CloseCon();
        }

        public void DeleteTruck(TruckerModel objEmpToAdd)
        {
            string truckId = objEmpToAdd.TruckId;
           
            try
            {
                string query = "DELETE FROM Trucker_Details where TruckId ='" + truckId + "'";
               
                int noline = DB_Con.insert_del_update(query);

                if (noline > 0)
                {
                    MessageBox.Show("Row Deleted!");
                   
                }
                else
                {
                    MessageBox.Show("Try again!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region update_supplier

        //public ObservableCollection<SupplierModel> SupplierDetails { get; set; }

        //private void FillList()
        //{
        //    try
        //    {
        //        DB_Con.connection_Sql();
        //        DB_Con.con.Open();
        //        DB_Con.com = new SqlCommand("select Supplier_Code from Supplier_details1", DB_Con.con);
        //        DB_Con.adapter = new SqlDataAdapter(DB_Con.com);
        //        DB_Con.ds = new DataSet();
        //        DB_Con.adapter.Fill(DB_Con.ds, "tblBankdetails");

        //        if (SupplierDetails == null)
        //            SupplierDetails = new ObservableCollection<SupplierModel>();

        //        foreach (DataRow dr in DB_Con.ds.Tables[0].Rows)
        //        {
        //            SupplierDetails.Add(new SupplierModel
        //            {
        //                SupplierCode = dr["Supplier_Code"].ToString()

        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        DB_Con.ds = null;
        //        DB_Con.adapter.Dispose();
        //        DB_Con.con.Close();
        //        DB_Con.con.Dispose();
        //    }
        //}

        private SupplierModel _selectedValue;

        public SupplierModel SelectedValue
        {
            get { return _selectedValue; }

            set
            {
                _selectedValue = value;
                NotifyOfPropertyChange(() => SelectedValue);

                FillSupplierDetails(_selectedValue.SupplierCode);
                NotifyOfPropertyChange(() => ItemInEditMode2);

                Gettruckers(_selectedValue.SupplierCode);

            }
        }

        private ObservableCollection<TruckerModel> _truckDetails = new ObservableCollection<TruckerModel>();

        public ObservableCollection<TruckerModel> TruckerDetails
        {
            get { return _truckDetails; }
            set { _truckDetails = value; }
        }


        public void Gettruckers( string supcode)
        {
           
            string query = "select sID  from Supplier_details1 where Supplier_Code = '" + _selectedValue.SupplierCode + "'";
            sID = DB_Con.getSupID(query);

            DB_Con.OpenCon();
            DB_Con.com = new SqlCommand();
            DB_Con.com.Connection = DB_Con.con;
            DB_Con.com.CommandText = "Select TruckId,Truck_size,Truck_Type,Join_date,Status from Trucker_Details where sup_code='" + sID + "'";

             
            SqlDataReader Reader = DB_Con.com.ExecuteReader();

            while (Reader.Read())
            {
                TruckerDetails.Add(new TruckerModel()
                {
                    TruckId = (Reader["TruckId"]).ToString(),
                    TruckSize = (Reader["Truck_size"]).ToString(),
                    TruckType = (Reader["Truck_Type"]).ToString(),
                    JoinDate = (Convert.ToDateTime(Reader["Join_date"])),
                    Status = (Reader["Status"]).ToString()
                });
                Console.WriteLine("register date" + registerDate);
            }
            DB_Con.CloseCon();
        }

        public ObservableCollection<SupplierModel> FillList()
        {
            ObservableCollection<SupplierModel> SupplierDetails = new ObservableCollection<SupplierModel>();
            try
            {
                DB_Con.connection_Sql();
                DB_Con.con.Open();
                DB_Con.com = new SqlCommand("select Supplier_Code from Supplier_details1", DB_Con.con);
                DB_Con.adapter = new SqlDataAdapter(DB_Con.com);
                DB_Con.ds = new DataSet();
                DB_Con.adapter.Fill(DB_Con.ds, "tblBankdetails");

                if (SupplierDetails == null)
                    SupplierDetails = new ObservableCollection<SupplierModel>();

                foreach (DataRow dr in DB_Con.ds.Tables[0].Rows)
                {
                    SupplierDetails.Add(new SupplierModel
                    {
                        SupplierCode = dr["Supplier_Code"].ToString()

                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                DB_Con.com.Dispose();
                DB_Con.con.Close();
            }
            
              return SupplierDetails;
        }

        private void FillSupplierDetails(string supCode)
        {
            try
            {
                DB_Con.con = new SqlConnection(DB_Con.ConString);
                DB_Con.con.Open();
                string query = "select * from Supplier_details1 where Supplier_Code = '" + supCode + "'";
                DB_Con.com = new SqlCommand(query, DB_Con.con);
                SqlDataAdapter da = new SqlDataAdapter(DB_Con.com);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblBankdetails");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string supName = dr["Supplier_name"].ToString();
                    ItemInEditMode2.SupplierName = supName;

                    string contactPerson = dr["RecepientName"].ToString();
                    ItemInEditMode2.ContactPerson = contactPerson;

                    string contactNo = dr["Mobile_Number"].ToString();
                    ItemInEditMode2.ContactNo = contactNo;

                    string faxNo = dr["Fax_Number"].ToString();
                    ItemInEditMode2.FaxNo = faxNo;

                    string addrLine1 = dr["Address1"].ToString();
                    ItemInEditMode2.AddrLine1 = addrLine1;

                    string addrLine2 = dr["Address2"].ToString();
                    ItemInEditMode2.AddrLine2 = addrLine2;

                    string addrLine3 = dr["Address3"].ToString();
                    ItemInEditMode2.AddrLine3 = addrLine3;

                    string email = dr["Email"].ToString();
                    ItemInEditMode2.Email = email;

                    string branchCode = dr["BranchCode"].ToString();
                    ItemInEditMode2.BankCode = branchCode;

                    string bankCode = dr["BankCode"].ToString();
                    ItemInEditMode2.BranchCode = bankCode;

                    string chequeName = dr["cheque_Name"].ToString();
                    ItemInEditMode2.ChequeName = chequeName;

                    string accountNo = dr["account_no"].ToString();
                    ItemInEditMode2.AccountNo = accountNo;
                }

                DB_Con.com.Dispose();
                DB_Con.con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private SupplierModel _itemInEditMode2 = new SupplierModel();

        public SupplierModel ItemInEditMode2
        {
            get { return _itemInEditMode2; }
        }

        //private void SearchSupplier()
        //{

        //    try
        //    {

        //        string query = "select * from Supplier_details1 where Supplier_Code like '%" + SupplierCode + "%'";
        //        DB_Con.connection_Sql();
        //        DB_Con.OpenCon();
        //        DB_Con.com = new SqlCommand(query, DB_Con.con);
        //        DB_Con.adapter = new SqlDataAdapter(DB_Con.com);
        //        DB_Con.ds = new DataSet();
        //        DB_Con.adapter.Fill(DB_Con.ds, "tb2Bankdetails");

        //        foreach (DataRow dr in DB_Con.ds.Tables[0].Rows)
        //        {
        //            string supName = dr["Supplier_name"].ToString();
        //            ItemInEditMode2.SupplierName = supName;

        //            string contactPerson = dr["RecepientName"].ToString();
        //            ItemInEditMode2.ContactPerson = contactPerson;

        //            string contactNo = dr["Mobile_Number"].ToString();
        //            ItemInEditMode2.ContactNo = contactNo;

        //            string faxNo = dr["Fax_Number"].ToString();
        //            ItemInEditMode2.FaxNo = faxNo;

        //            string addrLine1 = dr["Address1"].ToString();
        //            ItemInEditMode2.AddrLine1 = addrLine1;

        //            string addrLine2 = dr["Address2"].ToString();
        //            ItemInEditMode2.AddrLine2 = addrLine2;

        //            string addrLine3 = dr["Address3"].ToString();
        //            ItemInEditMode2.AddrLine3 = addrLine3;

        //            string email = dr["Email"].ToString();
        //            ItemInEditMode2.Email = email;

        //            string branchCode = dr["BranchCode"].ToString();
        //            ItemInEditMode2.BankCode = branchCode;

        //            string bankCode = dr["BankCode"].ToString();
        //            ItemInEditMode2.BranchCode = bankCode;

        //            string chequeName = dr["cheque_Name"].ToString();
        //            ItemInEditMode2.ChequeName = chequeName;

        //            string accountNo = dr["account_no"].ToString();
        //            ItemInEditMode2.AccountNo = accountNo;
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        DB_Con.adapter.Dispose();
        //        DB_Con.con.Close();
        //        DB_Con.con.Dispose();
        //    }
        //}

        public void updateSupplier(TruckerModel objSupToAdd)
        {
            try
            {
                int supcode = sID;
                Console.WriteLine("supplier Code = " + supcode);
                DB_Con.OpenCon();
                DB_Con.com = new SqlCommand();
                DB_Con.com.Connection = DB_Con.con;
                string query = "UPDATE Trucker_Details SET Truck_size ='" + objSupToAdd.TruckSize.Trim() + "',Truck_Type ='" + objSupToAdd.TruckType.Trim() + "',Join_date = '" + objSupToAdd.JoinDate + "',Status='" + objSupToAdd.Status.Trim() + "'  where sup_code='" + supcode + "' and TruckId ='" + objSupToAdd.TruckId + "'";

                int noline = DB_Con.insert(query);

                if (noline > 0)
                {
                    MessageBox.Show("Update Successfully");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

           
           
        }

        public void Search()
        {
            try
            {
                SaveCommand = new DelegateCommand(Search, () => CanSave);
            }


           catch (Exception ex)
            {
               MessageBox.Show(ex.ToString());
            }

         }

        #endregion

    }
}
