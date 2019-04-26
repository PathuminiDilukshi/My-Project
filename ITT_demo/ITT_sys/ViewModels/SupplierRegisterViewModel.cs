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


namespace ITT_sys.ViewModels
{
    public class SupplierRegisterViewModel:Screen
    {
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

        private string _mobileNo;

        public string MobileNo
        {
            get { return _mobileNo; }
            set
            {
                _mobileNo = value;
                NotifyOfPropertyChange(() => MobileNo);
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

        private string _bankName;

        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                NotifyOfPropertyChange(() => BankName);
            }
        }

        private string _branchName;

        public string BranchName
        {
            get { return _branchName; }
            set
            {
                _branchName = value;
                NotifyOfPropertyChange(() => BranchName);
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

        private string _status;

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }


        private string _joinDate;

        public string JoinDate
        {
            get { return _joinDate; }
            set
            {
                _joinDate = value;
                NotifyOfPropertyChange(() => JoinDate);
            }
        }

        DBCon DB_Con = new DBCon();


        public ObservableCollection<TruckerModel> GetAlltruckers()
        {
            ObservableCollection<TruckerModel> EmpCol = new ObservableCollection<TruckerModel>();
            DB_Con.OpenCon();
            DB_Con.com = new SqlCommand();
            DB_Con.com.Connection = DB_Con.con;
            DB_Con.com.CommandText = "Select * from Trucker_Details";
            SqlDataReader Reader = DB_Con.com.ExecuteReader();
 
            while (Reader.Read())
            {
                EmpCol.Add(new TruckerModel()
                {
                    TruckId = (Reader["TruckId"]).ToString(),
                    TruckType = (Reader["Truck_size"]).ToString(),
                    TruckSize = (Reader["Truck_Type"]).ToString(),
                    JoinDate = (Reader["Join_date"]).ToString(),
                    Status = (Reader["Status"]).ToString()
                });
            }

            DB_Con.CloseCon();
            return EmpCol;
        }

        public void InsertEmployee(TruckerModel objEmp)
        {
            DB_Con.OpenCon();
            DB_Con.com = new SqlCommand();
            DB_Con.com.Connection = DB_Con.con;
            DB_Con.com.CommandText = "Insert into Trucker_Details(TruckId,Truck_size,Truck_Type,Join_date,Status) Values(@TruckId,@TruckSize,@TruckType,@JoinDate,@Status)";
            DB_Con.com.Parameters.AddWithValue("@TruckId", objEmp.TruckId);
            DB_Con.com.Parameters.AddWithValue("@TruckSize", objEmp.TruckSize);
            DB_Con.com.Parameters.AddWithValue("@TruckType", objEmp.TruckType);
            DB_Con.com.Parameters.AddWithValue("@JoinDate", objEmp.JoinDate);
            DB_Con.com.Parameters.AddWithValue("@Status", objEmp.Status);
            DB_Con.com.ExecuteNonQuery();
            DB_Con.CloseCon();
        }
    }
}
